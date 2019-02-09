using System;

namespace Scarp.Results {
    /// <summary>
    /// Static class with methods for creating ResultOk and ResultError values.
    /// </summary>
    public static class Result {
        /// <summary>
        /// Creates a <c>ResultOk&lt;T&gt;</c>, which is implicitly convertible to a <c>Result&lt;T, E&gt;</c> for any <c>E</c>.
        /// </summary>
        public static ResultOk<T> Ok<T>(T t) => new ResultOk<T>(t);

        /// <summary>
        /// Obsoleted alias for Result.Ok().
        /// </summary>
        [Obsolete("Call Result.Ok() instead.", true)]
        public static ResultOk<T> Success<T>(T t) => new ResultOk<T>(t);

        /// <summary>
        /// Creates a <c>ResultError&lt;T&gt;</c>, which is implicitly convertible to a <c>Result&lt;T, E&gt;</c> for any <c>T</c>.
        /// </summary>
        public static ResultError<E> Error<E>(E e) => new ResultError<E>(e);
    }

    /// <summary>
    /// Represents either an Ok value or an Error value.
    /// </summary>
    /// <typeparam name="T">Ok type</typeparam>
    /// <typeparam name="E">Error type</typeparam>
    public struct Result<T, E> : IEquatable<Result<T, E>> {
        private T OkValue { get; }

        private E ErrorValue { get; }

        private bool IsOk { get; }

        private bool IsError => !IsOk;

        /// <summary>
        /// Converts the <c>ResultOk&lt;T&gt;</c> to a <c>Result&lt;T, E&gt;</c> with an Ok value.
        /// </summary>
        /// <param name="ok">A <c>ResultOk&lt;T&gt;</c> with the Ok value</param>
        public static implicit operator Result<T, E>(ResultOk<T> ok) =>
            new Result<T, E>(ok.Value, default(E), true);

        /// <summary>
        /// Converts the <c>ResultError&lt;E&gt;</c> to an Error Result&lt;T, E&gt;
        /// </summary>
        /// <param name="ok">A <c>ResultError&lt;E&gt;</c> with the Error value</param>
        public static implicit operator Result<T, E>(ResultError<E> error) =>
            new Result<T, E>(default(T), error.Value, false);

        private Result(T t, E e, bool ok) {
            OkValue = t;
            ErrorValue = e;
            IsOk = ok;
        }

        /// <summary>
        /// If this is an Ok value, assigns the Ok value to the <paramref name="t"/> parameter.
        /// </summary>
        /// <param name="t">A reference to a variable to hold the Ok value</param>
        /// <returns>true if this is an Ok value, false otherwise</returns>
        public bool TryOk(out T t) {
            t = IsOk ? OkValue : default(T);

            return IsOk;
        }

        /// <summary>
        /// Obsoleted alias for Result.TryOk().
        /// </summary>
        [Obsolete("Call Result.TryOk() instead.", true)]
        public bool TrySuccess(out T t) => TryOk(out t);

        /// <summary>
        /// If this is an Error value, assigns the Error value to the <paramref name="e"/> parameter.
        /// </summary>
        /// <param name="e">A reference to a variable to hold the Error value</param>
        /// <returns>true if this is an Error value, false otherwise</returns>
        public bool TryError(out E e) {
            e = IsError ? ErrorValue : default(E);

            return IsError;
        }

        /// <summary>
        /// Returns the return value of the appropriate function, depending on whether this is an Ok or an Error value.
        ///
        /// Both handlers must return something convertible to type <typeparamref name="R" />
        /// </summary>
        /// <param name="onOk">Invoked with the Ok value</param>
        /// <param name="onError">Invoked with the Error value</param>
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
        /// JavaScript-like alias for Handle().
        ///
        /// Returns the return value of the appropriate function, depending on whether this is an Ok or an Error value.
        ///
        /// Both handlers must return something convertible to type <typeparamref name="R" />
        /// </summary>
        /// <param name="onOk">Invoked with the Ok value</param>
        /// <param name="onError">Invoked with the Error value</param>
        /// <typeparam name="R">The type returned by the handler functions</typeparam>
        /// <returns>The result of the handler invocation</returns>
        public R Then<R>(Func<T, R> onOk, Func<E, R> onError) => Handle(onOk, onError);

        /// <summary>
        /// Invokes the appropriate <c>Action</c>, depending on whether this is an Ok or an Error value.
        /// </summary>
        /// <param name="onOk">Invoked with the Ok value</param>
        /// <param name="onError">Invoked with the Error value</param>
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
        /// JavaScript-like alias for Handle().
        ///
        /// <summary>
        /// Invokes the appropriate <c>Action</c>, depending on whether this is an Ok or an Error value.
        /// </summary>
        /// <param name="onOk">Invoked with the Ok value</param>
        /// <param name="onError">Invoked with the Error value</param>
        public void Then(Action<T> onOk, Action<E> onError) => Handle(onOk, onError);

        /// <summary>
        /// Returns the return value of <paramref name="onOk" /> if this is an Ok value, passes an Error value through unmodified.
        /// </summary>
        /// <param name="onOk">Invoked with the Ok value</param>
        /// <typeparam name="R">The type returned by <paramref name="onOk" /></typeparam>
        /// <returns>A Result&lt;R, E&gt; from invoking <paramref name="onOk" /> or propagating the error value</returns>
        public Result<R, E> Bind<R>(Func<T, Result<R, E>> onOk) => Handle(onOk, e => Result.Error(e));

        /// <summary>
        /// JavaScript-like alias for Bind().
        ///
        /// If this is an Ok Result, invokes <paramref name="onOk" /> with the return value.
        /// If this is an Error Result, propagates the error value.
        /// </summary>
        /// <param name="onOk">Invoked with the Ok value</param>
        /// <typeparam name="R">The return type of <paramref name="onOk" /></typeparam>
        /// <returns>A Result&lt;T, E&gt;  invoking <paramref name="onOk" /> or propagating the error value</returns>
        public Result<R, E> Then<R>(Func<T, Result<R, E>> onOk) => Bind(onOk);


        /// <summary>
        /// Maps an Ok value from a <typeparamref name="T" /> to an <typeparamref name="R" />by calling
        /// <paramref name="onOk"/>, passes an Error value through unmodified.
        /// </summary>
        /// <param name="onOk">Invoked with the Ok value</param>
        /// <typeparam name="R">The return type of <paramref name="onOk" /></typeparam>
        /// <returns>A Result&lt;T, E&gt;  invoking <paramref name="onOk" /> or propagating the error value</returns>
        public Result<R, E> Map<R>(Func<T, R> onOk) =>
            Handle<Result<R, E>>(
                ok => Result.Ok(onOk(ok)),
                e => Result.Error(e));

        /// <summary>
        /// Maps an Error value from an <typeparamref name="E" /> to an <typeparamref name="E2" /> by calling
        /// <paramref name="onError"/>, passes an Ok value through unmodified.
        /// </summary>
        /// <param name="onError">Invoked with the Error value</param>
        /// <typeparam name="E2">The return type of <paramref name="onError"/></typeparam>
        /// <returns>A Result&lt;T, E2&gt; from propagating the Ok value or invoking <paramref name="onError"/></returns>
        public Result<T, E2> MapError<E2>(Func<E, E2> onError) =>
            Handle<Result<T, E2>>(
                ok => Result.Ok(ok),
                error => Result.Error(onError(error)));

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
