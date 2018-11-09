using Newtonsoft.Json;
using Scarp.Tests;
using Xunit;
using Xunit.Sdk;

using Meter = Scarp.Primitive.Long<Scarp.Primitive.Tests.LongTest.MeterTag>;
using Gram = Scarp.Primitive.Long<Scarp.Primitive.Tests.LongTest.GramTag>;

namespace Scarp.Primitive.Tests {
    public class LongTest {
        public class MeterTag {
        }
        public class GramTag {
        }

        internal class Location {
            public Meter x;
            public Meter? y;

            public override bool Equals(object obj) =>
                obj is Location other && other.x == x && other.y == y;

            public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();
        }

        [Fact]
        public void ToJsonTest() {
            var location = new Location { x = Random.Long(), y = Random.Long() };
            var actual = JsonConvert.SerializeObject(location);

            var expected = $"{{\"x\":{location.x},\"y\":{location.y}}}";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromJsonTest() {
            var expected = new Location { x = Random.Long(), y = Random.Long() };
            var json = $"{{\"x\":{expected.x},\"y\":{expected.y}}}";
            var actual = JsonConvert.DeserializeObject<Location>(json);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Assignment() {
            Gram grams = Random.Long();
            Assert.Throws<IsAssignableFromException>(() =>
                Assert.IsAssignableFrom<Meter>(grams));
        }

        [Fact]
        public void UnaryNegation() {
            var expected = Random.Long();
            Meter actual = expected;
            Assert.Equal(-actual, new Meter(-expected));
        }

        [Fact]
        public void BitwiseComplement() {
            var expected = Random.Long();
            Meter actual = expected;
            Assert.Equal(new Meter(~expected), ~actual);
        }

        [Fact]
        public void PreIncrement() {
            var expected = Random.Long();
            Meter actual = expected;

            ++expected;
            Assert.Equal(new Meter(expected), ++actual);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PostIncrement() {
            var expected = Random.Long();
            Meter actual = expected;

            Assert.Equal(new Meter(expected), actual++);
            expected++;
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PreDecrement() {
            var expected = Random.Long();
            Meter actual = expected;

            --expected;
            Assert.Equal(new Meter(expected), --actual);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PostDecrement() {
            var expected = Random.Long();
            Meter actual = expected;

            Assert.Equal(new Meter(expected), actual--);
            expected--;
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void Addition() {
            var expected1 = Random.Long();
            var expected2 = Random.Long();
            var actual = new Meter(expected1) + new Meter(expected2);
            Assert.Equal(expected1 + expected2, actual.Value);
        }

        [Fact]
        public void Subtraction() {
            var expected1 = Random.Long();
            var expected2 = Random.Long();
            var actual = new Meter(expected1) - new Meter(expected2);
            Assert.Equal(expected1 - expected2, actual.Value);
        }

        [Fact]
        public void Multiplication() {
            var expected1 = Random.Long();
            var expected2 = Random.Long();
            var actual = new Meter(expected1) * new Meter(expected2);
            Assert.Equal(expected1 * expected2, actual.Value);
        }

        [Fact]
        public void Division() {
            var expected1 = Random.Long();
            var expected2 = Random.Long();
            var actual = new Meter(expected1) / new Meter(expected2);
            Assert.Equal(expected1 / expected2, actual.Value);
        }

        [Fact]
        public void Modulus() {
            var expected1 = Random.Long();
            var expected2 = Random.Long();
            var actual = new Meter(expected1) % new Meter(expected2);
            Assert.Equal(expected1 % expected2, actual.Value);
        }

        [Fact]
        public void BitwiseOr() {
            var expected1 = Random.Long();
            var expected2 = Random.Long();
            var actual = new Meter(expected1) | new Meter(expected2);
            Assert.Equal(expected1 | expected2, actual.Value);
        }

        [Fact]
        public void BitwiseAnd() {
            var expected1 = Random.Long();
            var expected2 = Random.Long();
            var actual = new Meter(expected1) & new Meter(expected2);
            Assert.Equal(expected1 & expected2, actual.Value);
        }

        [Fact]
        public void BitwiseXor() {
            var expected1 = Random.Long();
            var expected2 = Random.Long();
            var actual = new Meter(expected1) ^ new Meter(expected2);
            Assert.Equal(expected1 ^ expected2, actual.Value);
        }

        [Fact]
        public void Equality() {
            var longValue = Random.Long();
            Assert.True(new Meter(longValue) == new Meter(longValue));
            Assert.False(new Meter(longValue) == new Meter(longValue - 1));
        }

        [Fact]
        public void Inequality() {
            var longValue = Random.Long();
            Assert.False(new Meter(longValue) != new Meter(longValue));
            Assert.True(new Meter(longValue) != new Meter(longValue - 1));
        }

        [Fact]
        public void LessThan() {
            var longValue = Random.Long();
            Assert.True(new Meter(longValue) < new Meter(longValue + 1));
            Assert.False(new Meter(longValue) < new Meter(longValue));
        }

        [Fact]
        public void GreaterThan() {
            var longValue = Random.Long();
            Assert.True(new Meter(longValue + 1) > new Meter(longValue));
            Assert.False(new Meter(longValue) > new Meter(longValue));
        }

        [Fact]
        public void LessThanOrEqual() {
            var longValue = Random.Long();
            Assert.True(new Meter(longValue) <= new Meter(longValue + 1));
            Assert.True(new Meter(longValue) <= new Meter(longValue));
            Assert.False(new Meter(longValue) <= new Meter(longValue - 1));
        }

        [Fact]
        public void GreaterThanOrEqual() {
            var longValue = Random.Long();
            Assert.False(new Meter(longValue) >= new Meter(longValue + 1));
            Assert.True(new Meter(longValue) >= new Meter(longValue));
            Assert.True(new Meter(longValue) >= new Meter(longValue - 1));
        }
    }
}