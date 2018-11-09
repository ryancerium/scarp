using System;
using Newtonsoft.Json;

namespace Scarp.Primitive {
    [JsonConverter(typeof(PrimitiveJsonConverter))]
    public struct ULong<Tag> : IComparable, IComparable<ULong<Tag>>, IEquatable<ULong<Tag>>, IFormattable {
        public ULong(ulong value) => Value = value;

        public ulong Value {
            get; private set;
        }

        public override string ToString() => Value.ToString();

        public string ToString(string format, IFormatProvider formatProvider) => Value.ToString(format, formatProvider);

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj) => obj is ULong<Tag> other && Equals((ULong<Tag>) other);

        public bool Equals(ULong<Tag> other) => Value.CompareTo(other.Value) == 0;

        public int CompareTo(object obj) => (obj is ULong<Tag> other) ? Value.CompareTo(other.Value) : 1;

        public int CompareTo(ULong<Tag> other) => Value.CompareTo(other.Value);

        public static implicit operator ULong<Tag>(ulong value) => new ULong<Tag>(value);
        //public static implicit operator ulong(ULong<Tag> value) => value.Value;

        #region Unary Operators

        public static ULong<Tag> operator ~(ULong<Tag> value) => new ULong<Tag>(~value.Value);
        public static ULong<Tag> operator ++(ULong<Tag> value) => new ULong<Tag>(++value.Value);
        public static ULong<Tag> operator --(ULong<Tag> value) => new ULong<Tag>(--value.Value);

        #endregion

        #region Binary Operators

        public static ULong<Tag> operator +(ULong<Tag> lhs, ULong<Tag> rhs) => new ULong<Tag>(lhs.Value + rhs.Value);
        public static ULong<Tag> operator -(ULong<Tag> lhs, ULong<Tag> rhs) => new ULong<Tag>(lhs.Value - rhs.Value);
        public static ULong<Tag> operator *(ULong<Tag> lhs, ULong<Tag> rhs) => new ULong<Tag>(lhs.Value * rhs.Value);
        public static ULong<Tag> operator /(ULong<Tag> lhs, ULong<Tag> rhs) => new ULong<Tag>(lhs.Value / rhs.Value);
        public static ULong<Tag> operator %(ULong<Tag> lhs, ULong<Tag> rhs) => new ULong<Tag>(lhs.Value % rhs.Value);
        public static ULong<Tag> operator |(ULong<Tag> lhs, ULong<Tag> rhs) => new ULong<Tag>(lhs.Value | rhs.Value);
        public static ULong<Tag> operator &(ULong<Tag> lhs, ULong<Tag> rhs) => new ULong<Tag>(lhs.Value & rhs.Value);
        public static ULong<Tag> operator ^(ULong<Tag> lhs, ULong<Tag> rhs) => new ULong<Tag>(lhs.Value ^ rhs.Value);

        #endregion

        #region Comparison Operators

        public static bool operator ==(ULong<Tag> lhs, ULong<Tag> rhs) => lhs.Equals(rhs);
        public static bool operator !=(ULong<Tag> lhs, ULong<Tag> rhs) => !lhs.Equals(rhs);
        public static bool operator <(ULong<Tag> lhs, ULong<Tag> rhs) => lhs.Value < rhs.Value;
        public static bool operator >(ULong<Tag> lhs, ULong<Tag> rhs) => lhs.Value > rhs.Value;
        public static bool operator <=(ULong<Tag> lhs, ULong<Tag> rhs) => lhs.Value <= rhs.Value;
        public static bool operator >=(ULong<Tag> lhs, ULong<Tag> rhs) => lhs.Value >= rhs.Value;

        #endregion
    }
}