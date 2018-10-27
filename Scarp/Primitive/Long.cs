using System;

namespace Scarp.Primitive {
    public struct Long<Tag> : IComparable, IComparable<Long<Tag>>, IEquatable<Long<Tag>>, IFormattable {
        public Long(long value) => Value = value;

        public long Value {
            get; private set;
        }

        public override string ToString() => Value.ToString();

        public string ToString(string format, IFormatProvider formatProvider) => Value.ToString(format, formatProvider);

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object obj) => obj is Long<Tag> other && Equals((Long<Tag>) other);

        public bool Equals(Long<Tag> other) => Value.CompareTo(other.Value) == 0;

        public int CompareTo(object obj) => (obj is Long<Tag> other) ? Value.CompareTo(other.Value) : 1;

        public int CompareTo(Long<Tag> other) => Value.CompareTo(other.Value);

        public static implicit operator Long<Tag>(long value) => new Long<Tag>(value);
        //public static implicit operator long(Long<Tag> value) => value.Value;

        #region Unary Operators

        public static Long<Tag> operator -(Long<Tag> value) => new Long<Tag>(-value.Value);
        public static Long<Tag> operator ~(Long<Tag> value) => new Long<Tag>(~value.Value);
        public static Long<Tag> operator ++(Long<Tag> value) => new Long<Tag>(++value.Value);
        public static Long<Tag> operator --(Long<Tag> value) => new Long<Tag>(--value.Value);

        #endregion

        #region Binary Operators

        public static Long<Tag> operator +(Long<Tag> lhs, Long<Tag> rhs) => new Long<Tag>(lhs.Value + rhs.Value);
        public static Long<Tag> operator -(Long<Tag> lhs, Long<Tag> rhs) => new Long<Tag>(lhs.Value - rhs.Value);
        public static Long<Tag> operator *(Long<Tag> lhs, Long<Tag> rhs) => new Long<Tag>(lhs.Value * rhs.Value);
        public static Long<Tag> operator /(Long<Tag> lhs, Long<Tag> rhs) => new Long<Tag>(lhs.Value / rhs.Value);
        public static Long<Tag> operator %(Long<Tag> lhs, Long<Tag> rhs) => new Long<Tag>(lhs.Value % rhs.Value);
        public static Long<Tag> operator |(Long<Tag> lhs, Long<Tag> rhs) => new Long<Tag>(lhs.Value | rhs.Value);
        public static Long<Tag> operator &(Long<Tag> lhs, Long<Tag> rhs) => new Long<Tag>(lhs.Value & rhs.Value);
        public static Long<Tag> operator ^(Long<Tag> lhs, Long<Tag> rhs) => new Long<Tag>(lhs.Value ^ rhs.Value);

        #endregion

        #region Comparison Operators

        public static bool operator ==(Long<Tag> lhs, Long<Tag> rhs) => lhs.Equals(rhs);
        public static bool operator !=(Long<Tag> lhs, Long<Tag> rhs) => !lhs.Equals(rhs);
        public static bool operator <(Long<Tag> lhs, Long<Tag> rhs) => lhs.Value < rhs.Value;
        public static bool operator >(Long<Tag> lhs, Long<Tag> rhs) => lhs.Value > rhs.Value;
        public static bool operator <=(Long<Tag> lhs, Long<Tag> rhs) => lhs.Value <= rhs.Value;
        public static bool operator >=(Long<Tag> lhs, Long<Tag> rhs) => lhs.Value >= rhs.Value;

        #endregion
    }
}