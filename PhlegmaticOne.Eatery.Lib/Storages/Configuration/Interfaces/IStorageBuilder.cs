namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents contract for storage builder
/// </summary>
/// <typeparam name="TStorage">Storage type</typeparam>
public interface IStorageBuilder<TStorage> where TStorage : Storage, new()
{
    /// <summary>
    /// Sets amount of storages to create
    /// </summary>
    void InAmountOf(int amount);
    /// <summary>
    /// Sets lightning type in storage
    /// </summary>
    /// <param name="lightning"></param>
    void WithLightning(StorageLightning lightning);
    /// <summary>
    /// Sets ingredients types that can be stored in building storage
    /// </summary>
    /// <typeparam name="TStorageIngredientConfiguration">Configuration type for adding ingredient types</typeparam>
    /// <param name="ingredientsConfiguration">Configuration action for configuring ingredient types</param>
    void WithKeepingIngredientsTypes<TStorageIngredientConfiguration>
                             (Action<TStorageIngredientConfiguration> ingredientsConfiguration)
                             where TStorageIngredientConfiguration : IStorageIngredientsConfiguration, new();
    /// <summary>
    /// Sets temperature for building process
    /// </summary> 
    /// <typeparam name="TStorageTemperatureConfiguration">Configuration type for adding temperature</typeparam>
    /// <param name="temperatureConfiguration">Configuration action for configuring temperature in storage</param>
    void WithTemperarure<TStorageTemperatureConfiguration>
        (Action<TStorageTemperatureConfiguration> temperatureConfiguration)
        where TStorageTemperatureConfiguration : IStorageTemperatureConfiguration<IStorageTemperature>, new();
    /// <summary>
    /// Builds enumerable of configured storages
    /// </summary>
    IEnumerable<TStorage> Build();
}
