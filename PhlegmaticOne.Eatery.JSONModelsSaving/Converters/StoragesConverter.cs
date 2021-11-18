using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;
/// <summary>
/// Json converter of storage abstract type to storages concrete types
/// </summary>
internal class StoragesConverter : Newtonsoft.Json.JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(Storage);

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jsonReader = JObject.Load(reader);
        var typeName = jsonReader["$type"].Value<string>();
        if (typeName.Contains("Cellar"))
        {
            return jsonReader.ToObject<Cellar>(serializer);
        }
        return null;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) { }
    public override string ToString() => GetType().Name;
}
