using CQRS.Core.Events;

using Person.Common.Events;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Persons.Query.Infrastructure.Converters;
public class EventJsonConvert : JsonConverter<BaseEvent> {
    public override bool CanConvert(Type typeToConvert) {
        return typeToConvert.IsAssignableFrom(typeof(BaseEvent));
    }

    public override BaseEvent? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        if(!JsonDocument.TryParseValue(ref reader, out var doc))
            throw new JsonException($"Fallo al parsear {nameof(JsonDocument)}");

        if(!doc.RootElement.TryGetProperty("Type", out var type))
            throw new JsonException($"no se puede detectar el tipo discriminador de la propiedad");

        var typeDiscriminator = type.GetString();
        var json = doc.RootElement.GetRawText();
        return typeDiscriminator switch {
            nameof(PersonCreatedEvent) => JsonSerializer.Deserialize<PersonCreatedEvent>(json, options),
            nameof(PersonUpdatedEvent) => JsonSerializer.Deserialize<PersonUpdatedEvent>(json, options),
            nameof(PersonDeletedEvent) => JsonSerializer.Deserialize<PersonDeletedEvent>(json, options),
            nameof(IdentityDocumentAddedEvent) => JsonSerializer.Deserialize<IdentityDocumentAddedEvent>(json, options),
            nameof(IdentityDocumentUpdateEvent) => JsonSerializer.Deserialize<IdentityDocumentUpdateEvent>(json, options),
            nameof(IdentityDocumentRemovedEvent) => JsonSerializer.Deserialize<IdentityDocumentRemovedEvent>(json, options),
            _ => throw new JsonException($"{typeDiscriminator} no esta soportado")
        };
    }

    public override void Write(Utf8JsonWriter writer, BaseEvent value, JsonSerializerOptions options) {
        throw new NotImplementedException();
    }
}