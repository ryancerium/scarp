using Newtonsoft.Json;
using Scarp.Tests;
using Xunit;
using Xunit.Sdk;

using FirstName = Scarp.Primitive.String<Scarp.Primitive.Tests.FirstNameTag>;
using MiddleName = Scarp.Primitive.String<Scarp.Primitive.Tests.MiddleNameTag>;
using LastName = Scarp.Primitive.String<Scarp.Primitive.Tests.LastNameTag>;

namespace Scarp.Primitive.Tests {
    public class FirstNameTag { }
    public class MiddleNameTag { }
    public class LastNameTag { }

    internal class Person {
        public FirstName f;
        public MiddleName? m;
        public LastName l;

        public override bool Equals(object obj) =>
            obj is Person other &&
            other.f == f &&
            other.m == m &&
            other.l == l;

        public override int GetHashCode() =>
            f.GetHashCode() ^
            m.GetHashCode() ^
            l.GetHashCode();
    }

    public class StringTest {
        [Fact]
        public void ToJsonTest() {
            var person = new Person { f = Random.String10(), m = null, l = Random.String10() };
            var actual = JsonConvert.SerializeObject(person);

            var expected = $"{{\"f\":\"{person.f}\",\"m\":null,\"l\":\"{person.l}\"}}";

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void FromJsonTest() {
            var expected = new Person { f = Random.String10(), m = null, l = Random.String10() };
            var json = $"{{\"f\":\"{expected.f}\",\"m\":null,\"l\":\"{expected.l}\"}}";
            var actual = JsonConvert.DeserializeObject<Person>(json);
            Assert.Equal(expected, actual);
        }
    }
}