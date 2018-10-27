using Scarp.Result;
using Scarp.Tests;
using Xunit;

namespace Scarp.Result.Tests {
    public class ResultErrorTest {
        [Fact]
        public void MakeError() {
            var expected = Random.Int();
            var actual = Make.Error(expected);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void MakeErrorToResult() {
            var expected = Random.Int();
            Result<int, int> result = Make.Error(expected);

            int actual;
            Assert.True(result.TryError(out actual));
            Assert.Equal(expected, actual);

            Assert.False(result.TrySuccess(out actual));
            Assert.Equal(default(int), actual);
        }
    }
}
