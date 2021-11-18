using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;

internal class WorkersConverter : JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(Worker);

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jsonReader = JObject.Load(reader);
        var typeName = jsonReader["$type"].Value<string>();
        if (typeName.Contains("IngredientProcess"))
        {
            return jsonReader.ToObject<Cook>(serializer);
        }
        else if (typeName.Contains("IntermideateProcess"))
        {
            return jsonReader.ToObject<Chief>(serializer);
        }
        return null;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {

    }
}
