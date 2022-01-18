using System;
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
			if(ThreeNumber.Text.Length != 3) throw new Exception("Length Is Not 3");
			int number = Convert.ToInt32(ThreeNumber.Text)/100;
			bool checkNumber = ClassFinderFirstEven.FindFirstEven(Convert.ToInt32(ThreeNumber.Text));		
			if(checkNumber) FirstNumber.Text = $"Первая цифра - {number}, является четной";
			else FirstNumber.Text = $"Первая цифра - {number}, не является четной";
		}
		catch (FormatException)
		{
			MessageBox.Show("Некорректно введено значение, необходимо число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		catch
		{
			MessageBox.Show("Некорректно введено значение, необходимо трехзначное число!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);	
		}
	}
	private void FirstTask_TextChanged(object sender, TextChangedEventArgs e)
	{
		FirstNumber.Clear(); 
	}
	private void FindSumOfMultiple3_Click(object sender, RoutedEventArgs e)
	{
		SecondTask.Focus();
		try
		{
			SumOfMultiple3.Text = ClassFinderSumOfMultiple.FindSumOfMultiple3(Convert.ToInt32(FirstValue.Text),Convert.ToInt32(SecondValue.Text), Convert.ToInt32(ThirdValue.Text)).ToString();
		}
		catch
		{
			MessageBox.Show("Необходимы числа для проведения подсчета суммы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
		}
	}
	private void SecondTask_TextChanged(object sender, TextChangedEventArgs e)
	{
		SumOfMultiple3.Clear();
	}
	int[] _firstMas;
	int[] _secondMas;
	private void BothFill_Click(object sender, RoutedEventArgs e)
	{
		ThirdTask.Focus();
		try
		{
			int n = Convert.ToInt32(LengthForBothMas.Text);
			int range = Convert.ToInt32(RandomRangeForBothMas.Text);
			if(range < 0) throw new Exception("Range Less 0");
			_firstMas = new int[n];
			_secondMas = new int[n];
			Random rnd = new Random();
			for(int i = 0; i < n; i++)
			{
				_firstMas[i] = rnd.Next(range);
				_secondMas[i] = rnd.Next(range);
			}
			FirstMas.ItemsSource = VisualArray.ToDataTable(_firstMas).DefaultView;
			SecondMas.ItemsSource = VisualArray.ToDataTable(_secondMas).DefaultView;
			DominantCellCount.Clear();
		}
		catch(FormatException)
		{
			MessageBox.Show("Для правильного заполнения массивов необходимо ввести корректные значения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		catch
		{
			MessageAboutRangeLess0();
		}
	}
	private void FindDominance_Click(object sender, RoutedEventArgs e)
	{
		ThirdTask.Focus();
		DominantCellCount.Text = ClassFinderDominanceForTwoMas.FindCellCountMoreSecondMas(in _firstMas, in _secondMas).ToString();
	}
	int[,] _matrix;
	private void MatrixFill_Click(object sender, RoutedEventArgs e)
	{
		ForthTask.Focus();
		try
		{
			int n = Convert.ToInt32(RowCount.Text);
			int m = Convert.ToInt32(ColumnCount.Text);			
			int range = Convert.ToInt32(RandomRangeForMatrix.Text);
			if(range < 0) throw new Exception("Range Less 0");
			_matrix = new int[n,m];
			Random rnd = new Random();
			for(int i = 0; i < n; i++)
			{
				for(int j = 0; j < m; j++)
				{
					_matrix[i,j] = rnd.Next(range);
				}
			}
			Matrix.ItemsSource = VisualArray.ToDataTable(_matrix).DefaultView;			
			DifferentColumnCount.Clear();
		}
		catch(FormatException)
		{
			MessageBox.Show("Для правильного заполнения таблицы необходимо ввести корректные значения!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
		}
		catch
		{
			MessageAboutRangeLess0();
		}
	}
	private void MessageAboutRangeLess0()
	{
		MessageBox.Show("Диапазон должен быть больше или равен 0", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
	}
	private void FindDifferentColumnCount_Click(object sender, RoutedEventArgs e)
	{	
		ForthTask.Focus();		
		DifferentColumnCount.Text = ClassFinderDifferentColumnCount.FindDifferentColumnCount(_matrix).ToString();
	}
	private void BothClear_Click(object sender, RoutedEventArgs e)
	{
		FirstMas.ItemsSource = _firstMas = null;
		SecondMas.ItemsSource = _secondMas = null;
		DominantCellCount.Clear();
	}	
	private void MatrixClear_Click(object sender, RoutedEventArgs e)
	{
		Matrix.ItemsSource = _matrix = null;
		DifferentColumnCount.Clear();
	}
	private void AboutProgram_Click(object sender, RoutedEventArgs e)
	{
		MessageBox.Show("Лопаткин Сергей ИСП-31. Задание по учебной практике.","О программе", MessageBoxButton.OK, MessageBoxImage.Information);
	}
	private void Help_Click(object sender, RoutedEventArgs e)
	{
		MessageBox.Show("Включены следующие особенности в программу:\n" +
		"1) Размер строк для размеров массивов и таблицы - 2\n" +
		"2) Размер строк для обычных значений - 6 (Кроме первого задания - 3)\n" +
		"3) Клавиши заполнения таблицы и массивов не были внесены в меню из-за отсутствия надобности", "Справка", MessageBoxButton.OK, MessageBoxImage.Information);
	}
        private void AddColumn_Click(object sender, RoutedEventArgs e)
        {
			VisualArray.AddNewColumn(ref _matrix);
        }

        private void AddRow_Click(object sender, RoutedEventArgs e)
        {
			VisualArray.AddNewRow(ref _matrix);
        }

        private void DelColumn_Click(object sender, RoutedEventArgs e)
        {
			VisualArray.DeleteColumn(ref _matrix, Matrix.CurrentCell.Column.DisplayIndex);
        }

        private void DelRow_Click(object sender, RoutedEventArgs e)
        {
			VisualArray.DeleteRow(ref _matrix, Matrix.SelectedIndex);
        }
    }
}
