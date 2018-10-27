using System;

namespace Scarp.Primitive {
    public struct Double<Tag> : IComparable, IComparable<Double<Tag>>, IEquatable<Double<Tag>>, IFormattable {
        public Double(double value) => Value = value;

        public double Value {
            get; private set;
        }

        public override string ToString() => Value.ToString();

        public string ToString(string format, IFormatProvider formatProvider) => Value.ToString(format, formatProvider);

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj) => obj is Double<Tag> other && Equals((Double<Tag>) other);

        public bool Equals(Double<Tag> other) => Value.CompareTo(other.Value) == 0;

        public int CompareTo(object obj) => (obj is Double<Tag> other) ? Value.CompareTo(other.Value) : 1;

        public int CompareTo(Double<Tag> other) => Value.CompareTo(other.Value);

        public static implicit operator Double<Tag>(double value) => new Double<Tag>(value);
        //public static implicit operator double(Double<Tag> value) => value.Value;

        #region Unary Operators

        public static Double<Tag> operator -(Double<Tag> value) => new Double<Tag>(-value.Value);
        public static Double<Tag> operator ++(Double<Tag> value) => new Double<Tag>(++value.Value);
        public static Double<Tag> operator --(Double<Tag> value) => new Double<Tag>(--value.Value);

        #endregion

        #region Binary Operators

        public static Double<Tag> operator +(Double<Tag> lhs, Double<Tag> rhs) => new Double<Tag>(lhs.Value + rhs.Value);
        public static Double<Tag> operator -(Double<Tag> lhs, Double<Tag> rhs) => new Double<Tag>(lhs.Value - rhs.Value);
        public static Double<Tag> operator *(Double<Tag> lhs, Double<Tag> rhs) => new Double<Tag>(lhs.Value * rhs.Value);
        public static Double<Tag> operator /(Double<Tag> lhs, Double<Tag> rhs) => new Double<Tag>(lhs.Value / rhs.Value);
        public static Double<Tag> operator %(Double<Tag> lhs, Double<Tag> rhs) => new Double<Tag>(lhs.Value % rhs.Value);

        #endregion

        #region Comparison Operators

        public static bool operator ==(Double<Tag> lhs, Double<Tag> rhs) => lhs.Equals(rhs);
        public static bool operator !=(Double<Tag> lhs, Double<Tag> rhs) => !lhs.Equals(rhs);
        public static bool operator <(Double<Tag> lhs, Double<Tag> rhs) => lhs.Value < rhs.Value;
        public static bool operator >(Double<Tag> lhs, Double<Tag> rhs) => lhs.Value > rhs.Value;
        public static bool operator <=(Double<Tag> lhs, Double<Tag> rhs) => lhs.Value <= rhs.Value;
        public static bool operator >=(Double<Tag> lhs, Double<Tag> rhs) => lhs.Value >= rhs.Value;

        #endregion
    }
}