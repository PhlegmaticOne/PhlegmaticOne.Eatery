using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents base storage for other storages
/// </summary>
public abstract class Storage
{
    [Newtonsoft.Json.JsonProperty]
    public Dictionary<Type, double> _ingredientsKeepingTypes;
    protected Storage() => (KeepingIngredientsTypesInformation, Temperature) = (new Dictionary<Type, double>(), new StorageTemperature());
    protected Storage(StorageLightning lightning, StorageTemperature temperature) : this()
    {
        Lightning = lightning;
        Temperature = temperature ?? throw new ArgumentNullException(nameof(temperature));
    }
    internal Storage(StorageLightning lightning, StorageTemperature temperature,
                     Dictionary<Type, double> keepingIngredientsInfo, Dictionary<Type, double> keepingIngredients)
    {
        Lightning = lightning;
        Temperature = temperature;
        _ingredientsKeepingTypes = keepingIngredientsInfo;
        KeepingIngredients = keepingIngredients;
    }
    [Newtonsoft.Json.JsonProperty]
    public StorageLightning Lightning { get; internal set; }
    [Newtonsoft.Json.JsonProperty]
    public StorageTemperature Temperature { get; internal set; }
    public int KeepingIngredientTypesCount => KeepingIngredients.Count;
    [Newtonsoft.Json.JsonProperty]
    public Dictionary<Type, double> KeepingIngredients = new Dictionary<Type, double>();
    internal Dictionary<Type, double> KeepingIngredientsTypesInformation
    {
        get => _ingredientsKeepingTypes;
        init
        {
            foreach (var type in value.Keys)
            {
                KeepingIngredients.Add(type, 0);
            }
            _ingredientsKeepingTypes = value;
        }
    }
    internal bool TryAdd(Type ingredientType, double weight)
    {
        if (KeepingIngredients.TryGetValue(ingredientType, out double existingWeight) == false)
        {
            return false;
        }
        var maximalWeight = KeepingIngredientsTypesInformation[ingredientType];
        var newWeight = existingWeight + weight;
        if (newWeight > maximalWeight) return false;
        KeepingIngredients[ingredientType] = newWeight;
        return true;
    }
    internal bool TryGetExistingWeightOfIngredient(Type ingredientType, out double existingWeight) =>
        KeepingIngredients.TryGetValue(ingredientType, out existingWeight);
    internal double GetExistingWeightOfIngredient(Type ingredientType) => KeepingIngredients[ingredientType];
    internal bool TryRetrieveAllIngredient(Type ingredientType)
    {
        if (ContainsIngredient(ingredientType))
        {
            KeepingIngredients[ingredientType] = 0;
            return true;
        }
        return false;
    }
    internal bool TryRetrieveIngredientInWeight(Type ingredientType, double retrievingWeight)
    {
        if (ContainsIngredient(ingredientType))
        {
            KeepingIngredients[ingredientType] -= retrievingWeight;
            return true;
        }
        return false;
    }
    public IReadOnlyDictionary<Type, double> GetIngredientsKeepingTypes() =>
        new ReadOnlyDictionary<Type, double>(KeepingIngredients);
    public IReadOnlyDictionary<Type, double> GetContainerInformation() =>
        new ReadOnlyDictionary<Type, double>(KeepingIngredientsTypesInformation);
    public bool ContainsIngredient(Type ingredientType) => KeepingIngredients.ContainsKey(ingredientType);
    public override string ToString() => string.Format("{0}. Lightning: {1}. Temperature: {2}", GetType(), Lightning, Temperature);
    public override int GetHashCode()
    {
        int result = int.MaxValue;
        foreach (var type in KeepingIngredientsTypesInformation)
        {
            result ^= type.GetHashCode();
        }
        return result ^= (Lightning.GetHashCode() + Temperature.MaximalTemperature +
                          Temperature.MinimalTemperature + Temperature.AverageTemperatureAnytime);
    }
    public override bool Equals(object? obj) =>
                obj is Storage storage && storage.Lightning == Lightning && storage.Temperature == Temperature &&
                KeepingIngredientsTypesInformation.Except(storage.KeepingIngredientsTypesInformation).Any() == false;
}
