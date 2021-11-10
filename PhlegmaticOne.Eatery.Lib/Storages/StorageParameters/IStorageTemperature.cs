namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Contract for storage temperatures
/// </summary>
public interface IStorageTemperature
{
    /// <summary>
    /// Minimal temperature in storage
    /// </summary>
    int MaximalTemperature { get; set; }
    /// <summary>
    /// Maximal temperature in storage
    /// </summary>
    int MinimalTemperature { get; set; }
    /// <summary>
    /// Average temperature in storage
    /// </summary>
    int AverageTemperatureAnytime { get; set; }
}
