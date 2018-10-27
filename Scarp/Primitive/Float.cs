using System;

namespace Scarp.Primitive {
    public struct Float<Tag> : IComparable, IComparable<Float<Tag>>, IEquatable<Float<Tag>>, IFormattable {
        public Float(float value) => Value = value;

        public float Value {
            get; private set;
        }

        public override string ToString() => Value.ToString();

        public string ToString(string format, IFormatProvider formatProvider) => Value.ToString(format, formatProvider);

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj) => obj is Float<Tag> other && Equals((Float<Tag>) other);

        public bool Equals(Float<Tag> other) => Value.CompareTo(other.Value) == 0;

        public int CompareTo(object obj) => (obj is Float<Tag> other) ? Value.CompareTo(other.Value) : 1;

        public int CompareTo(Float<Tag> other) => Value.CompareTo(other.Value);

        public static implicit operator Float<Tag>(float value) => new Float<Tag>(value);
        //public static implicit operator float(Float<Tag> value) => value.Value;

        #region Unary Operators

        public static Float<Tag> operator -(Float<Tag> value) => new Float<Tag>(-value.Value);
        public static Float<Tag> operator ++(Float<Tag> value) => new Float<Tag>(++value.Value);
        public static Float<Tag> operator --(Float<Tag> value) => new Float<Tag>(--value.Value);

        #endregion

        #region Binary Operators

        public static Float<Tag> operator +(Float<Tag> lhs, Float<Tag> rhs) => new Float<Tag>(lhs.Value + rhs.Value);
        public static Float<Tag> operator -(Float<Tag> lhs, Float<Tag> rhs) => new Float<Tag>(lhs.Value - rhs.Value);
        public static Float<Tag> operator *(Float<Tag> lhs, Float<Tag> rhs) => new Float<Tag>(lhs.Value * rhs.Value);
        public static Float<Tag> operator /(Float<Tag> lhs, Float<Tag> rhs) => new Float<Tag>(lhs.Value / rhs.Value);
        public static Float<Tag> operator %(Float<Tag> lhs, Float<Tag> rhs) => new Float<Tag>(lhs.Value % rhs.Value);

        #endregion

        #region Comparison Operators

        public static bool operator ==(Float<Tag> lhs, Float<Tag> rhs) => lhs.Equals(rhs);
        public static bool operator !=(Float<Tag> lhs, Float<Tag> rhs) => !lhs.Equals(rhs);
        public static bool operator <(Float<Tag> lhs, Float<Tag> rhs) => lhs.Value < rhs.Value;
        public static bool operator >(Float<Tag> lhs, Float<Tag> rhs) => lhs.Value > rhs.Value;
        public static bool operator <=(Float<Tag> lhs, Float<Tag> rhs) => lhs.Value <= rhs.Value;
        public static bool operator >=(Float<Tag> lhs, Float<Tag> rhs) => lhs.Value >= rhs.Value;

        #endregion
    }
}