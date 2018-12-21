using System;

namespace Scarp.Result {
    /// <summary>
    /// Static class with methods for creating ResultOk and ResultError values.
    /// </summary>
    public static class Result {
        /// <summary>
        /// Uses type inference to make a ResultOk&lt;T&gt;
        /// </summary>
        public static ResultOk<T> Ok<T>(T t) => new ResultOk<T>(t);

        /// <summary>
        /// Obsoleted alias for Result.Ok().
        /// </summary>
        [Obsolete("Call Result.Ok() instead.")]
        public static ResultOk<T> Success<T>(T t) => new ResultOk<T>(t);

        /// <summary>
        /// Uses type inference to make a ResultError&lt;E&gt;
        /// </summary>
        public static ResultError<E> Error<E>(E e) => new ResultError<E>(e);
    }

    /// <summary>
    /// Represents either an ok return value or a failed error value.
    /// </summary>
    /// <typeparam name="T">Ok return type</typeparam>
    /// <typeparam name="E">Failure error type</typeparam>
    public struct Result<T, E> : IEquatable<Result<T, E>> {
        private T OkValue { get; }

        private E ErrorValue { get; }

        private bool IsOk { get; }

        private bool IsError => !IsOk;

        /// <summary>
        /// Converts the ResultOk<T> to an Ok Result<T, E>
        /// </summary>
        /// <param name="ok">A ResultOk<T> with the ok return value</param>
        /// <returns>An Ok Result</returns>
        public static implicit operator Result<T, E>(ResultOk<T> ok) =>
            new Result<T, E>(ok.Value, default(E), true);

        /// <summary>
        /// Converts the ResultError<E> to an Error Result<T, E>
        /// </summary>
        /// <param name="ok">A ResultError with the error value</param>
        /// <returns>An Error Result</returns>
        public static implicit operator Result<T, E>(ResultError<E> error) =>
            new Result<T, E>(default(T), error.Value, false);

        private Result(T t, E e, bool ok) {
            OkValue = t;
            ErrorValue = e;
            IsOk = ok;
        }

        /// <summary>
        /// If this is an Ok Result, assigns the ok return value to the t parameter.
        /// </summary>
        /// <param name="t">A reference to a variable to hold the ok return value</param>
        /// <returns>true if this is an Ok Result, false otherwise</returns>
        public bool TryOk(out T t) {
            t = IsOk ? OkValue : default(T);

            return IsOk;
        }

        /// <summary>
        /// Obsoleted alias for Result.TryOk().
        /// </summary>
        [Obsolete("Call Result.TryOk() instead.")]
        public bool TrySuccess(out T t) => TryOk(out t);

        /// <summary>
        /// If this is an Error Result, assigns the error value to the e parameter.
        /// </summary>
        /// <param name="e">A reference to a variable to hold the error value</param>
        /// <returns>true if this is an Error Result, false otherwise</returns>
        public bool TryError(out E e) {
            e = IsError ? ErrorValue : default(E);

            return IsError;
        }

        /// <summary>
        /// If this is an Ok Result, invokes onOk() with the return value.
        /// If this is an Error Result, invokes onError() with the error value.
        ///
        /// Both handlers must return something convertible to type R
        /// </summary>
        /// <param name="onOk">Invoked with the return value if this is an Ok Result</param>
        /// <param name="onError">Invoked with the error if this is an Error Result</param>
        /// <typeparam name="R">The type returned by the handler functions</typeparam>
        /// <returns>The result of the handler invocation</returns>
        public R Handle<R>(Func<T, R> onOk, Func<E, R> onError) {
            if (TryOk(out var ok)) {
                return onOk(ok);
            }

            if (TryError(out var error)) {
                return onError(error);
            }

            throw new InvalidOperationException("Tried to handle a Result type with no value.");
        }

        /// <summary>
        /// If this is an Ok Result, invokes onOk() with the return value.
        /// If this is an Error Result, invokes onError() with the error value.
        ///
        /// Both handlers must return something convertible to type R
        /// </summary>
        /// <param name="onOk">Invoked with the return value if this is an Ok Result</param>
        /// <param name="onError">Invoked with the error if this is an Error Result</param>
        /// <typeparam name="R">The type returned by the handler functions</typeparam>
        /// <returns>The result of the handler invocation</returns>
        public void Handle(Action<T> onOk, Action<E> onError) {
            if (TryOk(out var ok)) {
                onOk(ok);
                return;
            }

            if (TryError(out var error)) {
                onError(error);
                return;
            }

            throw new InvalidOperationException("Tried to handle a Result type with no value.");
        }

        /// <summary>
        /// If this is an Ok Result, invokes onOk() with the return value.
        /// If this is an Error Result, propagates the error value.
        /// </summary>
        /// <param name="onOk">Invoked with the return value if this is an Ok Result</param>
        /// <typeparam name="R">The type returned by the handler function</typeparam>
        /// <returns>A Result<R, E> from invoking onOk() or propagating the error value</returns>
        public Result<R, E> Bind<R>(Func<T, Result<R, E>> onOk) => Handle(onOk, e => Result.Error(e));

        public override string ToString() => IsOk ? OkValue.ToString() : ErrorValue.ToString();

        public override int GetHashCode() => IsOk ? OkValue.GetHashCode() : ErrorValue.GetHashCode();

        public override bool Equals(object other) => other is Result<T, E> result && Equals(result);

        public bool Equals(Result<T, E> other) {
            if (IsOk && other.IsOk) {
                if (object.ReferenceEquals(OkValue, other.OkValue)) {
                    return true;
                }

                if (OkValue == null || other.OkValue == null) {
                    return false;
                }

                return OkValue.Equals(other.OkValue);
            }

            if (IsError && other.IsError) {
                if (object.ReferenceEquals(ErrorValue, other.ErrorValue)) {
                    return true;
                }

                if (ErrorValue == null || other.ErrorValue == null) {
                    return false;
                }

                return ErrorValue.Equals(other.ErrorValue);
            }

            return false;

        }

        public static bool operator ==(Result<T, E> lhs, Result<T, E> rhs) => lhs.Equals(rhs);

        public static bool operator !=(Result<T, E> lhs, Result<T, E> rhs) => !lhs.Equals(rhs);
    }
}
