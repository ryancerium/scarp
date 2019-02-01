using Scarp.Results;
using Scarp.Tests;
using Xunit;

namespace Scarp.Results.Tests {
    public class ResultOkTest {
        [Fact]
        public void ResultOk() {
            var expected = Random.Int();
            var actual = Result.Ok(expected);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void ResultOkToResult() {
            var expected = Random.Int();
            Result<int, int> result = Result.Ok(expected);

            int actual;
            Assert.True(result.TryOk(out actual));
            Assert.Equal(expected, actual);

            Assert.False(result.TryError(out actual));
            Assert.Equal(default(int), actual);
        }
    }
}
