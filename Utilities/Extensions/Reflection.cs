using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace AndiSoft.Utilities.Extensions
{
    public static class Reflection
    {
        public static string ToShortString(this MethodInfo method)
        {
            if (method == null) { throw new ArgumentNullException(nameof(method)); }

            var indentWidth = 4;
            var indent = new Func<int, string>(depth => new string(' ', indentWidth * depth));

            var parameters = method.GetParameters().Select(p => $"{p.ParameterType} {p.Name}");

            var accessModifier = new[]
            {
            method.IsPublic ? "public" : string.Empty,
            method.IsAssembly ? "internal" : string.Empty,
            method.IsPrivate ? "private" : string.Empty,
            method.IsFamily ? "protected" : string.Empty,
        }
            .First(x => !string.IsNullOrEmpty(x));

            var inheritanceModifier = new[]
            {
            method.IsAbstract ? " abstract" : string.Empty,
            method.IsVirtual ? " virtual" : string.Empty,
            method.GetBaseDefinition() != method ? " override" : string.Empty,
        }
            .FirstOrDefault(x => !string.IsNullOrEmpty(x));

            var signature = new StringBuilder()
                .Append(method.DeclaringType?.FullName)
                .Append(" { ")
                .Append(accessModifier)
                .Append(method.IsStatic ? " static" : string.Empty)
                .Append(inheritanceModifier)
                .Append(method.GetCustomAttribute<AsyncStateMachineAttribute>() != null ? " async" : string.Empty)
                .Append(" ").Append(method.ReturnType)
                .Append(" ").Append(method.Name)
                .Append("(").Append(string.Join(", ", parameters)).Append(") { ... }")
                .Append(" } ")
                .ToString();

            return signature;
        }
    }
}