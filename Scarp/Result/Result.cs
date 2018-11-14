using System;

namespace Scarp.Result {
    /// <summary>
    /// Static class with methods for creating ResultSuccess and ResultError values.
    /// </summary>
    public static class Result {
        /// <summary>
        /// Uses type inference to make a ResultSuccess&lt;T&gt;
        /// </summary>
        public static ResultSuccess<T> Success<T>(T t) => new ResultSuccess<T>(t);

        /// <summary>
        /// Uses type inference to make a ResultError&lt;E&gt;
        /// </summary>
        public static ResultError<E> Error<E>(E e) => new ResultError<E>(e);
    }

    /// <summary>
    /// Represents either a successful return value or a failed error value.
    /// </summary>
    /// <typeparam name="T">Success return type</typeparam>
    /// <typeparam name="E">Failure error type</typeparam>
    public struct Result<T, E> : IEquatable<Result<T, E>> {
        private T SuccessValue { get; }

        private E ErrorValue { get; }

        private bool IsSuccess { get; }

        private bool IsError => !IsSuccess;

        /// <summary>
        /// Converts the ResultSuccess<T> to a Successful Result<T, E>
        /// </summary>
        /// <param name="success">A ResultSuccess with the successful return value</param>
        /// <returns>A Success Result</returns>
        public static implicit operator Result<T, E>(ResultSuccess<T> success) =>
            new Result<T, E>(success.Value, default(E), true);

        /// <summary>
        /// Converts the ResultError<E> to an Error Result<T, E>
        /// </summary>
        /// <param name="success">A ResultError with the error value</param>
        /// <returns>An Error Result</returns>
        public static implicit operator Result<T, E>(ResultError<E> error) =>
            new Result<T, E>(default(T), error.Value, false);

        private Result(T t, E e, bool success) {
            SuccessValue = t;
            ErrorValue = e;
            IsSuccess = success;
        }

        /// <summary>
        /// If this is a Success Result, assigns the success return value to the t parameter.
        /// </summary>
        /// <param name="t">A reference to a variable to hold the success return value</param>
        /// <returns>true if this is a Success Result, false otherwise</returns>
        public bool TrySuccess(out T t) {
            t = IsSuccess ? SuccessValue : default(T);

            return IsSuccess;
        }

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
        /// If this is a Success Result, invokes onSuccess() with the return value.
        /// If this is an Error Result, invokes onError() with the error value.
        ///
        /// Both handlers must return something convertible to type R
        /// </summary>
        /// <param name="onSuccess">Invoked with the return value if this is a Success Result</param>
        /// <param name="onError">Invoked with the error if this is an Error Result</param>
        /// <typeparam name="R">The type returned by the handler functions</typeparam>
        /// <returns>The result of the handler invocation</returns>
        public R Handle<R>(Func<T, R> onSuccess, Func<E, R> onError) {
            if (TrySuccess(out var success)) {
                return onSuccess(success);
            }

            if (TryError(out var error)) {
                return onError(error);
            }

            throw new InvalidOperationException("Tried to handle a Result type with no value.");
        }

        /// <summary>
        /// If this is a Success Result, invokes onSuccess() with the return value.
        /// If this is an Error Result, invokes onError() with the error value.
        ///
        /// Both handlers must return something convertible to type R
        /// </summary>
        /// <param name="onSuccess">Invoked with the return value if this is a Success Result</param>
        /// <param name="onError">Invoked with the error if this is an Error Result</param>
        /// <typeparam name="R">The type returned by the handler functions</typeparam>
        /// <returns>The result of the handler invocation</returns>
        public void Handle(Action<T> onSuccess, Action<E> onError) {
            if (TrySuccess(out var success)) {
                onSuccess(success);
                return;
            }

            if (TryError(out var error)) {
                onError(error);
                return;
            }

            throw new InvalidOperationException("Tried to handle a Result type with no value.");
        }

        public override string ToString() => IsSuccess ? SuccessValue.ToString() : ErrorValue.ToString();

        public override int GetHashCode() => IsSuccess ? SuccessValue.GetHashCode() : ErrorValue.GetHashCode();

        public override bool Equals(object other) => other is Result<T, E> result && Equals(result);

        public bool Equals(Result<T, E> other) {
            if (IsSuccess && other.IsSuccess) {
                if (object.ReferenceEquals(SuccessValue, other.SuccessValue)) {
                    return true;
                }

                if (SuccessValue == null || other.SuccessValue == null) {
                    return false;
                }

                return SuccessValue.Equals(other.SuccessValue);
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
