using System.Windows.Input;

namespace Practical_Work
{/// <summary>
/// Используется для горячих клавиш таблицы
/// </summary>
		public static class TableCommands
		{
			static TableCommands()
			{
				InputGestureCollection undo = new InputGestureCollection()
			{
				new KeyGesture(Key.Z, ModifierKeys.Control, "Ctrl+Z"),
			};
				Undo = new RoutedUICommand("CommandScalePlus", "MainCommands", typeof(TableCommands), undo);
			InputGestureCollection redo = new InputGestureCollection()
			{
				new KeyGesture(Key.Y, ModifierKeys.Control, "Ctrl+Y"),
			};
				InputGesture secondKeysRedo = new KeyGesture(Key.Z, ModifierKeys.Control | ModifierKeys.Shift);
				redo.Add(secondKeysRedo);
				Redo = new RoutedCommand("Redo", typeof(TableCommands), redo);
				InputGestureCollection addColumn = new InputGestureCollection()
			{
				new KeyGesture(Key.A|Key.C, ModifierKeys.Alt, "Alt+A+C"),
			};
				AddColumn = new RoutedCommand("AddColumn", typeof(TableCommands), addColumn);
				InputGestureCollection addRow = new InputGestureCollection()
			{
				new KeyGesture(Key.A|Key.R, ModifierKeys.Alt, "Alt+A+R"),
			};
				AddRow = new RoutedCommand("AddRow", typeof(TableCommands), addRow);
				InputGestureCollection delColumn = new InputGestureCollection()
			{
				new KeyGesture(Key.D|Key.C, ModifierKeys.Alt, "Alt+D+C"),
			};
				DelColumn = new RoutedCommand("DelColumn", typeof(TableCommands), delColumn);
				InputGestureCollection delRow = new InputGestureCollection()
			{
				new KeyGesture(Key.D|Key.R, ModifierKeys.Alt, "Alt+D+R"),
			};
				DelRow = new RoutedCommand("DelRow", typeof(TableCommands), delRow);
			}
			public static RoutedCommand Undo { get; private set; }
			public static RoutedCommand Redo { get; private set; }
			public static RoutedCommand AddColumn { get; private set; }
			public static RoutedCommand AddRow { get; private set; }
			public static RoutedCommand DelColumn { get; private set; }
			public static RoutedCommand DelRow { get; private set; }
		}
}

