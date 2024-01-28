using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace PinguApps.Blazor.QRCode;

[JsonConverter(typeof(DescriptionEnumConverter<ErrorCorrection>))]
public enum ErrorCorrection
{
    [EnumMember(Value = "L")]
    Low,
    [EnumMember(Value = "M")]
    Medium,
    [EnumMember(Value = "Q")]
    Quartile,
    [EnumMember(Value = "H")]
    High
}

public class DescriptionEnumConverter<T> : JsonConverter<T> where T : Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException("Reading has not been implemented.");
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.GetEnumMemberValue<T>());
    }
}
