using System;

namespace Scarp.Results {
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
        /// JavaScript-like alias for Bind().
        ///
        /// If this is an Ok Result, invokes onOk() with the return value.
        /// If this is an Error Result, propagates the error value.
        /// </summary>
        /// <param name="onOk">Invoked with the return value if this is an Ok Result</param>
        /// <returns>A Result<T, E> from invoking onOk() or propagating the error value</returns>
        public static Result<T, E> Then<T, E>(this Result<T, E> result, Func<T, Result<T, E>> onOk) =>
            result.Bind(onOk);
    }
}