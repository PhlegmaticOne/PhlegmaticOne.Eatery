namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents prepared default storage temperature
/// </summary>
public class StorageTemperature : IStorageTemperature, IEquatable<StorageTemperature>
{
    /// <summary>
    /// Initializes new storage temperature instance
    /// </summary>
    public StorageTemperature() { }
    /// <summary>
    /// Initializes new storage temperature instance
    /// </summary>
    /// <param name="minimalTemperature">Specified minimal temperature</param>
    /// <param name="maximalTemperature">Specified maximal temperature</param>
    /// <param name="averageTemperatureAnytime">Specified average temperature</param>
    public StorageTemperature(int minimalTemperature, int maximalTemperature,
                              int averageTemperatureAnytime = int.MinValue) =>
                              (MinimalTemperature, MaximalTemperature, AverageTemperatureAnytime) =
                              (minimalTemperature, maximalTemperature,
                               averageTemperatureAnytime == int.MinValue ?
                                                            minimalTemperature + maximalTemperature / 2 :
                                                            averageTemperatureAnytime);
    /// <summary>
    /// Maximal temperature
    /// </summary>
    public int MaximalTemperature { get; set; }
    /// <summary>
    /// Minimal temperature
    /// </summary>
    public int MinimalTemperature { get; set; }
    /// <summary>
    /// Average temperature anytime
    /// </summary>
    public int AverageTemperatureAnytime { get; set; }
    /// <summary>
    /// Check equality of storage temperature with other specified object
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as StorageTemperature);
    /// <summary>
    /// Checks equality of storage temperature with other specified storage temperature
    /// </summary>
    public bool Equals(StorageTemperature? other) => (other is not null) &&
                                                    MaximalTemperature == other.MinimalTemperature &&
                                                    MinimalTemperature == other.MinimalTemperature &&
                                                    AverageTemperatureAnytime == other.AverageTemperatureAnytime;
    /// <summary>
    /// Gets hash code of storage temperature
    /// </summary>
    public override int GetHashCode() => 314159 * (MinimalTemperature ^ MaximalTemperature ^ AverageTemperatureAnytime);
    /// <summary>
    /// Gets string representation of storage temperature
    /// </summary>
    /// <returns></returns>
    public override string ToString() => string.Format("Range is: [{0}, {1}]. Average: {2}",
                                            MinimalTemperature, MaximalTemperature, AverageTemperatureAnytime);
}
