﻿using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using LibraryFinderFirstEven;
using LibraryFinderSum;
using LibraryFinderDominance;
using LibraryFinderDifferentColumns;
using VisualTable;

namespace Practical_Work
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
		private void CheckFirstNumber_Click(object sender, RoutedEventArgs e)
		{
			FirstTask.Focus();
			try
			{
				if (ThreeNumber.Text.Length != 3) throw new Exception("Length Is Not 3");//Проверка на трехзначность числа
				int number = Convert.ToInt32(ThreeNumber.Text)/100;//Выделение первой цифры
				bool checkNumber = ClassFinderFirstEven.FindFirstEven(Convert.ToInt32(ThreeNumber.Text));//Обращение к методу, разработанному по заданию
				if (checkNumber) FirstNumber.Text = $"Первая цифра - {number}, является четной";//Вывод результата
				else FirstNumber.Text = $"Первая цифра - {number}, не является четной";
			}
			catch (FormatException)//Исключение, происходящее при безуспешной конвертации
			{
				MessageBox.Show("Некорректно введено значение, необходимо число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				ThreeNumber.Focus();
			}
			catch //Используется для недостаточного ввода количества цифр в поле
			{
				MessageBox.Show("Некорректно введено значение, необходимо трехзначное число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);	
				ThreeNumber.Focus();
			}
		}
		private void FirstTask_TextChanged(object sender, TextChangedEventArgs e)
		{
			FirstNumber.Clear(); 
		}
		private void FindSumOfMultiple3_Click(object sender, RoutedEventArgs e)
		{
			SecondTask.Focus();
			int firstValue, secondValue, thirdValue;//Описание 3-х чисел
			try//Проверка на некорректность ввода для отдельного поля и перевод фокуса на него
			{
				firstValue = Convert.ToInt32(FirstValue.Text);
			}
			catch
			{
				MessageBox.Show("Некорректно введено значение первого числа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				FirstValue.Focus();
				return;
			}
			try
			{
				secondValue = Convert.ToInt32(SecondValue.Text); 
			}
			catch
			{
				MessageBox.Show("Некорректно введено значение второго числа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				SecondValue.Focus();
				return;
			}
			try
			{
				thirdValue = Convert.ToInt32(ThirdValue.Text);
			}
			catch
			{
				MessageBox.Show("Некорректно введено значение третьего числа!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				ThirdValue.Focus();
				return;
			}
			SumOfMultiple3.Text = ClassFinderSumOfMultiple.FindSumOfMultiple3(firstValue, secondValue, thirdValue).ToString();//Обращение к методу по заданию и вывод результата в поле
		}
		private void SecondTask_TextChanged(object sender, TextChangedEventArgs e)
		{
			SumOfMultiple3.Clear();
		}
		int[] _firstMas;//Два массива по заданию
		int[] _secondMas;
		private void BothFill_Click(object sender, RoutedEventArgs e)
		{
			int n, range;//Описание размера массивов и диапазона соответственно
			ThirdTask.Focus();
			try
			{
				n = Convert.ToInt32(LengthForBothMas.Text);
				if (n < 0) throw new Exception();
			}
			catch
			{
				MessageBox.Show("Некорректно введено значение размера массивов! Нужно число больше или равно 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				LengthForBothMas.Focus();
				return;
			}
			try
			{
				range = Convert.ToInt32(RandomRangeForBothMas.Text);
			}
			catch
			{
				MessageAboutErrorRange();
				RandomRangeForBothMas.Focus();
				return;
			}
			try
			{
				if(range < 0) throw new Exception("Range Less 0");
			}
			catch
			{
				MessageAboutRangeLess0();
				RandomRangeForBothMas.Focus();
				return;
			}
			_firstMas = new int[n];
			_secondMas = new int[n];
			Random rnd = new Random();
			for(int i = 0; i < n; i++)//Заполнение
			{
				_firstMas[i] = rnd.Next(range);
				_secondMas[i] = rnd.Next(range);
			}
			FirstMas.ItemsSource = VisualArray.ToDataTable(_firstMas).DefaultView;
			SecondMas.ItemsSource = VisualArray.ToDataTable(_secondMas).DefaultView;
			DominantCellCount.Clear();
		}
		private void FindDominance_Click(object sender, RoutedEventArgs e)
		{
			ThirdTask.Focus();//Фокус, который используется для перевода на вкладку ThirdTask при нажатии кнопки в меню
			DominantCellCount.Text = ClassFinderDominanceForTwoMas.FindCellCountMoreSecondMas(in _firstMas, in _secondMas).ToString();
		}
		int[,] _arr;//Двумерный массив для таблицы
		private void ArrFill_Click(object sender, RoutedEventArgs e)
		{
			ForthTask.Focus();
			int n, m, range;
			try
			{
				m = Convert.ToInt32(ColumnCount.Text);
				if (m < 0) throw new Exception();
			}
			catch
			{
				MessageBox.Show("Некорректно введено значение столбцов! Нужно число больше или равно 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				ColumnCount.Focus();
				return;
			}
			try
			{
				n = Convert.ToInt32(RowCount.Text);
				if (n < 0) throw new Exception();
			}
			catch
			{
				MessageBox.Show("Некорректно введено значение строк! Нужно число больше или равно 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
				RowCount.Focus();
				return;
			}	
			try
			{
				range = Convert.ToInt32(RandomRangeForArr.Text);
			}
			catch
			{
				MessageAboutErrorRange();
				RandomRangeForArr.Focus();
				return;
			}
			try
			{
				if(range < 0) throw new Exception("Range Less 0");
			}
			catch
			{
				MessageAboutRangeLess0();
				RandomRangeForArr.Focus();
				return;
			}
			_arr = new int[n,m];
			Random rnd = new Random();
			for(int i = 0; i < n; i++)
			{
				for(int j = 0; j < m; j++)
				{
					_arr[i,j] = rnd.Next(range);
				}
			}
			VisualArray.ClearUndoAndRedo();
			Arr.ItemsSource = VisualArray.ToDataTable(_arr).DefaultView;			
			DifferentColumnCount.Clear();

		}
		private void MessageAboutErrorRange()
		{
			MessageBox.Show("Некорректно введено значение диапазона!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		private void MessageAboutRangeLess0()
		{
			MessageBox.Show("Диапазон должен быть больше или равен 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		private void FindDifferentColumnCount_Click(object sender, RoutedEventArgs e)
		{	
			ForthTask.Focus();		
			DifferentColumnCount.Text = ClassFinderDifferentColumnCount.FindDifferentColumnCount(_arr).ToString();
		}
		private void BothClear_Click(object sender, RoutedEventArgs e)
		{
			ThirdTask.Focus();
			FirstMas.ItemsSource = SecondMas.ItemsSource = _firstMas  = _secondMas = null;			
			DominantCellCount.Clear();			
		}	
		private void ArrClear_Click(object sender, RoutedEventArgs e)
		{
			ForthTask.Focus();
			Arr.ItemsSource = _arr = null;
			UndoM.IsEnabled = UndoCM.IsEnabled = RedoM.IsEnabled = RedoCM.IsEnabled = false;
			DifferentColumnCount.Clear();
			VisualArray.ClearUndoAndRedo();
		}
		private void AboutProgram_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Лопаткин Сергей ИСП-31. Задание по учебной практике.\n" +
				"№1.Ввести трехзначное число. Определить: начинается ли оно четной цифрой\n" +
				"№2.Ввести три целых числа. Найти сумму тех чисел, которые кратны 3\n" +
				"№3.Заданы два массива. Проверить, сколько элементов первого массива превосходят соответствующие элементы второго массива\n" +
				"№4.Дана целочисленная матрица M * N. Найти количество ее столбцов, все элементы которых различны","О программе", MessageBoxButton.OK, MessageBoxImage.Information);
		}
		private void Help_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Включены следующие особенности в программу:\n" +
			"1) Размер строк для размеров массивов и таблицы - 2\n" +
			"2) Размер строк для обычных значений - 6 (Кроме первого задания - 3)", "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
		}
        private void AddColumn_Click(object sender, RoutedEventArgs e)
        {
			if (ActionsForArr.IsEnabled == true)//Проверка используется для возможности использования горячих клавиш (не работает добавление, если элемент в меню отключен)
			{
				Arr.ItemsSource = VisualArray.AddNewColumn(ref _arr).DefaultView;
				EnableUndo();
			}
        }

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
			if (ActionsForArr.IsEnabled == true)
			{
				Arr.ItemsSource = VisualArray.AddNewRow(ref _arr).DefaultView;
				EnableUndo();
			}
        }

        private void DelColumn_Click(object sender, RoutedEventArgs e)
        {		
			if (ActionsForArr.IsEnabled == true)
			{		
				if (Arr.CurrentCell.Column != null) //Проверка для исключения ошибки
				{
					if (Arr.CurrentCell.Column.DisplayIndex != -1)
					{
						Arr.ItemsSource = VisualArray.DeleteColumn(ref _arr, Arr.CurrentCell.Column.DisplayIndex).DefaultView;
						EnableUndo();
					}
					else MessageBox.Show("Для удаления столбца необходимо его выбрать!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
				}
				else MessageBox.Show("Для удаления столбца необходимо его выбрать!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);				
			}
        }

        private void DelRow_Click(object sender, RoutedEventArgs e)
        {
			if (ActionsForArr.IsEnabled == true)
			{
				if (Arr.SelectedIndex == -1) MessageBox.Show("Для удаления строки необходимо ее выбрать!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Warning);
				else
				{
					Arr.ItemsSource = VisualArray.DeleteRow(ref _arr, Arr.SelectedIndex).DefaultView;
					EnableUndo();
				}
			}
        }
		string cell;//Переменная для хранения резервного значения на случай кривого ввода пользователя
		private void FirstMas_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
		{
			int iColumn = e.Column.DisplayIndex;
			DataRowView row = (DataRowView)FirstMas.Items[0];
			cell = row[iColumn].ToString();
		}
        private void SecondMas_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
			int iColumn = e.Column.DisplayIndex;
			DataRowView row = (DataRowView)SecondMas.Items[0];
			cell = row[iColumn].ToString();
		}
		private void Arr_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
			int iColumn = e.Column.DisplayIndex;
			int iRow = e.Row.GetIndex();
			DataRowView row = (DataRowView)Arr.Items[iRow];
			cell = row[iColumn].ToString();
		}
        private void FirstMas_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
			if (int.TryParse(((TextBox)e.EditingElement).Text, out int currentCell))
			{
				int iColumn = e.Column.DisplayIndex;
				_firstMas[iColumn] = currentCell;
				DominantCellCount.Clear();
			}
			else ((TextBox)e.EditingElement).Text = cell;
		}

        private void SecondMas_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
			if (int.TryParse(((TextBox)e.EditingElement).Text, out int currentCell))
			{
				int iColumn = e.Column.DisplayIndex;
				_secondMas[iColumn] = currentCell;
				DominantCellCount.Clear();
			}
			else ((TextBox)e.EditingElement).Text = cell;
		}
		private void Arr_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
		{
			if (int.TryParse(((TextBox)e.EditingElement).Text, out int currentCell))
			{
				int iColumn = e.Column.DisplayIndex;
				int iRow = e.Row.GetIndex();
				_arr[iRow, iColumn] = currentCell;
				VisualArray.ReserveTable(_arr);
				DifferentColumnCount.Clear();
				EnableUndo();				
			}
			else ((TextBox)e.EditingElement).Text = cell;
		}
		private void Undo_Click(object sender, RoutedEventArgs e)
		{
			if (ActionsForArr.IsEnabled == true)
			{
				Arr.ItemsSource = VisualArray.Undo().DefaultView;
				_arr = VisualArray.SyncData();
				DifferentColumnCount.Clear();
				if (VisualArray.ReservedTable.Count == 1) UndoM.IsEnabled = UndoCM.IsEnabled = false;
				if (VisualArray.CancelledChanges.Count > 0) RedoM.IsEnabled = RedoCM.IsEnabled = true;
			}
		}
		private void Redo_Click(object sender, RoutedEventArgs e)
		{
			if (ActionsForArr.IsEnabled == true)
			{
				Arr.ItemsSource = VisualArray.Redo().DefaultView;
				_arr = VisualArray.SyncData();
				DifferentColumnCount.Clear();
				if (VisualArray.CancelledChanges.Count == 0) RedoM.IsEnabled = RedoCM.IsEnabled = false;
				if (VisualArray.ReservedTable.Count > 1) UndoM.IsEnabled = UndoCM.IsEnabled = true;
			}
		}/// <summary>
		/// Используется для включения отмены, если произошли изменения таблицы 
		/// </summary>
		private void EnableUndo()
		{
			UndoM.IsEnabled = UndoCM.IsEnabled = true;
		}
		private void ForthTask_GotFocus(object sender, RoutedEventArgs e)
		{
			ActionsForArr.IsEnabled = true;
			MinWidth = 600;
			MinHeight = 450;
			SizeToContent = SizeToContent.Manual;//Используется для изменения размера под "контент", чтобы при создании таблицы не было растягивания на все мониторы
		}
		private void ForthTask_LostFocus(object sender, RoutedEventArgs e)
		{
			ActionsForArr.IsEnabled = false;
		}

        private void FirstTask_GotFocus(object sender, RoutedEventArgs e)//Использется для изменения минимума (в соответствии с логическим представлением программы)
        {
			MinWidth = 440;
			MinHeight = 170;
		}

        private void SecondTask_GotFocus(object sender, RoutedEventArgs e)
        {
			MinWidth = 440;
			MinHeight = 250;
        }

        private void ThirdTask_GotFocus(object sender, RoutedEventArgs e)
        {
			MinWidth = 600;
			MinHeight = 450;
			SizeToContent = SizeToContent.Manual;
		}
    }
}
