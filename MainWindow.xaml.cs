using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using System.Threading;

namespace Mind_fastMath
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /*
         * При инициализации программы идет изначальный выбор сложности(по дефолту медиум)
         * и срабатывает функция при выборе, которая вызывает создание таска.
         * AnswerCheck - исходя из названия проверяет правильность решения
         * CreateTask - создает таск и меняет параметры окна
         * Все функции, отвечающие за математические задачи используют объекты классе MainWindow
         * В будущем исправить
         */
        public MainWindow()
        {
            // Инициализация для полей класа TaskPatternCreator
            TaskPatternCreator.INIT();

            //
            InitializeComponent();

            // Изначальные параметры окна
            Header.Background       = (Brush)new BrushConverter().ConvertFrom("#2b2e34");
            mwin.Background         = (Brush)new BrushConverter().ConvertFrom("#202226");
            mwin.BorderBrush        = (Brush)new BrushConverter().ConvertFrom("#202226");

            userInField.Background  = (Brush)new BrushConverter().ConvertFrom("#2b2e34");
            userInField.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#2b2e34");

            sendResult.Background   = (Brush)new BrushConverter().ConvertFrom("#2b2e34");
            sendResult.BorderBrush  = (Brush)new BrushConverter().ConvertFrom("#2b2e34");

            resultLabel.Foreground  = (Brush)new BrushConverter().ConvertFrom("#e6e6e7");

            // Задание параметров для таймера и его запуск как отдельного потока
            StopwatchControler.timer.Tick += new EventHandler(StopwatchControler.tickRegestration);
            StopwatchControler.timer.Interval = new TimeSpan(0, 0, 0, 0, 1000);
            StopwatchControler.timer.Start();
        }

        //
        private void ExitFromProgram(object sender, RoutedEventArgs e)
        {
            try
            {
                Environment.Exit(0); // Завершить с кодом 0
            }
            catch (Exception exc)
            {
                MessageBox.Show("Программа завершилась некорректно: \n" + exc.Message);
            }
        }

        private void CheckStateOfComboBox() 
        {
            // Из-за слишком быстрого запуска программы ComboBox'ы не
            // успевают прогрузиться и выдают null при запросе
            if (ComboBoxDiff != null && ComboBoxTypeOf != null)
            {
                TaskPatternCreator.CreateTask();
            }
        }
        //

        //
        private void WinMouseDown(object sender, MouseButtonEventArgs e)        => DragMove();

        private void sendResult_Click(object sender, RoutedEventArgs e)         => TaskPatternCreator.AnswerCheck();

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e)    => CheckStateOfComboBox();

        private void ComboBoxTypeOf_Selected(object sender, RoutedEventArgs e)  => CheckStateOfComboBox();

        private void userInField_KeyDown(object sender, KeyEventArgs e)         { if (e.Key.ToString() == "Return") TaskPatternCreator.AnswerCheck(); }
        //
    }
}