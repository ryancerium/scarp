using System;

namespace Scarp.Result {
    public static class Make {
        /// <summary>
        /// Uses type inference to make a ResultSuccess&lt;T&gt;
        /// </summary>
        public static ResultSuccess<T> Success<T>(T t) {
            return new ResultSuccess<T>(t);
        }

        /// <summary>
        /// Uses type inference to make a ResultError&lt;E&gt;
        /// </summary>
        public static ResultError<E> Error<E>(E e) {
            return new ResultError<E>(e);
        }
    }
}