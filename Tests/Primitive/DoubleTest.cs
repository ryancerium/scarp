using Newtonsoft.Json;
using Scarp.Tests;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Sdk;

using Math = System.Math;
using Meter = Scarp.Primitive.Double<Scarp.Primitive.Tests.DoubleTest.MeterTag>;
using Gram = Scarp.Primitive.Double<Scarp.Primitive.Tests.DoubleTest.GramTag>;

namespace Scarp.Primitive.Tests {
    public class DoubleTest {
        public class MeterTag {
        }
        public class GramTag {
        }

        internal class Location {
            private const double EPSILON = (double) 0.000001;

            public Meter x;
            public Meter? y;

            public override bool Equals(object obj) =>
                obj is Location other &&
                Math.Abs((x - other.x).Value) < EPSILON &&
                Math.Abs((y.Value - other.y.Value).Value) < EPSILON;

            public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();
        }

        [Fact]
        public void ToJsonTest() {
            var location = new Location { x = Random.Double(), y = Random.Double() };
            var actual = JsonConvert.SerializeObject(location);

            var x = $"{location.x:0.########}".SubstringMaxLength(0, 8);
            var y = $"{location.y:0.########}".SubstringMaxLength(0, 8);
            var regex = new Regex($@"{{""x"":{x}\d*,""y"":{y}\d*}}");
            Assert.True(regex.IsMatch(actual), $"'{actual}' did not match pattern: {regex}");
        }

        [Fact]
        public void FromJsonTest() {
            var expected = new Location { x = Random.Double(), y = Random.Double() };
            var json = $"{{\"x\":{expected.x},\"y\":{expected.y}}}";
            var actual = JsonConvert.DeserializeObject<Location>(json);
            Assert.Equal(expected, actual);
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