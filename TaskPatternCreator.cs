using System;
using System.Media;
using System.Windows.Media;
using System.Windows;

namespace Mind_fastMath
{
    class TaskPatternCreator
    {
        /*
         * В функциях создается переменная actualWindow,
         * которая используется так, словно мы напрямую обращаемся к используемому
         * окну благодаря использованию _windowCopy как MainWindow !Исправить\Укоротить!
         */

        private static Window _windowCopy = Application.Current.MainWindow;

        private static int firstNumber;

        private static int secondNumber;

        /// <summary>
        /// Создает случайные примеры в зависимости от сложности, установленной в комбо-боксе.
        /// </summary>
        public static void CreateTask()
        {
            var actualWindow = (_windowCopy as MainWindow);

            // нумерация = индекс в комбо-боксе сложностей(от легкого к сложному)
            int difNum = actualWindow.ComboBoxDiff.SelectedIndex switch
            {
                0 => 10,

                1 => 100,

                2 => 1000,

                3 => 10000,

                _ => 1000,
            };

            // Рандомизация чисел в след. примере + обновление данных окна
            var rand = new Random();

            firstNumber = rand.Next(difNum / 10, difNum);
            secondNumber = rand.Next(difNum / 10, difNum);

            actualWindow.taskLabel.Content = $"{firstNumber} + {secondNumber}";
            actualWindow.userInField.Text = "";
        }

        /// <summary>
        /// Проверяет поле ввода пользователя и создает новые задачи,
        /// внося их в объекты из главного окна. 
        /// </summary>
        public static bool AnswerCheck()
        {
            var actualWindow = (_windowCopy as MainWindow);

            _ = int.TryParse(actualWindow.userInField.Text, out int userAnswer);

            if (userAnswer == firstNumber + secondNumber)
            {
                actualWindow.resultLabel.Foreground = (Brush)new BrushConverter().ConvertFrom("#73ef6d");
                actualWindow.resultLabel.Content = "Верно!";

                PlayCorrectSound();
                CreateTask();

                return true;
            }

            actualWindow.resultLabel.Foreground = (Brush)new BrushConverter().ConvertFrom("#ea102b");
            actualWindow.resultLabel.Content = "Не верно!";

            return false;
        }

        public static void PlayCorrectSound()
        {
            SoundPlayer SP = new SoundPlayer();
            SP.SoundLocation = "C:\\Users\\User\\Desktop\\correct.wav";
            SP.LoadAsync();
            SP.Play();
        }
    }
}