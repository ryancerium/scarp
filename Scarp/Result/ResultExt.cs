using System;

namespace Scarp.Results {
    public static class ResultExt {
        /// <summary>
        /// Returns the return value of <paramref name="onOk" /> if this is an Ok value, passes an Error value through unmodified.
        /// </summary>
        /// <param name="onOk">Invoked with the Ok value</param>
        /// <returns>A Result&lt;T, E&gt; from invoking <paramref name="onOk" /> or propagating the error value</returns>
        public static Result<T, E> Bind<T, E>(this Result<T, E> result, Func<T, Result<T, E>> onOk) =>
            result.Handle(onOk, e => Result.Error(e));

        /// <summary>
        /// JavaScript-like alias for Bind().
        ///
        /// Returns the return value of <paramref name="onOk" /> if this is an Ok value, passes an Error value through unmodified.
        /// </summary>
        /// <param name="onOk">Invoked with the Ok value</param>
        /// <returns>A Result&lt;T, E&gt; from invoking <paramref name="onOk" /> or propagating the error value</returns>
        public static Result<T, E> Then<T, E>(this Result<T, E> result, Func<T, Result<T, E>> onOk) =>
            result.Bind(onOk);
    }
}