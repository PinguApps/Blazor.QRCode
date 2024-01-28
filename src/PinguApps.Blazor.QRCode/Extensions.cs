using System.Reflection;
using System.Runtime.Serialization;

namespace PinguApps.Blazor.QRCode;

internal static class Extensions
{
    public static string? GetEnumMemberValue<T>(this T value)
        where T : Enum
    {
        return typeof(T)
            .GetTypeInfo()
            .DeclaredMembers
            .FirstOrDefault(x => x.Name == value.ToString())
            ?.GetCustomAttribute<EnumMemberAttribute>(false)
            ?.Value;
    }
}