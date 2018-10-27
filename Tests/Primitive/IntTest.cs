using Scarp.Tests;
using Xunit;
using Xunit.Sdk;

using Meter = Scarp.Primitive.Int<Scarp.Primitive.Tests.IntTest.MeterTag>;
using Gram = Scarp.Primitive.Int<Scarp.Primitive.Tests.IntTest.GramTag>;

namespace Scarp.Primitive.Tests {
    public class IntTest {
        public class MeterTag {
        }
        public class GramTag {
        }

        [Fact]
        public void Assignment() {
            Gram grams = Random.Int();
            Assert.Throws<IsAssignableFromException>(() =>
                Assert.IsAssignableFrom<Meter>(grams));
        }

        [Fact]
        public void UnaryNegation() {
            var expected = Random.Int();
            Meter actual = expected;
            Assert.Equal(-actual, new Meter(-expected));
        }

        [Fact]
        public void BitwiseComplement() {
            var expected = Random.Int();
            Meter actual = expected;
            Assert.Equal(new Meter(~expected), ~actual);
        }

        [Fact]
        public void PreIncrement() {
            var expected = Random.Int();
            Meter actual = expected;

            ++expected;
            Assert.Equal(new Meter(expected), ++actual);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PostIncrement() {
            var expected = Random.Int();
            Meter actual = expected;

            Assert.Equal(new Meter(expected), actual++);
            expected++;
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PreDecrement() {
            var expected = Random.Int();
            Meter actual = expected;

            --expected;
            Assert.Equal(new Meter(expected), --actual);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PostDecrement() {
            var expected = Random.Int();
            Meter actual = expected;

            Assert.Equal(new Meter(expected), actual--);
            expected--;
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void Addition() {
            var expected1 = Random.Int();
            var expected2 = Random.Int();
            var actual = new Meter(expected1) + new Meter(expected2);
            Assert.Equal(expected1 + expected2, actual.Value);
        }

        [Fact]
        public void Subtraction() {
            var expected1 = Random.Int();
            var expected2 = Random.Int();
            var actual = new Meter(expected1) - new Meter(expected2);
            Assert.Equal(expected1 - expected2, actual.Value);
        }

        [Fact]
        public void Multiplication() {
            var expected1 = Random.Int();
            var expected2 = Random.Int();
            var actual = new Meter(expected1) * new Meter(expected2);
            Assert.Equal(expected1 * expected2, actual.Value);
        }

        [Fact]
        public void Division() {
            var expected1 = Random.Int();
            var expected2 = Random.Int();
            var actual = new Meter(expected1) / new Meter(expected2);
            Assert.Equal(expected1 / expected2, actual.Value);
        }

        [Fact]
        public void Modulus() {
            var expected1 = Random.Int();
            var expected2 = Random.Int();
            var actual = new Meter(expected1) % new Meter(expected2);
            Assert.Equal(expected1 % expected2, actual.Value);
        }

        [Fact]
        public void BitwiseOr() {
            var expected1 = Random.Int();
            var expected2 = Random.Int();
            var actual = new Meter(expected1) | new Meter(expected2);
            Assert.Equal(expected1 | expected2, actual.Value);
        }

        [Fact]
        public void BitwiseAnd() {
            var expected1 = Random.Int();
            var expected2 = Random.Int();
            var actual = new Meter(expected1) & new Meter(expected2);
            Assert.Equal(expected1 & expected2, actual.Value);
        }

        [Fact]
        public void BitwiseXor() {
            var expected1 = Random.Int();
            var expected2 = Random.Int();
            var actual = new Meter(expected1) ^ new Meter(expected2);
            Assert.Equal(expected1 ^ expected2, actual.Value);
        }

        [Fact]
        public void Equality() {
            var intValue = Random.Int();
            Assert.True(new Meter(intValue) == new Meter(intValue));
            Assert.False(new Meter(intValue) == new Meter(intValue - 1));
        }

        [Fact]
        public void Inequality() {
            var intValue = Random.Int();
            Assert.False(new Meter(intValue) != new Meter(intValue));
            Assert.True(new Meter(intValue) != new Meter(intValue - 1));
        }

        [Fact]
        public void LessThan() {
            var intValue = Random.Int();
            Assert.True(new Meter(intValue) < new Meter(intValue + 1));
            Assert.False(new Meter(intValue) < new Meter(intValue));
        }

        [Fact]
        public void GreaterThan() {
            var intValue = Random.Int();
            Assert.True(new Meter(intValue + 1) > new Meter(intValue));
            Assert.False(new Meter(intValue) > new Meter(intValue));
        }

        [Fact]
        public void LessThanOrEqual() {
            var intValue = Random.Int();
            Assert.True(new Meter(intValue) <= new Meter(intValue + 1));
            Assert.True(new Meter(intValue) <= new Meter(intValue));
            Assert.False(new Meter(intValue) <= new Meter(intValue - 1));
        }

        [Fact]
        public void GreaterThanOrEqual() {
            var intValue = Random.Int();
            Assert.False(new Meter(intValue) >= new Meter(intValue + 1));
            Assert.True(new Meter(intValue) >= new Meter(intValue));
            Assert.True(new Meter(intValue) >= new Meter(intValue - 1));
        }
    }
}