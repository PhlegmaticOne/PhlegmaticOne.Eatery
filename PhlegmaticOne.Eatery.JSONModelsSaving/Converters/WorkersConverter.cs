using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;
/// <summary>
/// Json converter of worker abstract type to worker concrete types
/// </summary>
internal class WorkersConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(Worker);
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jsonReader = JObject.Load(reader);
        var typeName = jsonReader["$type"].Value<string>();
        if (typeName.Contains("Cook"))
        {
            return jsonReader.ToObject<Cook>(serializer);
        }
        else if (typeName.Contains("Chief"))
        {
            return jsonReader.ToObject<Chief>(serializer);
        }
        else if (typeName.Contains("Manager"))
        {
            return jsonReader.ToObject<Manager>(serializer);
        }
        return null;
    }
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) { }
    public override string ToString() => GetType().Name;
}
