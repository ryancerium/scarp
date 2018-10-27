using System;

namespace Scarp.Primitive {
    public struct Decimal<Tag> : IComparable, IComparable<Decimal<Tag>>, IEquatable<Decimal<Tag>>, IFormattable {
        public Decimal(decimal value) => Value = value;

        public decimal Value {
            get; private set;
        }

        public override string ToString() => Value.ToString();

        public string ToString(string format, IFormatProvider formatProvider) => Value.ToString(format, formatProvider);

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj) => obj is Decimal<Tag> other && Equals((Decimal<Tag>) other);

        public bool Equals(Decimal<Tag> other) => Value.CompareTo(other.Value) == 0;

        public int CompareTo(object obj) => (obj is Decimal<Tag> other) ? Value.CompareTo(other.Value) : 1;

        public int CompareTo(Decimal<Tag> other) => Value.CompareTo(other.Value);

        public static implicit operator Decimal<Tag>(decimal value) => new Decimal<Tag>(value);
        //public static implicit operator decimal(Decimal<Tag> value) => value.Value;

        #region Unary Operators

        public static Decimal<Tag> operator -(Decimal<Tag> value) => new Decimal<Tag>(-value.Value);
        public static Decimal<Tag> operator ++(Decimal<Tag> value) => new Decimal<Tag>(++value.Value);
        public static Decimal<Tag> operator --(Decimal<Tag> value) => new Decimal<Tag>(--value.Value);

        #endregion

        #region Binary Operators

        public static Decimal<Tag> operator +(Decimal<Tag> lhs, Decimal<Tag> rhs) => new Decimal<Tag>(lhs.Value + rhs.Value);
        public static Decimal<Tag> operator -(Decimal<Tag> lhs, Decimal<Tag> rhs) => new Decimal<Tag>(lhs.Value - rhs.Value);
        public static Decimal<Tag> operator *(Decimal<Tag> lhs, Decimal<Tag> rhs) => new Decimal<Tag>(lhs.Value * rhs.Value);
        public static Decimal<Tag> operator /(Decimal<Tag> lhs, Decimal<Tag> rhs) => new Decimal<Tag>(lhs.Value / rhs.Value);
        public static Decimal<Tag> operator %(Decimal<Tag> lhs, Decimal<Tag> rhs) => new Decimal<Tag>(lhs.Value % rhs.Value);

        #endregion

        #region Comparison Operators

        public static bool operator ==(Decimal<Tag> lhs, Decimal<Tag> rhs) => lhs.Equals(rhs);
        public static bool operator !=(Decimal<Tag> lhs, Decimal<Tag> rhs) => !lhs.Equals(rhs);
        public static bool operator <(Decimal<Tag> lhs, Decimal<Tag> rhs) => lhs.Value < rhs.Value;
        public static bool operator >(Decimal<Tag> lhs, Decimal<Tag> rhs) => lhs.Value > rhs.Value;
        public static bool operator <=(Decimal<Tag> lhs, Decimal<Tag> rhs) => lhs.Value <= rhs.Value;
        public static bool operator >=(Decimal<Tag> lhs, Decimal<Tag> rhs) => lhs.Value >= rhs.Value;

        #endregion
    }
}