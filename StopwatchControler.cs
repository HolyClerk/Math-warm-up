using System;
using System.Windows;

namespace Mind_fastMath
{
    /// <summary>
    /// Алгоритмы, находящиеся в классе вычисляют
    /// время, за которое пользователь решает пример
    /// </summary>
    class StopwatchControler
    {
        private static int _sMinute;
        private static int _sSecond;
        private static int _sMillisecond;

        private static int _eMinute;
        private static int _eSecond;
        private static int _eMillisecond;

        public static string passedTimeInTimer()
        {
            // Берем текущее время и вычитаем его из времени на момент
            // таска и умножаем для получения миллисекунд, переводим в флоат
            // и получаем строку в формате "секунды,миллисекунды".
            // Условные операторы необходимы для случаев обнуления часа

            int _endMinuteToMs = _eMinute * 60 * 1000;
            int _endSecondToMs = _eSecond * 1000 + _eMillisecond;

            int _startMinuteToMs = _sMinute * 60 * 1000;
            int _startSecondToMs = _sSecond * 1000 + _sMillisecond;

            try
            {
                if(_eMinute == 0 || _sMinute == 0)
                {
                    return (float)(
                        60 + (_endMinuteToMs - _startMinuteToMs) 
                        + (_endMinuteToMs - _endSecondToMs)) / 1000 + "s";
                }
                else
                {
                    return (float)(
                        (_endMinuteToMs % _startMinuteToMs)
                        + (_endSecondToMs - _startSecondToMs)) / 1000 + "s";
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());
                return (float)(_endSecondToMs - _startSecondToMs) / 1000 + "s";
            }
            
        }

        // Время на момент создания задачи
        public static void SetNowTime()
        {
            _sMillisecond = DateTime.UtcNow.Millisecond;
            _sSecond = DateTime.UtcNow.Second;
            _sMinute = DateTime.UtcNow.Minute;
        }

        // Время на момент проверки поля
        public static void SetEndTime()
        {
            _eMillisecond = DateTime.UtcNow.Millisecond;
            _eSecond = DateTime.UtcNow.Second;
            _eMinute = DateTime.UtcNow.Minute;
        }
    }
}