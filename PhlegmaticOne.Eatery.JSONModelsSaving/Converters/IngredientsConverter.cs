using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;

internal class IngredientsConverter : Newtonsoft.Json.JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(Ingredient);

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jsonReader = JObject.Load(reader);
        var typeName = jsonReader["$type"].Value<string>();
        if (typeName.Contains("Cucumber"))
        {
            return jsonReader.ToObject<Cucumber>(serializer);
        }
        else if (typeName.Contains("Tomato"))
        {
            return jsonReader.ToObject<Tomato>(serializer);
        }
        else if (typeName.Contains("Olive"))
        {
            return jsonReader.ToObject<Olive>(serializer);
        }
        else if (typeName.Contains("OliveOil"))
        {
            return jsonReader.ToObject<OliveOil>(serializer);
        }
        return null;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {

    }
}
