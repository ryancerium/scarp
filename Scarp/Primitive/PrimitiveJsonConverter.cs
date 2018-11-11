using System;
using System.Reflection;
using Newtonsoft.Json;

namespace Scarp.Primitive {
    public class PrimitiveJsonConverter : JsonConverter {
        public override bool CanConvert(Type t) {
            // Uses Int<T> as a sample type, but applies to all Scarp.Primitive types.

            // Reduce a Nullable<Int<T>> to a Int<T> if necessary
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
            if (reader.Value == null) {
                return null;
            }

            // Int<YearTag>, with YearTag as a sample tag type
            var closedType = objectType.GetUnderlyingNullableType();

            // Int<>, with no tag type
            var openType = closedType.GetGenericTypeDefinition();

            // integer values are read from JSON as Int64
            // floating point values are read from JSON as Double
            if (openType == typeof(Int<>)) {
                return closedType
                    .GetConstructor(typeof(int))
                    .Invoke(Convert.ToInt32(reader.Value));
            } else if (openType == typeof(UInt<>)) {
                return closedType
                    .GetConstructor(typeof(uint))
                    .Invoke(Convert.ToUInt32(reader.Value));
            } else if (openType == typeof(Long<>)) {
                return closedType
                    .GetConstructor(typeof(long))
                    .Invoke(reader.Value);
            } else if (openType == typeof(ULong<>)) {
                return closedType
                    .GetConstructor(typeof(ulong))
                    .Invoke(Convert.ToUInt64(reader.Value));
            } else if (openType == typeof(Float<>)) {
                return closedType
                    .GetConstructor(typeof(float))
                    .Invoke(Convert.ToSingle(reader.Value));
            } else if (openType == typeof(Double<>)) {
                return closedType
                    .GetConstructor(typeof(double))
                    .Invoke(Convert.ToDouble(reader.Value));
            } else if (openType == typeof(Decimal<>)) {
                return closedType
                    .GetConstructor(typeof(decimal))
                    .Invoke(Convert.ToDecimal(reader.Value));
            } else if (openType == typeof(String<>)) {
                return closedType
                    .GetConstructor(typeof(string))
                    .Invoke(reader.Value);
            } else {
                throw new Exception($"Unknown type: {objectType.Name}");
            }
        }

        readonly object[] p = new object[] { };

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (value == null) {
                writer.WriteNull();
                return;
            }

            var closedType = value.GetType();
            var property = closedType.GetProperty("Value");
            writer.WriteValue(property.GetValue(value));
        }
    }

    internal static class JsonExt {
        public static Type GetUnderlyingNullableType(this Type t) {
            return (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>))
                ? Nullable.GetUnderlyingType(t)
                : t;
        }

        public static object Invoke(this ConstructorInfo constructor, params object[] types) =>
            constructor.Invoke(types);

        public static ConstructorInfo GetConstructor(this Type type, params Type[] types) =>
            type.GetConstructor(types);
    }
}