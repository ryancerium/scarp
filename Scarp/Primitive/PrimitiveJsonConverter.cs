using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Scarp.Primitive {
    public class PrimitiveJsonConverter : JsonConverter {
        public override bool CanConvert(Type t) {
            // Using Int<T> as a sample type, but applies to all Scarp.Primitive types.
            // Strip a Nullable<Int<T>> to a Int<T> if necessary
            t = t.GetUnderlyingNullableType();
            if (!t.IsGenericType) {
                return false;
            }

            // Int<T> -> Int<>
            t = t.GetGenericTypeDefinition();

            return
                t == typeof(Int<>) || t == typeof(UInt<>) ||
                t == typeof(Long<>) || t == typeof(ULong<>) ||
                t == typeof(Float<>) || t == typeof(Double<>) ||
                t == typeof(Decimal<>) || t == typeof(String<>);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            if(reader.Value == null) {
                return null;
            }

            ConstructorInfo constructor = null;
            object value = null;

            var type = objectType.GetUnderlyingNullableType();
            var generic = type.GetGenericTypeDefinition();

            if (generic == typeof(Int<>)) {
                constructor = type.GetConstructor(new Type[] { typeof(int) });
                value = Convert.ToInt32(reader.Value);
            } else if (generic == typeof(UInt<>)) {
                constructor = type.GetConstructor(new Type[] { typeof(uint) });
                value = Convert.ToUInt32(reader.Value);
            } else if (generic == typeof(Long<>)) {
                constructor = type.GetConstructor(new Type[] { typeof(long) });
                value = Convert.ToInt64(reader.Value);
            } else if (generic == typeof(ULong<>)) {
                constructor = type.GetConstructor(new Type[] { typeof(ulong) });
                value = Convert.ToUInt64(reader.Value);
            } else if (generic == typeof(Float<>)) {
                constructor = type.GetConstructor(new Type[] { typeof(float) });
                value = (float) Convert.ToDouble(reader.Value);
            } else if (generic == typeof(Double<>)) {
                constructor = type.GetConstructor(new Type[] { typeof(double) });
                value = Convert.ToDouble(reader.Value);
            } else if (generic == typeof(Decimal<>)) {
                constructor = type.GetConstructor(new Type[] { typeof(decimal) });
                value = Convert.ToDecimal(reader.Value);
            } else if (generic == typeof(String<>)) {
                constructor = type.GetConstructor(new Type[] { typeof(string) });
                value = reader.Value;
            } else {
                throw new Exception($"Unknown type: {objectType.Name}");
            }

            return constructor.Invoke(new object[] { value });
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (value == null) {
                writer.WriteNull();
                return;
            }
            dynamic dValue = value;
            dynamic dValueValue = dValue.Value;
            writer.WriteValue(dValueValue);
        }
    }

    internal static class JsonExt {
        public static Type GetUnderlyingNullableType(this Type t) {
            return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                ? Nullable.GetUnderlyingType(t)
                : t;
        }
    }
}