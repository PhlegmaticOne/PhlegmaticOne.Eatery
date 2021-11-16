namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents prepared default storage builder
/// </summary>
/// <typeparam name="TStorage">Storage type</typeparam>
public class DefaultStorageBuilder<TStorage> : IStorageBuilder<TStorage> where TStorage : Storage, new()
{
    private IDictionary<Type, double>? _ingredientTypes;
    private IStorageTemperature? _storageTemperature;
    private int _amount;
    private StorageLightning _storageLightning;
    /// <summary>
    /// Sets amount of storages to create
    /// </summary>
    public void InAmountOf(int amount) => _amount = amount;
    /// <summary>
    /// Sets ingredients types that can be stored in building storage
    /// </summary>
    /// <typeparam name="TStorageIngredientConfiguration">Configuration type for adding ingredient types</typeparam>
    /// <param name="ingredientsConfiguration">Configuration action for configuring ingredient types</param>
    public void WithKeepingIngredientsTypes<TStorageIngredientConfiguration>
                                     (Action<TStorageIngredientConfiguration> ingredientsConfigurationAction)
                                     where TStorageIngredientConfiguration : IStorageIngredientsConfiguration, new()
    {
        var storageIngredientConfiguration = new TStorageIngredientConfiguration();
        ingredientsConfigurationAction.Invoke(storageIngredientConfiguration);
        _ingredientTypes = storageIngredientConfiguration.Configure();
    }
    /// <summary>
    /// Sets temperature for building process
    /// </summary> 
    /// <typeparam name="TStorageTemperatureConfiguration">Configuration type for adding temperature</typeparam>
    /// <param name="temperatureConfiguration">Configuration action for configuring temperature in storage</param>
    public void WithTemperarure<TStorageTemperatureConfiguration>
                                     (Action<TStorageTemperatureConfiguration> temperatureConfiguration)
                                     where TStorageTemperatureConfiguration : IStorageTemperatureConfiguration<IStorageTemperature>, new()
    {
        var storageTemperatureConfiguration = new TStorageTemperatureConfiguration();
        temperatureConfiguration.Invoke(storageTemperatureConfiguration);
        _storageTemperature = storageTemperatureConfiguration.Configure();
    }
    /// <summary>
    /// Sets lightning type in storage
    /// </summary>
    /// <param name="lightning"></param>
    public void WithLightning(StorageLightning lightning) => _storageLightning = lightning;
    /// <summary>
    /// Builds enumerable of configured storages
    /// </summary>
    public IEnumerable<TStorage> Build()
    {
        var result = new List<TStorage>();
        for (int i = 0; i < _amount; i++)
        {
            result.Add(new TStorage()
            {
                Lightning = _storageLightning,
                Temperature = _storageTemperature,
                KeepingIngredientsTypesInformation = _ingredientTypes
            });
        }
        return result;
    }
    /// <summary>
    /// Gets string representation of default storage builder
    /// </summary>
    /// <returns></returns>
    public override string ToString() => string.Format("Default storage builder for {0}", typeof(TStorage));
}