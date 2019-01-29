using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace PrimitiveGenerator {
    public class PrimitiveGenerator {
        private const string PrimitiveClass = "PrimitiveClass";
        private const string PrimitiveType = "PrimitiveType";

        private static string Value { get; } = "Hello";
        static void Main(string[] args) {
            if (args.Length == 0 || args.Contains("--help") || args.Contains("-h") || args.Contains("/?")) {
                Console.WriteLine(@"Usage: PrimitiveGenerator [all] [efcore] [primitive] [string] [--help|-h|/?]");
            }

            var all = args.Contains("all");
            var generateEfcore = all || args.Contains("efcore");
            var generatePrimitive = all || args.Contains("primitive");
            var generateString = all || args.Contains("string");

            var primitive = new PrimitiveGenerator();

            if (generateEfcore) {
                primitive.GenerateValueConverters();
            }

            if (generatePrimitive) {
                primitive.GenerateClass("UInt", Unsigned);
                primitive.GenerateClass("ULong", Unsigned);
                primitive.GenerateClass("Int", Signed);
                primitive.GenerateClass("Long", Signed);
                primitive.GenerateClass("Decimal", FloatingPoint);
                primitive.GenerateClass("Double", FloatingPoint);
                primitive.GenerateClass("Float", FloatingPoint);

                primitive.GenerateTests("UInt", UnsignedTest);
                primitive.GenerateTests("ULong", UnsignedTest);
                primitive.GenerateTests("Int", SignedTest);
                primitive.GenerateTests("Long", SignedTest);
                primitive.GenerateTests("Decimal", FloatingPointTest);
                primitive.GenerateTests("Double", FloatingPointTest);
                primitive.GenerateTests("Float", FloatingPointTest);
            }

            if (generateString) {
                primitive.GenerateString();
            }
        }

        private static string Signed { get; } = GetResource("Signed.txt");
        private static string Unsigned { get; } = GetResource("Unsigned.txt");
        private static string FloatingPoint { get; } = GetResource("FloatingPoint.txt");
        private static string SignedTest { get; } = GetResource("SignedTest.txt");
        private static string UnsignedTest { get; } = GetResource("UnsignedTest.txt");
        private static string FloatingPointTest { get; } = GetResource("FloatingPointTest.txt");

        private void GenerateClass(string className, string template) {
            Generate(className, template, File.Create($"../Scarp/Primitive/{className}.cs"));
        }
        private void GenerateTests(string className, string template) {
            Generate(className, template, File.Create($"../Tests/Primitive/{className}Test.cs"));
        }

        private void Generate(string className, string template, FileStream output) {
            using (var streamWriter = new StreamWriter(output)) {
                streamWriter.Write(
                    template.Replace(PrimitiveClass, className)
                            .Replace(PrimitiveType, className.ToLower()));
            }
        }

        private void GenerateValueConverters() {
            using (var streamWriter = new StreamWriter(File.Create("../Scarp.EntityFrameworkCore/ScarpValueConverters.cs"))) {
                streamWriter.Write(@"using Scarp.Primitive;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Scarp.EntityFrameworkCore.Storage.ValueConversion {");

                streamWriter.Write(string.Join(@"
",
                new[] { "Int", "UInt", "Long", "ULong", "Float", "Double", "Decimal" }.Select(type =>
                     @"
    public class PrimitiveClassValueConverter<Tag> : ValueConverter<PrimitiveClass<Tag>, PrimitiveType> {
        public PrimitiveClassValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new PrimitiveClass<Tag>(e), mappingHints) { }
    }

    public class NullablePrimitiveClassValueConverter<Tag> : ValueConverter<PrimitiveClass<Tag>?, PrimitiveType?> {
        public NullablePrimitiveClassValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                e => e.HasValue ? e.Value.Value : (PrimitiveType?) null,
                e => e.HasValue ? new PrimitiveClass<Tag>(e.Value) : (PrimitiveClass<Tag>?) null,
                mappingHints) { }
    }".Replace(PrimitiveClass, type)
        .Replace(PrimitiveType, type.ToLower()))));

                streamWriter.Write(@"

    public class StringValueConverter<Tag> : ValueConverter<String<Tag>, string> {
        public StringValueConverter(ConverterMappingHints mappingHints = null)
            : base(e => e.Value, e => new String<Tag>(e), mappingHints) { }
    }

    public class NullableStringValueConverter<Tag> : ValueConverter<String<Tag>?, string> {
        public NullableStringValueConverter(ConverterMappingHints mappingHints = null)
            : base(
                e => e.HasValue ? e.Value.Value : null,
                e => new String<Tag>(e),
                mappingHints) { }
    }
}");
            }
        }

        private void GenerateString() {
            using (var streamWriter = new StreamWriter(File.Create("../Scarp/Primitive/StringGenerated.cs"))) {
                streamWriter.Write(@"using System;
using System.Globalization;
using System.Text;

namespace Scarp.Primitive {
    public partial struct String<Tag> {");
                var stringType = typeof(string);

                foreach (var method in stringType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public)) {
                    if (method.Name.Contains('_') || method.IsVirtual) {
                        continue;
                    }

                    var returnType = method.GetReturnTypeName();
                    var toStrArray = returnType == "String<Tag>[]" ? ".ToStringArray<Tag>()" : "";
                    var genericArgs = "";
                    if (method.IsGenericMethod) {
                        genericArgs = string.Join(", ", method.GetGenericArguments().Select(arg => arg.Name));
                        genericArgs = $"<{genericArgs}>";
                    }
                    var arguments = string.Join(", ", method.GetParameters().Select(p => $"{p.GetParameterTypeName()} {p.Name}"));
                    var parameters = string.Join(", ", method.GetParameters().Select(p => p.Name));

                    streamWriter.Write($"");
                    streamWriter.Write($@"
        public {returnType} {method.Name}{genericArgs}({arguments}) => Value.{method.Name}({parameters}){toStrArray};");
                }

                streamWriter.Write(@"
    }
}");
            }
        }

        private static string GetResource(string resourceName) {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = new StreamReader(assembly.GetManifestResourceStream($"PrimitiveGenerator.Templates.{resourceName}"))) {
                return stream.ReadToEnd();
            }
        }
    }

    internal static class ReflectionExt {
        public static string JoinWith(this IEnumerable<string> strings, string separator) =>
            string.Join(separator, strings);

        public static string GetReturnTypeName(this MethodInfo methodInfo) {
            var typeName = GetTypeName(methodInfo.ReturnType);
            return
                typeName == "string" ? "String<Tag>" :
                typeName == "string[]" ? "String<Tag>[]" :
                typeName;
        }

        public static string GetParameterTypeName(this ParameterInfo parameterInfo) =>
            GetTypeName(parameterInfo.ParameterType);

        public static string GetTypeName(this Type typeInfo) {
            switch (typeInfo.Name) {
                case "Boolean":
                    return "bool";
                case "Int32":
                    return "int";
                case "Char":
                case "String":
                case "String[]":
                case "Void":
                    return typeInfo.Name.ToLower();
                default:
                    return typeInfo.Name;
            }
        }
    }
}