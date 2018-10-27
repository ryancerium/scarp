using Scarp.Result;
using Scarp.Tests;
using Xunit;

namespace Scarp.Result.Tests {
    public class ResultSuccessTest {
        [Fact]
        public void MakeSuccess() {
            var expected = Random.Int();
            var actual = Make.Success(expected);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void MakeSuccessToResult() {
            var expected = Random.Int();
            Result<int, int> result = Make.Success(expected);

            int actual;
            Assert.True(result.TrySuccess(out actual));
            Assert.Equal(expected, actual);

            Assert.False(result.TryError(out actual));
            Assert.Equal(default(int), actual);
        }
    }
}
