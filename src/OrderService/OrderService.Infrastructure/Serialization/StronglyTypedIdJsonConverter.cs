using System.Text.Json;
using System.Text.Json.Serialization;
using OrderService.Domain.ValueObjects;

namespace OrderService.Infrastructure.Serialization;

public class StronglyTypedIdJsonConverter<TStronglyTypedId, TValue>
    : JsonConverter<TStronglyTypedId>
    where TStronglyTypedId : StronglyTypedId<TValue>
    where TValue : struct, IEquatable<TValue>
{
    public override TStronglyTypedId Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options
    )
    {
        var value = JsonSerializer.Deserialize<TValue>(ref reader, options);
        return (TStronglyTypedId)Activator.CreateInstance(typeof(TStronglyTypedId), value);
    }

    public override void Write(
        Utf8JsonWriter writer,
        TStronglyTypedId value,
        JsonSerializerOptions options
    )
    {
        JsonSerializer.Serialize(writer, value.Value, options);
    }
}
