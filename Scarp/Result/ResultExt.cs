using System;

namespace Scarp.Result {
    public static class ResultExt {
        /// <summary>
        /// If this is an Ok Result, invokes onOk() with the return value.
        /// If this is an Error Result, propagates the error value.
        /// </summary>
        /// <param name="onOk">Invoked with the return value if this is an Ok Result</param>
        /// <returns>A Result<T, E> from invoking onOk() or propagating the error value</returns>
        public static Result<T, E> Bind<T, E>(this Result<T, E> result, Func<T, Result<T, E>> onOk) =>
            result.Handle(onOk, e => Result.Error(e));

        /// <summary>
        /// Maps a successful Ok Result to a new type, R.
        /// Propagates an Error Result's value.
        /// </summary>
        /// <param name="onOk">Invoked with the return value if this is an Ok Result</param>
        /// <typeparam name="R">The return type of onOk</typeparam>
        /// <returns>A Result<R, E> from invoking onOk() or propagating the error value</returns>
        public static Result<R, E> Map<T, E, R>(this Result<T, E> result, Func<T, R> onOk) =>
            result.Handle<Result<R, E>>(
                ok => Result.Ok(onOk(ok)),
                e => Result.Error(e));

        /// <summary>
        /// Maps an Error Result to a new type, E2.
        /// Propagates an Ok Result's value.
        /// </summary>
        /// <param name="onError">Invoked with the error value if this is an Error Result</param>
        /// <typeparam name="E2">The return type of onError</typeparam>
        /// <returns>A Result<T, E2> from propagating the Ok value or invoking onError()</returns>
        public static Result<T, E2> MapError<T, E, E2>(this Result<T, E> result, Func<E, E2> onError) =>
            result.Handle<Result<T, E2>>(
                ok => Result.Ok(ok),
                e => Result.Error(onError(e)));
    }
}