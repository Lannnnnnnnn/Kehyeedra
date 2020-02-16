using System;

namespace Kehyeedra3
{
    public static class Extensions
    {
        private static DateTime YeedraTime = new DateTime(2020, 2, 2, 0, 0, 0, DateTimeKind.Utc);

        public static ulong ToYeedraStamp(this DateTime time)
            => (ulong)(time.Subtract(YeedraTime)).TotalSeconds;

        public static DateTime FromYeedraStamp(this ulong time)
            => YeedraTime.AddSeconds(Convert.ToDouble(time));
    }
}