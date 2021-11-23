using System;
using System.Media;
using System.Windows.Media;
using System.Windows;
using System.Threading;

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

        // Возвращает правильный ответ в зависимости
        // от выбранного типа задачи(сложение, умножение и т.д.)
        private static double firstNumber;
        private static double secondNumber;

        private static int _Sminute;
        private static int _Ssecond;
        private static int _Smillisecond;

        private static int _Eminute;
        private static int _Esecond;
        private static int _Emillisecond;

        // Правильный ответ вычисляется только в момент
        // считывания ответа пользователя
        // Ответ высчитывается в зависимости от
        // типа задачи, выставленной в боксе с ними
        private static double trueAnswer
        {
            get
            {
                var actualWindow = (_windowCopy as MainWindow);

                return actualWindow.ComboBoxTypeOf.SelectedIndex switch
                {
                    0 => firstNumber + secondNumber,
                    1 => firstNumber - secondNumber,
                    2 => firstNumber * secondNumber,
                    3 => (double)(firstNumber / secondNumber),
                    _ => firstNumber + secondNumber,
                };
            }
        }


        public static void CreateTask()
        {
            var rand = new Random();
            var actualWindow = (_windowCopy as MainWindow);

            // Индекс комбо-бокса идет от легкого к сложному(от 0 до 3).
            // 10 возводим в степень индекса+1
            // Благодаря этому мы макс. число для примера,
            // на основе которого можно сделать и мин. число
            int difNum = Convert.ToInt32(Math.Pow(10, actualWindow.ComboBoxDiff.SelectedIndex + 1));

            firstNumber = rand.Next(difNum / 10, difNum);
            secondNumber = rand.Next(difNum / 10, difNum);

            // Обновление данных окна, на основе генерированных
            // чисел и выбранных позиций в комбо-боксе
            char op = actualWindow.ComboBoxTypeOf.SelectedIndex switch
            {
                0 => '+',
                1 => '-',
                2 => '*',
                3 => '/',
                _ => '+',
            };

            actualWindow.taskLabel.Content = $"{firstNumber} {op} {secondNumber}";
            actualWindow.userInField.Text = "";

            SetNowTime();
        }


        public static void AnswerCheck()
        {
            var actualWindow = (_windowCopy as MainWindow);

            _ = double.TryParse(actualWindow.userInField.Text, out double userAnswer);

            if (Math.Round(userAnswer, 1) == Math.Round(trueAnswer, 1))
            {
                PlayCorrectSound();
                SetEndTime();

                // При переполнении текста происходит ОБНУЛЕНИЕ СРОКОВ ПУТИНА
                if (actualWindow.stopwatchBlock.Text.Length > 34)
                    actualWindow.stopwatchBlock.Text = "";

                actualWindow.resultLabel.Foreground = (Brush)new BrushConverter().ConvertFrom("#73ef6d");
                actualWindow.resultLabel.Content = "Верно!";
                actualWindow.stopwatchBlock.Text += passedTimeInTimer() + "\n";

                CreateTask();
            }
            else
            {
                actualWindow.resultLabel.Foreground = (Brush)new BrushConverter().ConvertFrom("#ea102b");
                actualWindow.resultLabel.Content = "Не верно!";
            }
        }


        private static string passedTimeInTimer()
        {
            // Берем текущее время и вычитаем его из времени на момент
            // таска и умножаем для получения миллисекунд, переводим в флоат
            // и получаем строку в формате "секунды,миллисекунды".
            // Условные операторы необходимы для случаев обнуления часа

            if (_Eminute < _Sminute)
            {
                return (float)(
                    (_Eminute + _Sminute) * 60 * 1000 
                    + (_Esecond * 1000 + _Emillisecond) 
                    - (_Ssecond * 1000 - _Smillisecond)) / 1000 + "ms";
            }
            else 
            {
                return (float)(
                    (_Eminute % _Sminute) * 60 * 1000 
                    + (_Esecond * 1000 + _Emillisecond) 
                    - (_Ssecond * 1000 - _Smillisecond)) / 1000 + "ms";
            }
        }


        // Время на момент создания задачи
        private static void SetNowTime() 
        {
            _Smillisecond = DateTime.UtcNow.Millisecond;
            _Ssecond = DateTime.UtcNow.Second;
            _Sminute = DateTime.UtcNow.Minute;
        }


        // Время на момент проверки поля
        private static void SetEndTime()
        {
            _Emillisecond = DateTime.UtcNow.Millisecond;
            _Esecond = DateTime.UtcNow.Second;
            _Eminute = DateTime.UtcNow.Minute;
        }


        private static void PlayCorrectSound()
        {
            SoundPlayer SP = new SoundPlayer();
            SP.SoundLocation = "C:\\Users\\User\\Desktop\\correct.wav";
            SP.LoadAsync();
            SP.Play();
        }
    }
}