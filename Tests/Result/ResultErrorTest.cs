using Scarp.Result;
using Scarp.Tests;
using Xunit;

namespace Scarp.Result.Tests {
    public class ResultErrorTest {
        [Fact]
        public void ResultError() {
            var expected = Random.Int();
            var actual = Result.Error(expected);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void ResultErrorToResult() {
            var expected = Random.Int();
            Result<int, int> result = Result.Error(expected);

            int actual;
            Assert.True(result.TryError(out actual));
            Assert.Equal(expected, actual);

            Assert.False(result.TryOk(out actual));
            Assert.Equal(default(int), actual);
        }
    }
}
