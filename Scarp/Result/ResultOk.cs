using System;

namespace Scarp.Result {
    /// <summary>
    /// Contains an ok return value.
    /// Is implicitly convertible to a Result&lt;T, E&gt; for any E.
    /// </summary>
    public struct ResultOk<T> : IEquatable<ResultOk<T>> {
        /// <summary>
        /// The ok return value.
        /// </summary>
        public T Value {
            get;
        }

        public ResultOk(T t) => Value = t;

        public override string ToString() => Value.ToString();

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object other) => other is ResultOk<T> && Equals((ResultOk<T>) other);

        public bool Equals(ResultOk<T> other) => Value.Equals(other.Value);

        public static bool operator ==(ResultOk<T> lhs, ResultOk<T> rhs) => lhs.Equals(rhs);

        public static bool operator !=(ResultOk<T> lhs, ResultOk<T> rhs) => !lhs.Equals(rhs);
    }
}