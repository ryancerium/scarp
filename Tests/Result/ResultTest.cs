using Scarp.Result;
using Scarp.Tests;
using Xunit;

using ResultT = Scarp.Result.Result<int, string>;

namespace Scarp.Result.Tests {
    public class ResultTest {
        [Fact]
        public void HandleOkAction() {
            var okExpected = Random.Int();
            ResultT result = Result.Ok(okExpected);

            int okActual = -1;
            string errorExpected = Random.String10();
            string errorActual = null;
            result.Handle(s => okActual = s, e => errorActual = e);

            Assert.Equal(okExpected, okActual);
            Assert.Null(errorActual);
        }

        [Fact]
        public void HandleErrorAction() {
            var okExpected = Random.String10();
            ResultT result = Result.Error(okExpected);

            int okActual = -1;
            string error = Random.String20();
            string errorActual = null;
            result.Handle(s => okActual = s, e => errorActual = e);

            Assert.Equal(-1, okActual);
            Assert.Equal(okExpected, errorActual);
        }

        [Fact]
        public void TryOk() {
            var okExpected = Random.Int();
            ResultT result = Result.Ok(okExpected);

            var okActual = 0;
            Assert.True(result.TryOk(out okActual));
            Assert.Equal(okExpected, okActual);

            string errorActual = null;
            Assert.False(result.TryError(out errorActual));
            Assert.Null(errorActual);
        }

        [Fact]
        public void TryError() {
            var errorExpected = Random.String10();
            ResultT result = Result.Error(errorExpected);

            string errorActual = null;
            Assert.True(result.TryError(out errorActual));
            Assert.Equal(errorExpected, errorActual);

            int okValue = -1;
            Assert.False(result.TryOk(out okValue));
            Assert.Equal(default(int), okValue);
        }

        [Fact]
        public void HandleOkFunc() {
            var okExpected = Random.Int();
            ResultT result = Result.Ok(okExpected);

            var r = result.Handle(s => 1, e => 0);

            Assert.Equal(1, r);
        }

        [Fact]
        public void HandleErrorFunc() {
            var okExpected = Random.String10();
            ResultT result = Result.Error(okExpected);

            var r = result.Handle(s => 1, e => 0);

            Assert.Equal(0, r);
        }

        [Fact]
        public void ToStringTest() {
            var okExpected = Random.Int();
            ResultT result = Result.Ok(okExpected);
            Assert.Equal(okExpected.ToString(), result.ToString());

            var errorExpected = Random.String10();
            result = Result.Error(errorExpected);
            Assert.Equal(errorExpected.ToString(), result.ToString());
        }

        [Fact]
        public void GetHashCodeTest() {
            var okExpected = Random.Int();
            ResultT result = Result.Ok(okExpected);
            Assert.Equal(okExpected.GetHashCode(), result.GetHashCode());

            var errorExpected = Random.String10();
            result = Result.Error(errorExpected);
            Assert.Equal(errorExpected.GetHashCode(), result.GetHashCode());
        }

        [Fact]
        public void Equality() {
            var okExpected = Random.Int();

            ResultT lhs = Result.Ok(okExpected);
            Assert.False(lhs.Equals(new object()));

            ResultT rhsRight = Result.Ok(okExpected);
            object rhsRightObject = rhsRight;
            Assert.True(lhs.Equals(rhsRight));
            Assert.True(lhs.Equals(rhsRightObject));
            Assert.True(lhs == rhsRight);

            ResultT rhsWrong = Result.Ok(okExpected + 1);
            object rhsWrongObject = rhsWrong;
            Assert.False(lhs.Equals(rhsWrong));
            Assert.False(lhs.Equals(rhsWrongObject));
            Assert.False(lhs == rhsWrong);

            var errorExpected = Random.String10();
            lhs = Result.Error(errorExpected);
            Assert.False(lhs.Equals(new object()));

            rhsRight = Result.Error(errorExpected);
            rhsRightObject = rhsRight;
            Assert.True(lhs.Equals(rhsRight));
            Assert.True(lhs.Equals(rhsRightObject));
            Assert.True(lhs == rhsRight);

            rhsWrong = Result.Error(Random.String(20));
            rhsWrongObject = rhsWrong;
            Assert.False(lhs.Equals(rhsWrong));
            Assert.False(lhs.Equals(rhsWrongObject));
            Assert.False(lhs == rhsWrong);
        }

        [Fact]
        public void NullEquality() {
            Result<string, string> lhs = Result.Ok<string>(null);
            Result<string, string> rhs = Result.Ok<string>(null);
            Assert.Equal(lhs, rhs);
            Assert.Equal(rhs, lhs);

            rhs = Result.Ok<string>(Random.String10());
            Assert.NotEqual(lhs, rhs);
            Assert.NotEqual(rhs, lhs);

            lhs = Result.Error<string>(null);
            rhs = Result.Error<string>(null);
            Assert.Equal(lhs, rhs);
            Assert.Equal(rhs, lhs);

            rhs = Result.Error<string>(Random.String10());
            Assert.NotEqual(lhs, rhs);
            Assert.NotEqual(rhs, lhs);
        }
    }
}