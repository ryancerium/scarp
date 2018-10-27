using System;
using System.IO;
using System.Reflection;

namespace PrimitiveGenerator {
    public class PrimitiveGenerator {
        private const string PrimitiveClass = "PrimitiveClass";
        private const string PrimitiveType = "PrimitiveType";

        static void Main(string[] args) {
            var primitive = new PrimitiveGenerator();
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

        private static string GetResource(string resourceName) {
            var assembly = Assembly.GetExecutingAssembly();
            using (var stream = new StreamReader(assembly.GetManifestResourceStream($"PrimitiveGenerator.Templates.{resourceName}"))) {
                return stream.ReadToEnd();
            }
        }
    }
}