﻿using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
            InitializeComponent();
            // Изначальные параметры окна ->
            mwin.Background = (Brush)new BrushConverter().ConvertFrom("#202226");
            mwin.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#202226");

            Header.Background = (Brush)new BrushConverter().ConvertFrom("#2b2e34");

            userInField.Background = (Brush)new BrushConverter().ConvertFrom("#2b2e34");
            userInField.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#2b2e34");

            sendResult.Background = (Brush)new BrushConverter().ConvertFrom("#2b2e34");
            sendResult.BorderBrush = (Brush)new BrushConverter().ConvertFrom("#2b2e34");

            resultLabel.Foreground = (Brush)new BrushConverter().ConvertFrom("#e6e6e7");
            // <-
        }

        private void userInField_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key.ToString() == "Return") 
            {
                TaskPatternCreator.AnswerCheck();
            }
        }

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

        private void WinMouseDown(object sender, MouseButtonEventArgs e) => DragMove();

        private void sendResult_Click(object sender, RoutedEventArgs e) => TaskPatternCreator.AnswerCheck();

        private void ComboBoxItem_Selected(object sender, RoutedEventArgs e) => TaskPatternCreator.CreateTask();
    }
}