using Newtonsoft.Json;
using Scarp.Tests;
using Xunit;
using Xunit.Sdk;

using Meter = Scarp.Primitive.UInt<Scarp.Primitive.Tests.UIntTest.MeterTag>;
using Gram = Scarp.Primitive.UInt<Scarp.Primitive.Tests.UIntTest.GramTag>;

namespace Scarp.Primitive.Tests {
    public class UIntTest {
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
            var location = new Location { x = Random.UInt(), y = Random.UInt() };
            var actual = JsonConvert.SerializeObject(location);

            var expected = $"{{\"x\":{location.x},\"y\":{location.y}}}";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromJsonTest() {
            var expected = new Location { x = Random.UInt(), y = Random.UInt() };
            var json = $"{{\"x\":{expected.x},\"y\":{expected.y}}}";
            var actual = JsonConvert.DeserializeObject<Location>(json);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Assignment() {
            Gram grams = Random.UInt();
            Assert.Throws<IsAssignableFromException>(() =>
                Assert.IsAssignableFrom<Meter>(grams));
        }

        [Fact]
        public void BitwiseComplement() {
            var expected = Random.UInt();
            Meter actual = expected;
            Assert.Equal(new Meter(~expected), ~actual);
        }

        [Fact]
        public void PreIncrement() {
            var expected = Random.UInt();
            Meter actual = expected;

            ++expected;
            Assert.Equal(new Meter(expected), ++actual);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PostIncrement() {
            var expected = Random.UInt();
            Meter actual = expected;

            Assert.Equal(new Meter(expected), actual++);
            expected++;
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PreDecrement() {
            var expected = Random.UInt();
            Meter actual = expected;

            --expected;
            Assert.Equal(new Meter(expected), --actual);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PostDecrement() {
            var expected = Random.UInt();
            Meter actual = expected;

            Assert.Equal(new Meter(expected), actual--);
            expected--;
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void Addition() {
            var expected1 = Random.UInt();
            var expected2 = Random.UInt();
            var actual = new Meter(expected1) + new Meter(expected2);
            Assert.Equal(expected1 + expected2, actual.Value);
        }

        [Fact]
        public void Subtraction() {
            var expected1 = Random.UInt();
            var expected2 = Random.UInt();
            var actual = new Meter(expected1) - new Meter(expected2);
            Assert.Equal(expected1 - expected2, actual.Value);
        }

        [Fact]
        public void Multiplication() {
            var expected1 = Random.UInt();
            var expected2 = Random.UInt();
            var actual = new Meter(expected1) * new Meter(expected2);
            Assert.Equal(expected1 * expected2, actual.Value);
        }

        [Fact]
        public void Division() {
            var expected1 = Random.UInt();
            var expected2 = Random.UInt();
            var actual = new Meter(expected1) / new Meter(expected2);
            Assert.Equal(expected1 / expected2, actual.Value);
        }

        [Fact]
        public void Modulus() {
            var expected1 = Random.UInt();
            var expected2 = Random.UInt();
            var actual = new Meter(expected1) % new Meter(expected2);
            Assert.Equal(expected1 % expected2, actual.Value);
        }

        [Fact]
        public void BitwiseOr() {
            var expected1 = Random.UInt();
            var expected2 = Random.UInt();
            var actual = new Meter(expected1) | new Meter(expected2);
            Assert.Equal(expected1 | expected2, actual.Value);
        }

        [Fact]
        public void BitwiseAnd() {
            var expected1 = Random.UInt();
            var expected2 = Random.UInt();
            var actual = new Meter(expected1) & new Meter(expected2);
            Assert.Equal(expected1 & expected2, actual.Value);
        }

        [Fact]
        public void BitwiseXor() {
            var expected1 = Random.UInt();
            var expected2 = Random.UInt();
            var actual = new Meter(expected1) ^ new Meter(expected2);
            Assert.Equal(expected1 ^ expected2, actual.Value);
        }

        [Fact]
        public void Equality() {
            var uintValue = Random.UInt();
            Assert.True(new Meter(uintValue) == new Meter(uintValue));
            Assert.False(new Meter(uintValue) == new Meter(uintValue - 1));
        }

        [Fact]
        public void Inequality() {
            var uintValue = Random.UInt();
            Assert.False(new Meter(uintValue) != new Meter(uintValue));
            Assert.True(new Meter(uintValue) != new Meter(uintValue - 1));
        }

        [Fact]
        public void LessThan() {
            var uintValue = Random.UInt();
            Assert.True(new Meter(uintValue) < new Meter(uintValue + 1));
            Assert.False(new Meter(uintValue) < new Meter(uintValue));
        }

        [Fact]
        public void GreaterThan() {
            var uintValue = Random.UInt();
            Assert.True(new Meter(uintValue + 1) > new Meter(uintValue));
            Assert.False(new Meter(uintValue) > new Meter(uintValue));
        }

        [Fact]
        public void LessThanOrEqual() {
            var uintValue = Random.UInt();
            Assert.True(new Meter(uintValue) <= new Meter(uintValue + 1));
            Assert.True(new Meter(uintValue) <= new Meter(uintValue));
            Assert.False(new Meter(uintValue) <= new Meter(uintValue - 1));
        }

        [Fact]
        public void GreaterThanOrEqual() {
            var uintValue = Random.UInt();
            Assert.False(new Meter(uintValue) >= new Meter(uintValue + 1));
            Assert.True(new Meter(uintValue) >= new Meter(uintValue));
            Assert.True(new Meter(uintValue) >= new Meter(uintValue - 1));
        }
    }
}