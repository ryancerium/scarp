using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Newtonsoft.Json;

namespace Scarp.Primitive {
    [JsonConverter(typeof(PrimitiveJsonConverter))]
    public partial struct String<Tag> : IEnumerable<char>, IEnumerable, ICloneable, IComparable, IComparable<String<Tag>>, IEquatable<String<Tag>> {
        public string Value { get; }

        private int Length => Value.Length;

        public String(string s) => Value = s;

        public String(char[] chars) => Value = new string(chars);

        public override string ToString() => Value;

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj) =>
            object.ReferenceEquals(obj, Value) || (obj is String<Tag> other && Equals(other));

        public static implicit operator String<Tag>(string s) => new String<Tag>(s);

        public static bool operator ==(String<Tag> lhs, String<Tag> rhs) => lhs.Value == rhs.Value;

        public static bool operator !=(String<Tag> lhs, String<Tag> rhs) => lhs.Value != rhs.Value;

        public static String<Tag> operator +(String<Tag> lhs, String<Tag> rhs) => lhs.Value + rhs.Value;

        public char this[int i] => Value[i];

        #region Interface Implementations
        IEnumerator<char> IEnumerable<char>.GetEnumerator() => Value.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => Value.GetEnumerator();

        public object Clone() => new String<Tag>(Value);

        public int CompareTo(object obj) => (obj is String<Tag> other) ? Value.CompareTo(other.Value) : 1;

        public int CompareTo(String<Tag> other) => Value.CompareTo(other.Value);

        public TypeCode GetTypeCode() => Value.GetTypeCode();

        public string ToString(IFormatProvider provider) => Value.ToString(provider);

        public bool Equals(String<Tag> other) {
            if (object.ReferenceEquals(Value, other.Value)) {
                return true;
            }

            if (Value == null || other.Value == null) {
                return false;
            }

            return Value.Equals(other.Value);
        }

        #endregion

        #region String Member Methods

        public bool EndsWith(String<Tag> value) => Value.EndsWith(value.Value);

        public bool EndsWith(String<Tag> value, StringComparison comparisonType) => Value.EndsWith(value.Value, comparisonType);

        public bool EndsWith(String<Tag> value, bool ignoreCase, CultureInfo culture) => Value.EndsWith(value.Value, ignoreCase, culture);

        public bool Equals(String<Tag> value, StringComparison comparisonType) => Value.Equals(value.Value, comparisonType);

        public bool StartsWith(String<Tag> value) => Value.StartsWith(value.Value);

        public bool StartsWith(String<Tag> value, StringComparison comparisonType) => Value.StartsWith(value.Value, comparisonType);

        public bool StartsWith(String<Tag> value, bool ignoreCase, CultureInfo culture) => Value.StartsWith(value.Value, ignoreCase, culture);

        public String<Tag> Insert(int startIndex, String<Tag> value) => Value.Insert(startIndex, value.Value);

        public String<Tag> Replace(String<Tag> oldValue, String<Tag> newValue, bool ignoreCase, CultureInfo culture) => Value.Replace(oldValue.Value, newValue.Value, ignoreCase, culture);

        public String<Tag> Replace(String<Tag> oldValue, String<Tag> newValue, StringComparison comparisonType) => Value.Replace(oldValue.Value, newValue.Value, comparisonType);

        public String<Tag> Replace(String<Tag> oldValue, String<Tag> newValue) => Value.Replace(oldValue.Value, newValue.Value);

        public String<Tag>[] Split(String<Tag> separator, StringSplitOptions options) => Value.Split(separator.Value, options).ToStringArray<Tag>();

        public String<Tag>[] Split(String<Tag> separator, int count, StringSplitOptions options) => Value.Split(separator.Value, count, options).ToStringArray<Tag>();

        public String<Tag>[] Split(String<Tag>[] separator, StringSplitOptions options) => Value.Split(separator.ToStringArray(), options).ToStringArray<Tag>();

        public String<Tag>[] Split(String<Tag>[] separator, int count, StringSplitOptions options) => Value.Split(separator.ToStringArray(), count, options).ToStringArray<Tag>();

        public bool Contains(String<Tag> value) => Value.Contains(value.Value);

        public bool Contains(String<Tag> value, StringComparison comparisonType) => Value.Contains(value.Value, comparisonType);

        public int IndexOf(String<Tag> value) => Value.IndexOf(value.Value);

        public int IndexOf(String<Tag> value, int startIndex) => Value.IndexOf(value.Value, startIndex);

        public int IndexOf(String<Tag> value, int startIndex, int count) => Value.IndexOf(value.Value, startIndex, count);

        public int IndexOf(String<Tag> value, StringComparison comparisonType) => Value.IndexOf(value.Value, comparisonType);

        public int IndexOf(String<Tag> value, int startIndex, StringComparison comparisonType) => Value.IndexOf(value.Value, startIndex, comparisonType);

        public int IndexOf(String<Tag> value, int startIndex, int count, StringComparison comparisonType) => Value.IndexOf(value.Value, startIndex, count, comparisonType);

        public int LastIndexOf(String<Tag> value) => Value.LastIndexOf(value.Value);

        public int LastIndexOf(String<Tag> value, int startIndex) => Value.LastIndexOf(value.Value, startIndex);

        public int LastIndexOf(String<Tag> value, int startIndex, int count) => Value.LastIndexOf(value.Value, startIndex, count);

        public int LastIndexOf(String<Tag> value, StringComparison comparisonType) => Value.LastIndexOf(value.Value, comparisonType);

        public int LastIndexOf(String<Tag> value, int startIndex, StringComparison comparisonType) => Value.LastIndexOf(value.Value, startIndex, comparisonType);

        public int LastIndexOf(String<Tag> value, int startIndex, int count, StringComparison comparisonType) => Value.LastIndexOf(value.Value, startIndex, count, comparisonType);

        #endregion
    }

    internal static class StrExt {
        public static String<Tag>[] ToStringArray<Tag>(this string[] strings) {
            String<Tag>[] tagStrings = new String<Tag>[strings.Length];
            for (int i = 0; i < strings.Length; ++i) {
                tagStrings[i] = new String<Tag>(strings[i]);
            }
            return tagStrings;
        }

        public static string[] ToStringArray<Tag>(this String<Tag>[] oldTagStrings) {
            string[] strings = new string[oldTagStrings.Length];
            for (int i = 0; i < oldTagStrings.Length; ++i) {
                strings[i] = oldTagStrings[i].Value;
            }
            return strings;
        }
    }
}