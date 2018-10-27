using System.Linq;

namespace Scarp.Tests {
    public static class Random {
        private static readonly System.Random source = new System.Random();
        private static readonly string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890+/";

        /// <summary>
        /// Returns a random string that contains {paramref name="length"} characters.
        /// </summary>
        public static string String(int length) {
            char[] s = new char[length];
            for (int i = 0; i < s.Length; ++i) {
                s[i] = chars[source.Next(chars.Length)];
            }
            return new string(s);
        }

        /// <summary>
        /// Returns a random string that contains 10 characters.
        /// </summary>
        public static string String10() => String(10);

        /// <summary>
        /// Returns a random string that contains 20 characters.
        /// </summary>
        public static string String20() => String(20);

        public static int Int() => source.Next(1024) + 1;

        public static uint UInt() => (uint) source.Next(1024) + 1;

        public static long Long() => Int();

        public static ulong ULong() => UInt();

        public static float Float() => (float) source.NextDouble();

        public static double Double() => source.NextDouble();

        public static decimal Decimal() => (decimal) source.NextDouble();
    }
}