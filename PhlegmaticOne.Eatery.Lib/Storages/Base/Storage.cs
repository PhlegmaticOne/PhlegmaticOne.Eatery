using PhlegmaticOne.Eatery.Lib.Ingredients;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents base storage for other storages
/// </summary>
public abstract class Storage
{
    private readonly Dictionary<Type, double> _keepingIngredients = new();
    /// <summary>
    /// Initializes new storage instance
    /// </summary>
    protected Storage() => (IngredientsKeepingTypes, Temperature) = (new Dictionary<Type, double>(), new StorageTemperature());
    /// <summary>
    /// Initializes new storage instance
    /// </summary>
    /// <param name="lightning">Specified lightning</param>
    /// <param name="temperature">Specified temperature</param>
    /// <exception cref="ArgumentNullException">Temperature is null</exception>
    protected Storage(StorageLightning lightning, IStorageTemperature temperature) : this()
    {
        Lightning = lightning;
        Temperature = temperature ?? throw new ArgumentNullException(nameof(temperature));
    }
    /// <summary>
    /// Storage lightning
    /// </summary>
    public StorageLightning Lightning { get; internal set; }
    /// <summary>
    /// Storage temperature
    /// </summary>
    public IStorageTemperature Temperature { get; internal set; }
    /// <summary>
    /// All ingredients in storage
    /// </summary>
    public int Count => IngredientsKeepingTypes.Count;
    private IDictionary<Type, double> _ingredientsKeepingTypes;
    /// <summary>
    /// Ingredients keeping types with its maximal values to keep
    /// </summary>
    internal IDictionary<Type, double> IngredientsKeepingTypes
    {
        get => _ingredientsKeepingTypes;
        init
        {
            foreach (var type in value.Keys)
            {
                _keepingIngredients.Add(type, 0);
            }
            _ingredientsKeepingTypes = value;
        }
    }
    public bool TryAdd(Type ingredientType, double weight)
    {
        if (weight <= 0) return false;
        if(_keepingIngredients.TryGetValue(ingredientType, out double existingWeight) == false) return false;
        if (IngredientsKeepingTypes.TryGetValue(ingredientType, out double maximalWeight) == false) return false;
        var newWeight = existingWeight + weight;
        if (newWeight > maximalWeight) return false;

        _keepingIngredients.Remove(ingredientType);
        _keepingIngredients.Add(ingredientType, newWeight);

        return true;
    }
    public Ingredient TryRetrieve(Type ingredientType, double weight)
    {
        if (weight <= 0) return default;
        if (_keepingIngredients.TryGetValue(ingredientType, out double existingWeight))
        {
            if(weight > existingWeight) return null;
            _keepingIngredients.Remove(ingredientType);
            _keepingIngredients.Add(ingredientType, existingWeight - weight);
            return Activator.CreateInstance(ingredientType, weight, weight) as Ingredient;
        }
        return default;
    }
    /// <summary>
    /// Gets ingredient keeping types and thei maximal values to keep
    /// </summary>
    /// <returns></returns>
    public IReadOnlyDictionary<Type, double> GetIngredientsKeepingTypes() => new ReadOnlyDictionary<Type, double>(IngredientsKeepingTypes);
    /// <summary>
    /// Gets string representation of storage
    /// </summary>
    public override string ToString() => string.Format("{0}. Lightning: {1}. Temperature: {2}", GetType(), Lightning, Temperature);
    /// <summary>
    /// Gets hash code of storage
    /// </summary>
    public override int GetHashCode()
    {
        int result = int.MaxValue;
        foreach (var type in IngredientsKeepingTypes)
        {
            result ^= type.GetHashCode();
        }
        return result ^= (Lightning.GetHashCode() + Temperature.MaximalTemperature +
                          Temperature.MinimalTemperature + Temperature.AverageTemperatureAnytime);
    }
    /// <summary>
    /// Checks equality of storage with other specified object
    /// </summary>
    public override bool Equals(object? obj) =>
                obj is Storage storage && storage.Lightning == Lightning && storage.Temperature == Temperature &&
                IngredientsKeepingTypes.Except(storage.IngredientsKeepingTypes).Any() == false;
}
