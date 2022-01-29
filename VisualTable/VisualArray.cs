﻿using System.Collections.Generic;
using System.Data;

namespace VisualTable
{
    /// <summary>
    ///Класс для связывания массива с элементом DataGrid
    ///для визуализации данных, где усовершенствовано на удаление значений, восстановление при необходимости.
    ///Синхронизацию данных после выполнения undo and redo обязательно
    ///Во всех (почти) методах выходным значением является _res (таблица)
    /// </summary>
    public static class VisualArray
    {        
        public static DataTable _res = new DataTable();//Таблица
        private static Stack<int[,]> _reservedtable = new Stack<int[,]>();//Стек массивов(Зарезервированные таблицы)
        private static Stack<int[,]> _cancelledchanges = new Stack<int[,]>();//Стек массивов с отмененными изменениями
        private static bool _needreserve = true;//Используется для свойства NeedReserve        
        private static bool _wascancel;    //Значение об отмене изменений (была ли она)                                                         
        public static Stack<int[,]> ReservedTable { get => _reservedtable; }//Значение зарезервированных таблиц
        public static Stack<int[,]> CancelledChanges { get => _cancelledchanges; }//Значение отмененных таблиц
        /// <summary>
        /// Метод заполнения таблицы для одномерного значения (может использоваться для вывода результатов)
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns>_res</returns>
        public static DataTable ToDataTable(int[] matrix)
        {
            DataTable result = new DataTable();
            for (int i = 0; i < matrix.Length; i++)
            {
                result.Columns.Add("Column" + (i + 1), typeof(string));
            }
                DataRow row = result.NewRow();
                for (int i = 0; i < matrix.Length; i++)
                {
                    row[i] = matrix[i];
                }
                result.Rows.Add(row);            
            return result;
        }
        //Метод для заполнения таблицы значениями двумерного массива
        public static DataTable ToDataTable(int[,] matrix)
        {            
            _res = new DataTable();
             for (int i = 0; i < matrix.GetLength(1); i++)
            {
                _res.Columns.Add("Column" + (i+1), typeof(string));
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                DataRow row = _res.NewRow();
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    row[j] = matrix[i, j];
                }
                _res.Rows.Add(row);
            }
            if(_needreserve)ReserveTable(matrix);
            _needreserve = true;
            return _res;
        }
        /// <summary>
        /// Добавление новой строки в таблицу
        /// </summary>
        /// <returns>_res</returns>
        public static DataTable AddNewRow(ref int [,] dmas)
        {
            DataRow row;
            row = _res.NewRow();
            for (int i = 0; i < _res.Columns.Count; i++)
            {
                row[i] = 0;
            }
            _res.Rows.Add(row);
            ReserveTable(dmas = SyncData());            
            return _res;
        }/// <summary>
        /// Удаление строки из таблицы (динамически по индексу)
        /// </summary>
        /// <param name="index"></param>
        /// <returns>_res</returns>
        public static DataTable DeleteRow(ref int [,] dmas, int index)
        {
            DataRow row = _res.Rows[index];
            _res.Rows.Remove(row);
            ReserveTable(dmas = SyncData());
            return _res;
        }/// <summary>
        /// Добавление столбца в таблицу
        /// </summary>
        /// <returns>_res</returns>
        public static DataTable AddNewColumn(ref int [,] dmas)
        {            
            dmas = AddNewColumnIntoMas(dmas);            
            _res = ToDataTable(dmas);            
            return _res;
        }/// <summary>
        /// Удаление столбца в таблице (динамически по индексу)
        /// </summary>
        /// <param name="index"></param>
        /// <returns>_res</returns>
        public static DataTable DeleteColumn(ref int[,] dmas, int index)
        {            
            dmas = DeleteColumnIntoMas(dmas, index);            
            _res = ToDataTable(dmas);
            return _res;
        }
        /// <summary>
        /// Обновление значений для массива относительно таблицы (необходимо использовать в основной программе после метода cancel или redo)
        /// для успешного восстановления значений или возврата к проделанным изменениям
        /// </summary>
        /// <returns>dmas - двумерный массив, сформированный из значений таблицы</returns>
        public static int[,] SyncData()
        {            
            int[,] dmas = new int[_res.Rows.Count, _res.Columns.Count];
            for (int i = 0; i < dmas.GetLength(0); i++)
            {
                DataRow row = _res.Rows[i];
                for (int j = 0; j < dmas.GetLength(1); j++)
                {
                    bool prove = int.TryParse(row[j].ToString(), out dmas[i, j]);
                    if (!prove) dmas[i, j] = 0;
                }
            }            
            return dmas;
        }/// <summary>
        /// Добавление столбца в массив
        /// </summary>
        /// <param name="dmas"></param>
        /// <returns>newdmas - новый массив с добавленным столбцом</returns>
        public static int[,] AddNewColumnIntoMas(int [,] dmas)
        {
            int[,] newdmas;
            if (dmas != null)
            {
                newdmas = new int[dmas.GetLength(0), dmas.GetLength(1) + 1];
                for (int i = 0; i < dmas.GetLength(0); i++)
                {
                    for (int j = 0; j < dmas.GetLength(1); j++)
                    {
                        newdmas[i, j] = dmas[i, j];
                    }
                }
            }
            else
            {
                newdmas = new int[0, 1];
            }
            return newdmas;
        }/// <summary>
        /// Добавление новой строки в массив
        /// </summary>
        /// <param name="dmas"></param>
        /// <returns>newdmas - новый массив с добавленной строкой</returns>
        public static int[,] AddNewRowIntoMas(int[,] dmas)
        {
            int[,] newdmas;
            if (dmas != null)
            {
                newdmas = new int[dmas.GetLength(0) + 1, dmas.GetLength(1)];
                for (int i = 0; i < dmas.GetLength(0); i++)
                {
                    for (int j = 0; j < dmas.GetLength(1); j++)
                    {
                        newdmas[i, j] = dmas[i, j];
                    }
                }
            }
            else
            {
                newdmas = new int[1, 0];
            }
            return newdmas;
        }
        public static void DataTableClear()
        {
            _res.Clear();            
        }/// <summary>
        /// Удаление столбца внутри массива(динамически по индексу)
        /// </summary>
        /// <param name="dmas"></param>
        /// <param name="index"></param>
        /// <returns>_res</returns>
        public static int[,] DeleteColumnIntoMas(int[,] dmas, int index)
        {
            int[,] newdmas = new int[dmas.GetLength(0), dmas.GetLength(1) - 1];           
            for (int i = 0; i < dmas.GetLength(0); i++)
            {
                int nj = -1;
                for (int j = 0; j < dmas.GetLength(1); j++)
                {
                    nj++;
                    if (j == index)
                    {
                        nj--;
                        continue;
                    }
                    newdmas[i, nj] = dmas[i, j];
                }
            }
            return newdmas;
        }/// <summary>
        /// Используется при изменении значения ячейки   
        /// </summary>
        /// <param name="dmas"></param>
        public static void ReserveTable(int[,] dmas)
        {
            if (_wascancel)//Проверка на резервирование после отмены действий (используется для фиксации отката и возможности выполнения отмены при новых значениях                           
            {//не затрагивая старые)
                _cancelledchanges = new Stack<int[,]>();
                _wascancel = false;
            }
            _reservedtable.Push((int[,])dmas.Clone());//Внесение клона массива, чтобы не было ссылки на него при сохранении данных

        }/// <summary>
         /// Отмена изменений таблицы
         /// </summary>
         /// <returns>_res</returns>
        public static DataTable Undo()
        {
            if (_reservedtable.Count > 1)
            {                
                _cancelledchanges.Push(_reservedtable.Pop());
                _needreserve = false;
                _wascancel = true;
                return ToDataTable(_reservedtable.Peek());
            }
            return _res;
        }
        /// <summary>
        /// Отмена возврата изменений
        /// </summary>
        /// <returns>_res</returns>
        public static DataTable Redo()
        {
            if (_cancelledchanges.Count > 0)
            {                                 
                if(_cancelledchanges.Count == 1) _wascancel = false;
                _needreserve = false;
                _reservedtable.Push(_cancelledchanges.Peek());
                return ToDataTable(_cancelledchanges.Pop());
            }
            return _res;
        }/// <summary>
        /// Очистка значений UndoAndRedo
        /// </summary>
        public static void ClearUndoAndRedo()
        {
            _cancelledchanges = new Stack<int[,]>();
            _reservedtable = new Stack<int[,]>();
            _wascancel = false;
        }
    }
}
