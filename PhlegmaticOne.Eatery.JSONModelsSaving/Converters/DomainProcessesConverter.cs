using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;

internal class DomainProcessesConverter : Newtonsoft.Json.JsonConverter
{
    public override bool CanConvert(Type objectType) => objectType == typeof(DomainProductProcess) ||
                                                        objectType == typeof(IngredientProcess) ||
                                                        objectType == typeof(IntermediateProcess);

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jsonReader = JObject.Load(reader);
        var typeName = jsonReader["$type"].Value<string>();
        if (typeName.Contains("AddingProcess"))
        {
            return jsonReader.ToObject<AddingProcess>(serializer);
        }
        else if (typeName.Contains("CuttingProcess"))
        {
            return jsonReader.ToObject<CuttingProcess>(serializer);
        }
        else if (typeName.Contains("MixingProcess"))
        {
            return jsonReader.ToObject<MixingProcess>(serializer);
        }
        return null;
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {

    }
}
