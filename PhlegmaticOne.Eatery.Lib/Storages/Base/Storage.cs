using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents base storage for other storages
/// </summary>
public abstract class Storage : IEquatable<Storage>
{
    [Newtonsoft.Json.JsonProperty]
    private readonly Dictionary<Type, double> _ingredientsKeepingTypes;
    /// <summary>
    /// Keeping ingredients 
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    internal Dictionary<Type, double> KeepingIngredients = new Dictionary<Type, double>();
    /// <summary>
    /// Initializes new Storage instance
    /// </summary>
    protected Storage() => (KeepingIngredientsTypesInformation, Temperature) = (new Dictionary<Type, double>(), new StorageTemperature());
    /// <summary>
    /// Initializes new Storage instance
    /// </summary>
    /// <param name="lightning">Specified lightning</param>
    /// <param name="temperature">Specified tmperature</param>
    /// <exception cref="ArgumentNullException"></exception>
    protected Storage(StorageLightning lightning, StorageTemperature temperature) : this()
    {
        Lightning = lightning;
        Temperature = temperature ?? throw new ArgumentNullException(nameof(temperature));
    }
    /// <summary>
    /// Initializes new Storage instance
    /// </summary>
    /// <param name="lightning">Specified lightning</param>
    /// <param name="temperature">Specified tmperature</param>
    /// <param name="keepingIngredientsInfo">Information about keeping ingredients in storage</param>
    /// <param name="keepingIngredients">Specified keeping ingredients</param>
    [Newtonsoft.Json.JsonConstructor]
    internal Storage(StorageLightning lightning, StorageTemperature temperature,
                     Dictionary<Type, double> keepingIngredientsInfo, Dictionary<Type, double> keepingIngredients)
    {
        Lightning = lightning;
        Temperature = temperature;
        _ingredientsKeepingTypes = keepingIngredientsInfo;
        KeepingIngredients = keepingIngredients;
    }
    /// <summary>
    /// Lisghtning in storage
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public StorageLightning Lightning { get; internal set; }
    /// <summary>
    /// Temperature in storage
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public StorageTemperature Temperature { get; internal set; }
    /// <summary>
    /// Count of keeping ingredients in storage
    /// </summary>
    public int KeepingIngredientTypesCount => KeepingIngredients.Count;
    /// <summary>
    /// Keeping ingredients types information
    /// </summary>
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
    /// <summary>
    /// Gets all ingredients with their weight that are contained in storage in current moment
    /// </summary>
    public IReadOnlyDictionary<Type, double> GetIngredientsKeepingTypes() =>
        new ReadOnlyDictionary<Type, double>(KeepingIngredients);
    /// <summary>
    /// Gets all ingredients with their maximal weight that are can be contained in storage
    /// </summary>
    public IReadOnlyDictionary<Type, double> GetContainerInformation() =>
        new ReadOnlyDictionary<Type, double>(KeepingIngredientsTypesInformation);
    /// <summary>
    /// Checks if ingredient with specified ingredient type is contained in storage
    /// </summary>
    /// <param name="ingredientType"></param>
    /// <returns></returns>
    public bool ContainsIngredient(Type ingredientType) => KeepingIngredients.ContainsKey(ingredientType);
    /// <summary>
    /// Tries to add an ingredient type with specified weght in collection
    /// </summary>
    /// <returns>True - ingredient with ingredientType was added</returns>
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
    /// <summary>
    /// Tries to get an existing weight of ingredient in storage
    /// </summary>
    /// <returns>True - ingredient is contained in storage and weight was returned</returns>
    internal bool TryGetExistingWeightOfIngredient(Type ingredientType, out double existingWeight) =>
        KeepingIngredients.TryGetValue(ingredientType, out existingWeight);
    /// <summary>
    /// Gets an existing weight of ingredient in storage
    /// </summary>
    internal double GetExistingWeightOfIngredient(Type ingredientType) => KeepingIngredients[ingredientType];
    /// <summary>
    /// Tries to retrieve all ingredient of specified ingredient type
    /// </summary>
    /// <returns>True - ingredient type is contained in storage and it was totally retrieved</returns>
    internal bool TryRetrieveAllIngredient(Type ingredientType)
    {
        if (ContainsIngredient(ingredientType))
        {
            KeepingIngredients[ingredientType] = 0;
            return true;
        }
        return false;
    }
    /// <summary>
    /// Tries to retrieve an ingredientwith specified type in specified weight from storage
    /// </summary>
    /// <returns>True - ingredient type is contained in storage and it was retrieved in specified weight</returns>
    internal bool TryRetrieveIngredientInWeight(Type ingredientType, double retrievingWeight)
    {
        if (ContainsIngredient(ingredientType))
        {
            KeepingIngredients[ingredientType] -= retrievingWeight;
            return true;
        }
        return false;
    }
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
    public override bool Equals(object? obj) => Equals(obj as Storage);
    public bool Equals(Storage? other) => other is not null && other.Lightning == Lightning && other.Temperature == Temperature &&
                                          KeepingIngredientsTypesInformation.Except(other.KeepingIngredientsTypesInformation).Any() == false;
}
