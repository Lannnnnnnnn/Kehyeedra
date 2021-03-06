using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Kehyeedra3
{
    public static class Extensions
    {
        private static DateTime YeedraTime = new DateTime(2020, 2, 2, 0, 0, 0, DateTimeKind.Utc);

        public static ulong ToYeedraStamp(this DateTime time)
            => (ulong)(time.Subtract(YeedraTime)).TotalSeconds;

        public static DateTime FromYeedraStamp(this ulong time)
            => YeedraTime.AddSeconds(Convert.ToDouble(time));

        public static string ToYeedraDisplay(this long number)
        {
            double numb = (double)number/10000;
            return numb.ToString("N4");
        }
        

        //https://stackoverflow.com/a/1262619
        public static void Shuffle<T>(this IList<T> list)
        {
            using (RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider())
            {
                int n = list.Count;
                while (n > 1)
                {
                    byte[] box = new byte[1];
                    do provider.GetBytes(box);
                    while (!(box[0] < n * (byte.MaxValue / n)));
                    int k = (box[0] % n);
                    n--;
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        }
    }
}