using System;
using System.Globalization;
using System.Text;

namespace Scarp.Primitive {
    public partial struct String<Tag> {
        public void CopyTo(int sourceIndex, Char[] destination, int destinationIndex, int count) => Value.CopyTo(sourceIndex, destination, destinationIndex, count);
        public Char[] ToCharArray() => Value.ToCharArray();
        public Char[] ToCharArray(int startIndex, int length) => Value.ToCharArray(startIndex, length);
        public CharEnumerator GetEnumerator() => Value.GetEnumerator();
        public bool IsNormalized() => Value.IsNormalized();
        public bool IsNormalized(NormalizationForm normalizationForm) => Value.IsNormalized(normalizationForm);
        public String<Tag> Normalize() => Value.Normalize();
        public String<Tag> Normalize(NormalizationForm normalizationForm) => Value.Normalize(normalizationForm);
        public bool EndsWith(string value) => Value.EndsWith(value);
        public bool EndsWith(string value, StringComparison comparisonType) => Value.EndsWith(value, comparisonType);
        public bool EndsWith(string value, bool ignoreCase, CultureInfo culture) => Value.EndsWith(value, ignoreCase, culture);
        public bool EndsWith(char value) => Value.EndsWith(value);
        public bool Equals(string value, StringComparison comparisonType) => Value.Equals(value, comparisonType);
        public int GetHashCode(StringComparison comparisonType) => Value.GetHashCode(comparisonType);
        public bool StartsWith(string value) => Value.StartsWith(value);
        public bool StartsWith(string value, StringComparison comparisonType) => Value.StartsWith(value, comparisonType);
        public bool StartsWith(string value, bool ignoreCase, CultureInfo culture) => Value.StartsWith(value, ignoreCase, culture);
        public bool StartsWith(char value) => Value.StartsWith(value);
        public String<Tag> Insert(int startIndex, string value) => Value.Insert(startIndex, value);
        public String<Tag> PadLeft(int totalWidth) => Value.PadLeft(totalWidth);
        public String<Tag> PadLeft(int totalWidth, char paddingChar) => Value.PadLeft(totalWidth, paddingChar);
        public String<Tag> PadRight(int totalWidth) => Value.PadRight(totalWidth);
        public String<Tag> PadRight(int totalWidth, char paddingChar) => Value.PadRight(totalWidth, paddingChar);
        public String<Tag> Remove(int startIndex, int count) => Value.Remove(startIndex, count);
        public String<Tag> Remove(int startIndex) => Value.Remove(startIndex);
        public String<Tag> Replace(string oldValue, string newValue, bool ignoreCase, CultureInfo culture) => Value.Replace(oldValue, newValue, ignoreCase, culture);
        public String<Tag> Replace(string oldValue, string newValue, StringComparison comparisonType) => Value.Replace(oldValue, newValue, comparisonType);
        public String<Tag> Replace(char oldChar, char newChar) => Value.Replace(oldChar, newChar);
        public String<Tag> Replace(string oldValue, string newValue) => Value.Replace(oldValue, newValue);
        public String<Tag>[] Split(char separator, StringSplitOptions options) => Value.Split(separator, options).ToStringArray<Tag>();
        public String<Tag>[] Split(char separator, int count, StringSplitOptions options) => Value.Split(separator, count, options).ToStringArray<Tag>();
        public String<Tag>[] Split(Char[] separator) => Value.Split(separator).ToStringArray<Tag>();
        public String<Tag>[] Split(Char[] separator, int count) => Value.Split(separator, count).ToStringArray<Tag>();
        public String<Tag>[] Split(Char[] separator, StringSplitOptions options) => Value.Split(separator, options).ToStringArray<Tag>();
        public String<Tag>[] Split(Char[] separator, int count, StringSplitOptions options) => Value.Split(separator, count, options).ToStringArray<Tag>();
        public String<Tag>[] Split(string separator, StringSplitOptions options) => Value.Split(separator, options).ToStringArray<Tag>();
        public String<Tag>[] Split(string separator, int count, StringSplitOptions options) => Value.Split(separator, count, options).ToStringArray<Tag>();
        public String<Tag>[] Split(string[] separator, StringSplitOptions options) => Value.Split(separator, options).ToStringArray<Tag>();
        public String<Tag>[] Split(string[] separator, int count, StringSplitOptions options) => Value.Split(separator, count, options).ToStringArray<Tag>();
        public String<Tag> Substring(int startIndex) => Value.Substring(startIndex);
        public String<Tag> Substring(int startIndex, int length) => Value.Substring(startIndex, length);
        public String<Tag> ToLower() => Value.ToLower();
        public String<Tag> ToLower(CultureInfo culture) => Value.ToLower(culture);
        public String<Tag> ToLowerInvariant() => Value.ToLowerInvariant();
        public String<Tag> ToUpper() => Value.ToUpper();
        public String<Tag> ToUpper(CultureInfo culture) => Value.ToUpper(culture);
        public String<Tag> ToUpperInvariant() => Value.ToUpperInvariant();
        public String<Tag> Trim() => Value.Trim();
        public String<Tag> Trim(char trimChar) => Value.Trim(trimChar);
        public String<Tag> Trim(Char[] trimChars) => Value.Trim(trimChars);
        public String<Tag> TrimStart() => Value.TrimStart();
        public String<Tag> TrimStart(char trimChar) => Value.TrimStart(trimChar);
        public String<Tag> TrimStart(Char[] trimChars) => Value.TrimStart(trimChars);
        public String<Tag> TrimEnd() => Value.TrimEnd();
        public String<Tag> TrimEnd(char trimChar) => Value.TrimEnd(trimChar);
        public String<Tag> TrimEnd(Char[] trimChars) => Value.TrimEnd(trimChars);
        public bool Contains(string value) => Value.Contains(value);
        public bool Contains(string value, StringComparison comparisonType) => Value.Contains(value, comparisonType);
        public bool Contains(char value) => Value.Contains(value);
        public bool Contains(char value, StringComparison comparisonType) => Value.Contains(value, comparisonType);
        public int IndexOf(char value) => Value.IndexOf(value);
        public int IndexOf(char value, int startIndex) => Value.IndexOf(value, startIndex);
        public int IndexOf(char value, StringComparison comparisonType) => Value.IndexOf(value, comparisonType);
        public int IndexOf(char value, int startIndex, int count) => Value.IndexOf(value, startIndex, count);
        public int IndexOfAny(Char[] anyOf) => Value.IndexOfAny(anyOf);
        public int IndexOfAny(Char[] anyOf, int startIndex) => Value.IndexOfAny(anyOf, startIndex);
        public int IndexOfAny(Char[] anyOf, int startIndex, int count) => Value.IndexOfAny(anyOf, startIndex, count);
        public int IndexOf(string value) => Value.IndexOf(value);
        public int IndexOf(string value, int startIndex) => Value.IndexOf(value, startIndex);
        public int IndexOf(string value, int startIndex, int count) => Value.IndexOf(value, startIndex, count);
        public int IndexOf(string value, StringComparison comparisonType) => Value.IndexOf(value, comparisonType);
        public int IndexOf(string value, int startIndex, StringComparison comparisonType) => Value.IndexOf(value, startIndex, comparisonType);
        public int IndexOf(string value, int startIndex, int count, StringComparison comparisonType) => Value.IndexOf(value, startIndex, count, comparisonType);
        public int LastIndexOf(char value) => Value.LastIndexOf(value);
        public int LastIndexOf(char value, int startIndex) => Value.LastIndexOf(value, startIndex);
        public int LastIndexOf(char value, int startIndex, int count) => Value.LastIndexOf(value, startIndex, count);
        public int LastIndexOfAny(Char[] anyOf) => Value.LastIndexOfAny(anyOf);
        public int LastIndexOfAny(Char[] anyOf, int startIndex) => Value.LastIndexOfAny(anyOf, startIndex);
        public int LastIndexOfAny(Char[] anyOf, int startIndex, int count) => Value.LastIndexOfAny(anyOf, startIndex, count);
        public int LastIndexOf(string value) => Value.LastIndexOf(value);
        public int LastIndexOf(string value, int startIndex) => Value.LastIndexOf(value, startIndex);
        public int LastIndexOf(string value, int startIndex, int count) => Value.LastIndexOf(value, startIndex, count);
        public int LastIndexOf(string value, StringComparison comparisonType) => Value.LastIndexOf(value, comparisonType);
        public int LastIndexOf(string value, int startIndex, StringComparison comparisonType) => Value.LastIndexOf(value, startIndex, comparisonType);
        public int LastIndexOf(string value, int startIndex, int count, StringComparison comparisonType) => Value.LastIndexOf(value, startIndex, count, comparisonType);
    }
}