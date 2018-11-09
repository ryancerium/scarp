using System;
using Newtonsoft.Json;

namespace Scarp.Primitive {
    [JsonConverter(typeof(PrimitiveJsonConverter))]
    public struct Int<Tag> : IComparable, IComparable<Int<Tag>>, IEquatable<Int<Tag>>, IFormattable {
        public Int(int value) => Value = value;

        public int Value {
            get; private set;
        }

        public override string ToString() => Value.ToString();

        public string ToString(string format, IFormatProvider formatProvider) => Value.ToString(format, formatProvider);

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj) => obj is Int<Tag> other && Equals((Int<Tag>) other);

        public bool Equals(Int<Tag> other) => Value.CompareTo(other.Value) == 0;

        public int CompareTo(object obj) => (obj is Int<Tag> other) ? Value.CompareTo(other.Value) : 1;

        public int CompareTo(Int<Tag> other) => Value.CompareTo(other.Value);

        public static implicit operator Int<Tag>(int value) => new Int<Tag>(value);
        //public static implicit operator int(Int<Tag> value) => value.Value;

        #region Unary Operators

        public static Int<Tag> operator -(Int<Tag> value) => new Int<Tag>(-value.Value);
        public static Int<Tag> operator ~(Int<Tag> value) => new Int<Tag>(~value.Value);
        public static Int<Tag> operator ++(Int<Tag> value) => new Int<Tag>(++value.Value);
        public static Int<Tag> operator --(Int<Tag> value) => new Int<Tag>(--value.Value);

        #endregion

        #region Binary Operators

        public static Int<Tag> operator +(Int<Tag> lhs, Int<Tag> rhs) => new Int<Tag>(lhs.Value + rhs.Value);
        public static Int<Tag> operator -(Int<Tag> lhs, Int<Tag> rhs) => new Int<Tag>(lhs.Value - rhs.Value);
        public static Int<Tag> operator *(Int<Tag> lhs, Int<Tag> rhs) => new Int<Tag>(lhs.Value * rhs.Value);
        public static Int<Tag> operator /(Int<Tag> lhs, Int<Tag> rhs) => new Int<Tag>(lhs.Value / rhs.Value);
        public static Int<Tag> operator %(Int<Tag> lhs, Int<Tag> rhs) => new Int<Tag>(lhs.Value % rhs.Value);
        public static Int<Tag> operator |(Int<Tag> lhs, Int<Tag> rhs) => new Int<Tag>(lhs.Value | rhs.Value);
        public static Int<Tag> operator &(Int<Tag> lhs, Int<Tag> rhs) => new Int<Tag>(lhs.Value & rhs.Value);
        public static Int<Tag> operator ^(Int<Tag> lhs, Int<Tag> rhs) => new Int<Tag>(lhs.Value ^ rhs.Value);

        #endregion

        #region Comparison Operators

        public static bool operator ==(Int<Tag> lhs, Int<Tag> rhs) => lhs.Equals(rhs);
        public static bool operator !=(Int<Tag> lhs, Int<Tag> rhs) => !lhs.Equals(rhs);
        public static bool operator <(Int<Tag> lhs, Int<Tag> rhs) => lhs.Value < rhs.Value;
        public static bool operator >(Int<Tag> lhs, Int<Tag> rhs) => lhs.Value > rhs.Value;
        public static bool operator <=(Int<Tag> lhs, Int<Tag> rhs) => lhs.Value <= rhs.Value;
        public static bool operator >=(Int<Tag> lhs, Int<Tag> rhs) => lhs.Value >= rhs.Value;

        #endregion
    }
}