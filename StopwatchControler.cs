using System;
using System.Windows;
using System.Windows.Media;

namespace Mind_fastMath
{
    /// <summary>
    /// Алгоритмы, находящиеся в классе вычисляют
    /// время, за которое пользователь решает пример
    /// </summary>
    class StopwatchControler
    {
        private static int timerCount = 0;

        private static int startMinute;
        private static int startSecond;
        private static int startMillisecond;

        private static int endMinute;
        private static int endSecond;
        private static int endMillisecond;

        public static System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();
        private static Window _windowCopy = Application.Current.MainWindow;

        public static string GetCountedTimeInSC()
        {
            // Берем текущее время и вычитаем его из времени на момент
            // таска и умножаем для получения миллисекунд, переводим в float
            // и получаем строку в формате "секунды,миллисекунды".
            // Условные операторы необходимы для случаев обнуления часа

            int _endMinuteToMs = endMinute * 60 * 1000;
            int _endSecondToMs = endSecond * 1000 + endMillisecond;

            int _startMinuteToMs = startMinute * 60 * 1000;
            int _startSecondToMs = startSecond * 1000 + startMillisecond;

            try
            {
                if(endMinute == 0 || startMinute == 0)
                {
                    return (float)(
                        60 + _endMinuteToMs - _startMinuteToMs
                        + (_endSecondToMs - _startSecondToMs)) / 1000 + "s if";
                }
                else
                {
                    return (float)(
                        (_endMinuteToMs % _startMinuteToMs) // 5min % 4min = 1min = 60000ms
                        + (_endSecondToMs - _startSecondToMs)) / 1000 + "s"; // 2sec - 55sec = -53000ms
                        // 60000ms + (-53000ms) = 7sec past
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return (float)(_endSecondToMs - _startSecondToMs) / 1000 + "s exc";
            }
        }

        // Время на момент создания задачи
        public static void SetStartTime()
        {
            startMillisecond = DateTime.UtcNow.Millisecond;
            startSecond = DateTime.UtcNow.Second;
            startMinute = DateTime.UtcNow.Minute;

            timerCount = 0;
        }

        // Время на момент проверки поля
        public static void SetEndTime()
        {
            endMillisecond = DateTime.UtcNow.Millisecond;
            endSecond = DateTime.UtcNow.Second;
            endMinute = DateTime.UtcNow.Minute;

            ChangeTaskForegroundHEX("#ffffff");
        }

        public static void tickRegestration(object sender, EventArgs e) 
        {
            int _step = 2 + (_windowCopy as MainWindow).ComboBoxDiff.SelectedIndex;

            if (timerCount == _step)
            {
                ChangeTaskForegroundHEX("#8eff4d");
            }
            if (timerCount == _step * 2)
            {
                ChangeTaskForegroundHEX("#f9ff4d");
            }
            if (timerCount == _step * 4)
            {
                ChangeTaskForegroundHEX("#ff4d4d");
            }
        
            timerCount++;
        }

        private static void ChangeTaskForegroundHEX(string HEXcode) 
        {
            (_windowCopy as MainWindow).taskLabel.Foreground = (Brush)new BrushConverter().ConvertFrom(HEXcode);
        }
    }
}