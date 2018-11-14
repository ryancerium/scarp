using Scarp.Result;
using Scarp.Tests;
using Xunit;

using ResultT = Scarp.Result.Result<int, string>;

namespace Scarp.Result.Tests {
    public class ResultTest {
        [Fact]
        public void HandleSuccessAction() {
            var successExpected = Random.Int();
            ResultT result = Result.Success(successExpected);

            int successActual = -1;
            string errorExpected = Random.String10();
            string errorActual = null;
            result.Handle(s => successActual = s, e => errorActual = e);

            Assert.Equal(successExpected, successActual);
            Assert.Null(errorActual);
        }

        [Fact]
        public void HandleErrorAction() {
            var successExpected = Random.String10();
            ResultT result = Result.Error(successExpected);

            int successActual = -1;
            string error = Random.String20();
            string errorActual = null;
            result.Handle(s => successActual = s, e => errorActual = e);

            Assert.Equal(-1, successActual);
            Assert.Equal(successExpected, errorActual);
        }

        [Fact]
        public void TrySuccess() {
            var successExpected = Random.Int();
            ResultT result = Result.Success(successExpected);

            var successActual = 0;
            Assert.True(result.TrySuccess(out successActual));
            Assert.Equal(successExpected, successActual);

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

            int successValue = -1;
            Assert.False(result.TrySuccess(out successValue));
            Assert.Equal(default(int), successValue);
        }

        [Fact]
        public void HandleSuccessFunc() {
            var successExpected = Random.Int();
            ResultT result = Result.Success(successExpected);

            var r = result.Handle(s => 1, e => 0);

            Assert.Equal(1, r);
        }

        [Fact]
        public void HandleErrorFunc() {
            var successExpected = Random.String10();
            ResultT result = Result.Error(successExpected);

            var r = result.Handle(s => 1, e => 0);

            Assert.Equal(0, r);
        }

        [Fact]
        public void ToStringTest() {
            var successExpected = Random.Int();
            ResultT result = Result.Success(successExpected);
            Assert.Equal(successExpected.ToString(), result.ToString());

            var errorExpected = Random.String10();
            result = Result.Error(errorExpected);
            Assert.Equal(errorExpected.ToString(), result.ToString());
        }

        [Fact]
        public void GetHashCodeTest() {
            var successExpected = Random.Int();
            ResultT result = Result.Success(successExpected);
            Assert.Equal(successExpected.GetHashCode(), result.GetHashCode());

            var errorExpected = Random.String10();
            result = Result.Error(errorExpected);
            Assert.Equal(errorExpected.GetHashCode(), result.GetHashCode());
        }

        [Fact]
        public void Equality() {
            var successExpected = Random.Int();

            ResultT lhs = Result.Success(successExpected);
            Assert.False(lhs.Equals(new object()));

            ResultT rhsRight = Result.Success(successExpected);
            object rhsRightObject = rhsRight;
            Assert.True(lhs.Equals(rhsRight));
            Assert.True(lhs.Equals(rhsRightObject));
            Assert.True(lhs == rhsRight);

            ResultT rhsWrong = Result.Success(successExpected + 1);
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
            Result<string, string> lhs = Result.Success<string>(null);
            Result<string, string> rhs = Result.Success<string>(null);
            Assert.Equal(lhs, rhs);
            Assert.Equal(rhs, lhs);

            rhs = Result.Success<string>(Random.String10());
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