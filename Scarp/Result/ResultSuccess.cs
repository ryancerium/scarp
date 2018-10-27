using System;

namespace Scarp.Result {
    /// <summary>
    /// Contains a successful return value.
    /// Is implicitly convertible to a Result&lt;T, E&gt; for any E.
    /// </summary>
    public struct ResultSuccess<T> : IEquatable<ResultSuccess<T>> {
        /// <summary>
        /// The successful return value.
        /// </summary>
        public T Value {
            get;
        }

        public ResultSuccess(T t) => Value = t;

        public override string ToString() => Value.ToString();

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object other) => other is ResultSuccess<T> && Equals((ResultSuccess<T>) other);

        public bool Equals(ResultSuccess<T> other) => Value.Equals(other.Value);

        public static bool operator ==(ResultSuccess<T> lhs, ResultSuccess<T> rhs) => lhs.Equals(rhs);

        public static bool operator !=(ResultSuccess<T> lhs, ResultSuccess<T> rhs) => !lhs.Equals(rhs);
    }
}