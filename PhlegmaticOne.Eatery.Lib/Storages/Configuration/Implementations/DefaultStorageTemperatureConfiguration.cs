namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents prepared default storage temperature configuration
/// </summary>
/// <typeparam name="TStorageTemperature">Storage temperature contract</typeparam>
public class DefaultStorageTemperatureConfiguration<TStorageTemperature> : IStorageTemperatureConfiguration<IStorageTemperature>
                                                                           where TStorageTemperature : IStorageTemperature, new()
{
    private int _minimalTemperature;
    private int _maximalTemperature;
    private int _averageTemperature;
    /// <summary>
    /// Sets average temperature of configuring temperature
    /// </summary>
    public void WithAverageTemperature(int averageTemperature) => _averageTemperature = averageTemperature;
    /// <summary>
    /// Sets maximal temperature of configuring temperature
    /// </summary>
    public void WithMaximalTemperature(int minimalTemperature) => _maximalTemperature = minimalTemperature;
    /// <summary>
    /// Sets minimal temperature of configuring temperature
    /// </summary>
    public void WithMinimalTemperature(int minimalTemperature) => _minimalTemperature = minimalTemperature;
    /// <summary>
    /// Sets average temperature of configuring temperature
    /// </summary>
    public IStorageTemperature Configure() => new TStorageTemperature()
    {
        MinimalTemperature = _minimalTemperature,
        MaximalTemperature = _maximalTemperature,
        AverageTemperatureAnytime = _averageTemperature
    };
    /// <summary>
    /// Gets string representation of default storage temperature configuration
    /// </summary>
    public override string ToString() => string.Format("Default storage temperature configuration");
}
