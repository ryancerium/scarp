using System;
using Newtonsoft.Json;

namespace Scarp.Primitive {
    [JsonConverter(typeof(PrimitiveJsonConverter))]
    public struct UInt<Tag> : IComparable, IComparable<UInt<Tag>>, IEquatable<UInt<Tag>>, IFormattable {
        public UInt(uint value) => Value = value;

        public uint Value {
            get; private set;
        }

        public override string ToString() => Value.ToString();

        public string ToString(string format, IFormatProvider formatProvider) => Value.ToString(format, formatProvider);

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj) => obj is UInt<Tag> other && Equals((UInt<Tag>) other);

        public bool Equals(UInt<Tag> other) => Value.CompareTo(other.Value) == 0;

        public int CompareTo(object obj) => (obj is UInt<Tag> other) ? Value.CompareTo(other.Value) : 1;

        public int CompareTo(UInt<Tag> other) => Value.CompareTo(other.Value);

        public static implicit operator UInt<Tag>(uint value) => new UInt<Tag>(value);
        //public static implicit operator uint(UInt<Tag> value) => value.Value;

        #region Unary Operators

        public static UInt<Tag> operator ~(UInt<Tag> value) => new UInt<Tag>(~value.Value);
        public static UInt<Tag> operator ++(UInt<Tag> value) => new UInt<Tag>(++value.Value);
        public static UInt<Tag> operator --(UInt<Tag> value) => new UInt<Tag>(--value.Value);

        #endregion

        #region Binary Operators

        public static UInt<Tag> operator +(UInt<Tag> lhs, UInt<Tag> rhs) => new UInt<Tag>(lhs.Value + rhs.Value);
        public static UInt<Tag> operator -(UInt<Tag> lhs, UInt<Tag> rhs) => new UInt<Tag>(lhs.Value - rhs.Value);
        public static UInt<Tag> operator *(UInt<Tag> lhs, UInt<Tag> rhs) => new UInt<Tag>(lhs.Value * rhs.Value);
        public static UInt<Tag> operator /(UInt<Tag> lhs, UInt<Tag> rhs) => new UInt<Tag>(lhs.Value / rhs.Value);
        public static UInt<Tag> operator %(UInt<Tag> lhs, UInt<Tag> rhs) => new UInt<Tag>(lhs.Value % rhs.Value);
        public static UInt<Tag> operator |(UInt<Tag> lhs, UInt<Tag> rhs) => new UInt<Tag>(lhs.Value | rhs.Value);
        public static UInt<Tag> operator &(UInt<Tag> lhs, UInt<Tag> rhs) => new UInt<Tag>(lhs.Value & rhs.Value);
        public static UInt<Tag> operator ^(UInt<Tag> lhs, UInt<Tag> rhs) => new UInt<Tag>(lhs.Value ^ rhs.Value);

        #endregion

        #region Comparison Operators

        public static bool operator ==(UInt<Tag> lhs, UInt<Tag> rhs) => lhs.Equals(rhs);
        public static bool operator !=(UInt<Tag> lhs, UInt<Tag> rhs) => !lhs.Equals(rhs);
        public static bool operator <(UInt<Tag> lhs, UInt<Tag> rhs) => lhs.Value < rhs.Value;
        public static bool operator >(UInt<Tag> lhs, UInt<Tag> rhs) => lhs.Value > rhs.Value;
        public static bool operator <=(UInt<Tag> lhs, UInt<Tag> rhs) => lhs.Value <= rhs.Value;
        public static bool operator >=(UInt<Tag> lhs, UInt<Tag> rhs) => lhs.Value >= rhs.Value;

        #endregion
    }
}