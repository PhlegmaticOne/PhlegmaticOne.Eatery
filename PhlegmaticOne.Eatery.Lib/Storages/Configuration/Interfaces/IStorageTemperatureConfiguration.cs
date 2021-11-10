namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents contract for storage temperature configuration
/// </summary>
/// <typeparam name="IStorageTemperature">Storage temperature contract</typeparam>
public interface IStorageTemperatureConfiguration<IStorageTemperature>
{
    /// <summary>
    /// Sets minimal temperature of configuring temperature
    /// </summary>
    void WithMinimalTemperature(int minimalTemperature);
    /// <summary>
    /// Sets maximal temperature of configuring temperature
    /// </summary>
    void WithMaximalTemperature(int maxmalTemperature);
    /// <summary>
    /// Sets average temperature of configuring temperature
    /// </summary>
    void WithAverageTemperature(int averageTemperature);
    /// <summary>
    /// Sets average temperature of configuring temperature
    /// </summary>
    IStorageTemperature Configure();
}
