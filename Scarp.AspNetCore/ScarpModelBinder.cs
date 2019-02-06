using Microsoft.AspNetCore.Mvc.ModelBinding;
using Scarp.Results;
using System;
using System.Threading.Tasks;

namespace Scarp.AspNetCore {
    public class ScarpModelBinder : IModelBinder {
        private Type ResultType { get; set; }

        public ScarpModelBinder(Type type) => ResultType = type;

        public Task BindModelAsync(ModelBindingContext bindingContext) {
            if (bindingContext == null) {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            // Try to fetch the value of the argument by name
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None) {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

            var resultTypeConstructor = ResultType.GetConstructor(new[] { typeof(string) });

            try {
                var result = resultTypeConstructor.Invoke(new object[] { valueProviderResult.FirstValue });
                bindingContext.Result = ModelBindingResult.Success(result);
            } catch (FormatException e) {
                // Invalid arguments result in model state errors
                bindingContext.ModelState.TryAddModelError(
                    bindingContext.ModelName,
                    $"'value' was not convertible to a {ResultType.Name}\n{e.Message}");
            } catch (OverflowException e) {
                // Invalid arguments result in model state errors
                bindingContext.ModelState.TryAddModelError(
                    bindingContext.ModelName,
                    $"'value' was not convertible to a {ResultType.Name}\n{e.Message}");
            }

            return Task.CompletedTask;
        }
    }
}
