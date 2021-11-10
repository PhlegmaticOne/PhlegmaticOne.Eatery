namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents base storage for other storages
/// </summary>
public abstract class Storage
{
    /// <summary>
    /// Initializes new storage instance
    /// </summary>
    protected Storage() => (IngredientsKeepingTypes, Temperature) = (new List<Type>(), new StorageTemperature());
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
    /// Ingredients keeping types
    /// </summary>
    internal IEnumerable<Type> IngredientsKeepingTypes { get; init; }
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
