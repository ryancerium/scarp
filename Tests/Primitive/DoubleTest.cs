using Scarp.Tests;
using Xunit;
using Xunit.Sdk;

using Meter = Scarp.Primitive.Double<Scarp.Primitive.Tests.DoubleTest.MeterTag>;
using Gram = Scarp.Primitive.Double<Scarp.Primitive.Tests.DoubleTest.GramTag>;

namespace Scarp.Primitive.Tests {
    public class DoubleTest {
        public class MeterTag {
        }
        public class GramTag {
        }

        [Fact]
        public void Assignment() {
            Gram grams = Random.Double();
            Assert.Throws<IsAssignableFromException>(() =>
                Assert.IsAssignableFrom<Meter>(grams));
        }

        [Fact]
        public void UnaryNegation() {
            var expected = Random.Double();
            Meter actual = expected;
            Assert.Equal(-actual, new Meter(-expected));
        }

        [Fact]
        public void PreIncrement() {
            var expected = Random.Double();
            Meter actual = expected;

            ++expected;
            Assert.Equal(new Meter(expected), ++actual);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PostIncrement() {
            var expected = Random.Double();
            Meter actual = expected;

            Assert.Equal(new Meter(expected), actual++);
            expected++;
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PreDecrement() {
            var expected = Random.Double();
            Meter actual = expected;

            --expected;
            Assert.Equal(new Meter(expected), --actual);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PostDecrement() {
            var expected = Random.Double();
            Meter actual = expected;

            Assert.Equal(new Meter(expected), actual--);
            expected--;
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void Addition() {
            var expected1 = Random.Double();
            var expected2 = Random.Double();
            var actual = new Meter(expected1) + new Meter(expected2);
            Assert.Equal(expected1 + expected2, actual.Value);
        }

        [Fact]
        public void Subtraction() {
            var expected1 = Random.Double();
            var expected2 = Random.Double();
            var actual = new Meter(expected1) - new Meter(expected2);
            Assert.Equal(expected1 - expected2, actual.Value);
        }

        [Fact]
        public void Multiplication() {
            var expected1 = Random.Double();
            var expected2 = Random.Double();
            var actual = new Meter(expected1) * new Meter(expected2);
            Assert.Equal(expected1 * expected2, actual.Value);
        }

        [Fact]
        public void Division() {
            var expected1 = Random.Double();
            var expected2 = Random.Double();
            var actual = new Meter(expected1) / new Meter(expected2);
            Assert.Equal(expected1 / expected2, actual.Value);
        }

        [Fact]
        public void Modulus() {
            var expected1 = Random.Double();
            var expected2 = Random.Double();
            var actual = new Meter(expected1) % new Meter(expected2);
            Assert.Equal(expected1 % expected2, actual.Value);
        }

        [Fact]
        public void Equality() {
            var doubleValue = Random.Double();
            Assert.True(new Meter(doubleValue) == new Meter(doubleValue));
            Assert.False(new Meter(doubleValue) == new Meter(doubleValue - 1));
        }

        [Fact]
        public void Inequality() {
            var doubleValue = Random.Double();
            Assert.False(new Meter(doubleValue) != new Meter(doubleValue));
            Assert.True(new Meter(doubleValue) != new Meter(doubleValue - 1));
        }

        [Fact]
        public void LessThan() {
            var doubleValue = Random.Double();
            Assert.True(new Meter(doubleValue) < new Meter(doubleValue + 1));
            Assert.False(new Meter(doubleValue) < new Meter(doubleValue));
        }

        [Fact]
        public void GreaterThan() {
            var doubleValue = Random.Double();
            Assert.True(new Meter(doubleValue + 1) > new Meter(doubleValue));
            Assert.False(new Meter(doubleValue) > new Meter(doubleValue));
        }

        [Fact]
        public void LessThanOrEqual() {
            var doubleValue = Random.Double();
            Assert.True(new Meter(doubleValue) <= new Meter(doubleValue + 1));
            Assert.True(new Meter(doubleValue) <= new Meter(doubleValue));
            Assert.False(new Meter(doubleValue) <= new Meter(doubleValue - 1));
        }

        [Fact]
        public void GreaterThanOrEqual() {
            var doubleValue = Random.Double();
            Assert.False(new Meter(doubleValue) >= new Meter(doubleValue + 1));
            Assert.True(new Meter(doubleValue) >= new Meter(doubleValue));
            Assert.True(new Meter(doubleValue) >= new Meter(doubleValue - 1));
        }
    }
}