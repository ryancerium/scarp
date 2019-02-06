using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Scarp.AspNetCore {
    public class ScarpModelBinderProvider : IModelBinderProvider {
        public IModelBinder GetBinder(ModelBinderProviderContext context) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            return IsScarpBoundType(context.Metadata.ModelType)
                ? new ScarpModelBinder(context.Metadata.ModelType)
                : null;
        }

        private static IEnumerable<Type> ScarpBoundTypes = new[] {
            typeof(AspInt<>),
            typeof(AspUInt<>),
            typeof(AspLong<>),
            typeof(AspULong<>),
            typeof(AspFloat<>),
            typeof(AspDouble<>),
            typeof(AspDecimal<>)
        };

        private static bool IsScarpBoundType(Type type) =>
            type.IsGenericType && ScarpBoundTypes.Contains(type.GetGenericTypeDefinition());
    }
}