using System;

namespace Scarp.Tests {
    public static class StringExt {
        public static string SubstringMaxLength(this string s, int startIndex, int maxLength) {
            return s.Substring(startIndex, Math.Min(maxLength, s.Length - startIndex));
        }
    }
}