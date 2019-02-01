using System;

namespace Scarp.Results {
    /// <summary>
    /// Contains a failure error value.
    /// Is implicitly convertible to a Result&lt;T, E&gt; for any T.
    /// </summary>
    public struct ResultError<E> : IEquatable<ResultError<E>> {
        /// <summary>
        /// The failure error value.
        /// </summary>
        public E Value {
            get;
        }

        public ResultError(E e) => Value = e;

        public override string ToString() => Value.ToString();

        public override int GetHashCode() => Value.GetHashCode();

        public override bool Equals(object other) => other is ResultError<E> && Equals((ResultError<E>) other);

        public bool Equals(ResultError<E> other) => Value.Equals(other.Value);

        public static bool operator ==(ResultError<E> lhs, ResultError<E> rhs) => lhs.Equals(rhs);

        public static bool operator !=(ResultError<E> lhs, ResultError<E> rhs) => !lhs.Equals(rhs);
    }
}