using Scarp.Tests;
using Xunit;
using Xunit.Sdk;

using Meter = Scarp.Primitive.ULong<Scarp.Primitive.Tests.ULongTest.MeterTag>;
using Gram = Scarp.Primitive.ULong<Scarp.Primitive.Tests.ULongTest.GramTag>;

namespace Scarp.Primitive.Tests {
    public class ULongTest {
        public class MeterTag {
        }
        public class GramTag {
        }

        [Fact]
        public void Assignment() {
            Gram grams = Random.ULong();
            Assert.Throws<IsAssignableFromException>(() =>
                Assert.IsAssignableFrom<Meter>(grams));
        }

        [Fact]
        public void BitwiseComplement() {
            var expected = Random.ULong();
            Meter actual = expected;
            Assert.Equal(new Meter(~expected), ~actual);
        }

        [Fact]
        public void PreIncrement() {
            var expected = Random.ULong();
            Meter actual = expected;

            ++expected;
            Assert.Equal(new Meter(expected), ++actual);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PostIncrement() {
            var expected = Random.ULong();
            Meter actual = expected;

            Assert.Equal(new Meter(expected), actual++);
            expected++;
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PreDecrement() {
            var expected = Random.ULong();
            Meter actual = expected;

            --expected;
            Assert.Equal(new Meter(expected), --actual);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PostDecrement() {
            var expected = Random.ULong();
            Meter actual = expected;

            Assert.Equal(new Meter(expected), actual--);
            expected--;
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void Addition() {
            var expected1 = Random.ULong();
            var expected2 = Random.ULong();
            var actual = new Meter(expected1) + new Meter(expected2);
            Assert.Equal(expected1 + expected2, actual.Value);
        }

        [Fact]
        public void Subtraction() {
            var expected1 = Random.ULong();
            var expected2 = Random.ULong();
            var actual = new Meter(expected1) - new Meter(expected2);
            Assert.Equal(expected1 - expected2, actual.Value);
        }

        [Fact]
        public void Multiplication() {
            var expected1 = Random.ULong();
            var expected2 = Random.ULong();
            var actual = new Meter(expected1) * new Meter(expected2);
            Assert.Equal(expected1 * expected2, actual.Value);
        }

        [Fact]
        public void Division() {
            var expected1 = Random.ULong();
            var expected2 = Random.ULong();
            var actual = new Meter(expected1) / new Meter(expected2);
            Assert.Equal(expected1 / expected2, actual.Value);
        }

        [Fact]
        public void Modulus() {
            var expected1 = Random.ULong();
            var expected2 = Random.ULong();
            var actual = new Meter(expected1) % new Meter(expected2);
            Assert.Equal(expected1 % expected2, actual.Value);
        }

        [Fact]
        public void BitwiseOr() {
            var expected1 = Random.ULong();
            var expected2 = Random.ULong();
            var actual = new Meter(expected1) | new Meter(expected2);
            Assert.Equal(expected1 | expected2, actual.Value);
        }

        [Fact]
        public void BitwiseAnd() {
            var expected1 = Random.ULong();
            var expected2 = Random.ULong();
            var actual = new Meter(expected1) & new Meter(expected2);
            Assert.Equal(expected1 & expected2, actual.Value);
        }

        [Fact]
        public void BitwiseXor() {
            var expected1 = Random.ULong();
            var expected2 = Random.ULong();
            var actual = new Meter(expected1) ^ new Meter(expected2);
            Assert.Equal(expected1 ^ expected2, actual.Value);
        }

        [Fact]
        public void Equality() {
            var ulongValue = Random.ULong();
            Assert.True(new Meter(ulongValue) == new Meter(ulongValue));
            Assert.False(new Meter(ulongValue) == new Meter(ulongValue - 1));
        }

        [Fact]
        public void Inequality() {
            var ulongValue = Random.ULong();
            Assert.False(new Meter(ulongValue) != new Meter(ulongValue));
            Assert.True(new Meter(ulongValue) != new Meter(ulongValue - 1));
        }

        [Fact]
        public void LessThan() {
            var ulongValue = Random.ULong();
            Assert.True(new Meter(ulongValue) < new Meter(ulongValue + 1));
            Assert.False(new Meter(ulongValue) < new Meter(ulongValue));
        }

        [Fact]
        public void GreaterThan() {
            var ulongValue = Random.ULong();
            Assert.True(new Meter(ulongValue + 1) > new Meter(ulongValue));
            Assert.False(new Meter(ulongValue) > new Meter(ulongValue));
        }

        [Fact]
        public void LessThanOrEqual() {
            var ulongValue = Random.ULong();
            Assert.True(new Meter(ulongValue) <= new Meter(ulongValue + 1));
            Assert.True(new Meter(ulongValue) <= new Meter(ulongValue));
            Assert.False(new Meter(ulongValue) <= new Meter(ulongValue - 1));
        }

        [Fact]
        public void GreaterThanOrEqual() {
            var ulongValue = Random.ULong();
            Assert.False(new Meter(ulongValue) >= new Meter(ulongValue + 1));
            Assert.True(new Meter(ulongValue) >= new Meter(ulongValue));
            Assert.True(new Meter(ulongValue) >= new Meter(ulongValue - 1));
        }
    }
}