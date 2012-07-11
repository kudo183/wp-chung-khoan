using System;

namespace PhoneApp1
{
    public static class StopWatch
    {
        private static long _prevTick = DateTime.Now.Ticks;
        private static long _currentTick = 0;

        public static TimeSpan TimeSpanFromPreviousCall()
        {
            _currentTick = DateTime.Now.Ticks;
            var result = new TimeSpan(_currentTick - _prevTick);
            _prevTick = _currentTick;
            return result;
        }
    }
}
