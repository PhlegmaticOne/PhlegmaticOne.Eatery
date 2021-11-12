using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using System.Collections;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents base storage for other storages
/// </summary>
public abstract class Storage : IRetrievingCollection<Ingredient>
{
    private readonly RetrievingList<Ingredient> _ingredients;
    /// <summary>
    /// Initializes new storage instance
    /// </summary>
    protected Storage() => (IngredientsKeepingTypes, Temperature, _ingredients) = (new Dictionary<Type, double>(), new StorageTemperature(), new());
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
    public int Count => _ingredients.Count;
    /// <summary>
    /// Is collection read only
    /// </summary>
    public bool IsReadOnly => false;
    /// <summary>
    /// Ingredients keeping types with its maximal values to keep
    /// </summary>
    internal IDictionary<Type, double> IngredientsKeepingTypes { get; init; }
    /// <summary>
    /// Gets ingredient keeping types and thei maximal values to keep
    /// </summary>
    /// <returns></returns>
    public IReadOnlyDictionary<Type, double> GetIngredientsKeepingTypes() => new ReadOnlyDictionary<Type, double>(IngredientsKeepingTypes);
    public Ingredient RetrieveFirstOrDefault(Func<Ingredient, bool> predicate) => _ingredients.RetrieveFirstOrDefault(predicate);

    public IEnumerable<Ingredient> Retrieve(Func<Ingredient, bool> predicate) => _ingredients.Retrieve(predicate);

    public IEnumerable<Ingredient> RetrieveAll() => _ingredients.RetrieveAll();

    public void Add(Ingredient item) => _ingredients.Add(item);

    public void Clear() => _ingredients.Clear();

    public bool Contains(Ingredient item) => _ingredients.Contains(item);

    public void CopyTo(Ingredient[] array, int arrayIndex) => _ingredients.CopyTo(array, arrayIndex);

    public bool Remove(Ingredient item) => _ingredients.Remove(item);

    public IEnumerator<Ingredient> GetEnumerator() => _ingredients.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => _ingredients.GetEnumerator();
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
