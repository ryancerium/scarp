using Scarp.Result;
using Scarp.Tests;
using Xunit;

using ResultT = Scarp.Result.Result<int, string>;

namespace Scarp.Result.Tests {
    public class ResultTest {
        [Fact]
        public void HandleSuccessAction() {
            var successExpected = Random.Int();
            ResultT result = Make.Success(successExpected);

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
            ResultT result = Make.Error(successExpected);

            int successActual = -1;
            string error = Random.String20();
            string errorActual = null;
            result.Handle(s => successActual = s, e => errorActual = e);

            Assert.Equal(-1, successActual);
            Assert.Equal(successExpected, errorActual);
        }

        [Fact]
        public void HandleSuccessFunc() {
            var successExpected = Random.Int();
            ResultT result = Make.Success(successExpected);

            var r = result.Handle(s => 1, e => 0);

            Assert.Equal(1, r);
        }

        [Fact]
        public void HandleErrorFunc() {
            var successExpected = Random.String10();
            ResultT result = Make.Error(successExpected);

            var r = result.Handle(s => 1, e => 0);

            Assert.Equal(0, r);
        }

        [Fact]
        public void ToStringTest() {
            var successExpected = Random.Int();
            ResultT result = Make.Success(successExpected);
            Assert.Equal(successExpected.ToString(), result.ToString());

            var errorExpected = Random.String10();
            result = Make.Error(errorExpected);
            Assert.Equal(errorExpected.ToString(), result.ToString());
        }

        [Fact]
        public void GetHashCodeTest() {
            var successExpected = Random.Int();
            ResultT result = Make.Success(successExpected);
            Assert.Equal(successExpected.GetHashCode(), result.GetHashCode());

            var errorExpected = Random.String10();
            result = Make.Error(errorExpected);
            Assert.Equal(errorExpected.GetHashCode(), result.GetHashCode());
        }

        [Fact]
        public void Equality() {
            var successExpected = Random.Int();

            ResultT lhs = Make.Success(successExpected);
            Assert.False(lhs.Equals(new object()));

            ResultT rhsRight = Make.Success(successExpected);
            object rhsRightObject = rhsRight;
            Assert.True(lhs.Equals(rhsRight));
            Assert.True(lhs.Equals(rhsRightObject));
            Assert.True(lhs == rhsRight);

            ResultT rhsWrong = Make.Success(successExpected + 1);
            object rhsWrongObject = rhsWrong;
            Assert.False(lhs.Equals(rhsWrong));
            Assert.False(lhs.Equals(rhsWrongObject));
            Assert.False(lhs == rhsWrong);

            var errorExpected = Random.String10();
            lhs = Make.Error(errorExpected);
            Assert.False(lhs.Equals(new object()));

            rhsRight = Make.Error(errorExpected);
            rhsRightObject = rhsRight;
            Assert.True(lhs.Equals(rhsRight));
            Assert.True(lhs.Equals(rhsRightObject));
            Assert.True(lhs == rhsRight);

            rhsWrong = Make.Error(Random.String(20));
            rhsWrongObject = rhsWrong;
            Assert.False(lhs.Equals(rhsWrong));
            Assert.False(lhs.Equals(rhsWrongObject));
            Assert.False(lhs == rhsWrong);
        }
    }
}