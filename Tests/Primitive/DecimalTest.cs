using Newtonsoft.Json;
using Scarp.Tests;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Sdk;

using Math = System.Math;
using Meter = Scarp.Primitive.Decimal<Scarp.Primitive.Tests.DecimalTest.MeterTag>;
using Gram = Scarp.Primitive.Decimal<Scarp.Primitive.Tests.DecimalTest.GramTag>;

namespace Scarp.Primitive.Tests {
    public class DecimalTest {
        public class MeterTag {
        }
        public class GramTag {
        }

        internal class Location {
            private const decimal EPSILON = (decimal) 0.000001;

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
            var location = new Location { x = Random.Decimal(), y = Random.Decimal() };
            var actual = JsonConvert.SerializeObject(location);

            var x = $"{location.x:0.########}".SubstringMaxLength(0, 8);
            var y = $"{location.y:0.########}".SubstringMaxLength(0, 8);
            var regex = new Regex($@"{{""x"":{x}\d*,""y"":{y}\d*}}");
            Assert.True(regex.IsMatch(actual), $"'{actual}' did not match pattern: {regex}");
        }

        [Fact]
        public void FromJsonTest() {
            var expected = new Location { x = Random.Decimal(), y = Random.Decimal() };
            var json = $@"{{""x"":{expected.x},""y"":{expected.y}}}";
            var actual = JsonConvert.DeserializeObject<Location>(json);
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Assignment() {
            Gram grams = Random.Decimal();
            Assert.Throws<IsAssignableFromException>(() =>
                Assert.IsAssignableFrom<Meter>(grams));
        }

        [Fact]
        public void UnaryNegation() {
            var expected = Random.Decimal();
            Meter actual = expected;
            Assert.Equal(-actual, new Meter(-expected));
        }

        [Fact]
        public void PreIncrement() {
            var expected = Random.Decimal();
            Meter actual = expected;

            ++expected;
            Assert.Equal(new Meter(expected), ++actual);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PostIncrement() {
            var expected = Random.Decimal();
            Meter actual = expected;

            Assert.Equal(new Meter(expected), actual++);
            expected++;
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PreDecrement() {
            var expected = Random.Decimal();
            Meter actual = expected;

            --expected;
            Assert.Equal(new Meter(expected), --actual);
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void PostDecrement() {
            var expected = Random.Decimal();
            Meter actual = expected;

            Assert.Equal(new Meter(expected), actual--);
            expected--;
            Assert.Equal(expected, actual.Value);
        }

        [Fact]
        public void Addition() {
            var expected1 = Random.Decimal();
            var expected2 = Random.Decimal();
            var actual = new Meter(expected1) + new Meter(expected2);
            Assert.Equal(expected1 + expected2, actual.Value);
        }

        [Fact]
        public void Subtraction() {
            var expected1 = Random.Decimal();
            var expected2 = Random.Decimal();
            var actual = new Meter(expected1) - new Meter(expected2);
            Assert.Equal(expected1 - expected2, actual.Value);
        }

        [Fact]
        public void Multiplication() {
            var expected1 = Random.Decimal();
            var expected2 = Random.Decimal();
            var actual = new Meter(expected1) * new Meter(expected2);
            Assert.Equal(expected1 * expected2, actual.Value);
        }

        [Fact]
        public void Division() {
            var expected1 = Random.Decimal();
            var expected2 = Random.Decimal();
            var actual = new Meter(expected1) / new Meter(expected2);
            Assert.Equal(expected1 / expected2, actual.Value);
        }

        [Fact]
        public void Modulus() {
            var expected1 = Random.Decimal();
            var expected2 = Random.Decimal();
            var actual = new Meter(expected1) % new Meter(expected2);
            Assert.Equal(expected1 % expected2, actual.Value);
        }

        [Fact]
        public void Equality() {
            var decimalValue = Random.Decimal();
            Assert.True(new Meter(decimalValue) == new Meter(decimalValue));
            Assert.False(new Meter(decimalValue) == new Meter(decimalValue - 1));
        }

        [Fact]
        public void Inequality() {
            var decimalValue = Random.Decimal();
            Assert.False(new Meter(decimalValue) != new Meter(decimalValue));
            Assert.True(new Meter(decimalValue) != new Meter(decimalValue - 1));
        }

        [Fact]
        public void LessThan() {
            var decimalValue = Random.Decimal();
            Assert.True(new Meter(decimalValue) < new Meter(decimalValue + 1));
            Assert.False(new Meter(decimalValue) < new Meter(decimalValue));
        }

        [Fact]
        public void GreaterThan() {
            var decimalValue = Random.Decimal();
            Assert.True(new Meter(decimalValue + 1) > new Meter(decimalValue));
            Assert.False(new Meter(decimalValue) > new Meter(decimalValue));
        }

        [Fact]
        public void LessThanOrEqual() {
            var decimalValue = Random.Decimal();
            Assert.True(new Meter(decimalValue) <= new Meter(decimalValue + 1));
            Assert.True(new Meter(decimalValue) <= new Meter(decimalValue));
            Assert.False(new Meter(decimalValue) <= new Meter(decimalValue - 1));
        }

        [Fact]
        public void GreaterThanOrEqual() {
            var decimalValue = Random.Decimal();
            Assert.False(new Meter(decimalValue) >= new Meter(decimalValue + 1));
            Assert.True(new Meter(decimalValue) >= new Meter(decimalValue));
            Assert.True(new Meter(decimalValue) >= new Meter(decimalValue - 1));
        }
    }
}