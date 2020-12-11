using System;
using System.Linq;
using System.Reflection;

namespace Configuration.ProxyFactory
{
    internal static class Exceptions
    {
        internal static ArgumentException CannotCreateProxyOfNonInterfaceType(Type type) =>
            new ArgumentException($"Cannot create proxy instance of non-interface type {type}.", nameof(type));

        internal static ArgumentException TargetInterfaceCannotHaveAnyMethods(Type type, MethodInfo methodInfo) =>
            new ArgumentException(
                $"Cannot create proxy {type} implementation: target interface cannot contain any methods. `{methodInfo}`",
                nameof(type));

        internal static ArgumentException TargetInterfaceCannotHaveAnyEvents(Type type, EventInfo eventInfo) =>
            new ArgumentException(
                $"Cannot create proxy {type} implementation: target interface cannot contain any events. `{eventInfo}`",
                nameof(type));

        internal static ArgumentException TargetInterfaceCannotHaveAnyIndexerProperties(Type type,
            PropertyInfo propertyInfo) =>
            new ArgumentException(
                $"Cannot create proxy {type} implementation: target interface cannot contain any indexer properties. `{propertyInfo.PropertyType.Name} this[{propertyInfo.GetIndexParameters().Select(i => i.ParameterType.Name).StringJoin(",")}] {{ {(propertyInfo.CanRead ? "get; " : "")} {(propertyInfo.CanWrite ? "set; " : "")}}}`",
                nameof(type));

        internal static ArgumentException TargetInterfaceCannotHaveAnyWriteOnlyProperties(Type type,
            PropertyInfo propertyInfo) =>
            new ArgumentException(
                $"Cannot create proxy {type} implementation: target interface cannot contain write-only methods. `{propertyInfo} {{ set; }}`",
                nameof(type));
    }
}
