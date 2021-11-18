using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhlegmaticOne.Eatery.Lib.Dishes;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;
/// <summary>
/// Json converter of dish abstract type to dishes concrete types
/// </summary>
internal class DishesConverter : Newtonsoft.Json.JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(DishBase);
    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jsonReader = JObject.Load(reader);
        var typeName = jsonReader["$type"].Value<string>();
        if (typeName.Contains("Dish"))
        {
            return jsonReader.ToObject<Dish>(serializer);
        }
        else if (typeName.Contains("Drink"))
        {
            return jsonReader.ToObject<Drink>(serializer);
        }
        return null;
    }
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer) { }
    public override string ToString() => GetType().Name;
}
