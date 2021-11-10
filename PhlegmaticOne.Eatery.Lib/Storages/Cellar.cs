namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents cellar storage
/// </summary>
public class Cellar : Storage, IEquatable<Cellar>
{
    /// <summary>
    /// Initializes new cellar instance
    /// </summary>
    public Cellar() : base() { }
    /// <summary>
    /// Initializes new storage instance
    /// </summary>
    /// <param name="lightning">Specified lightning</param>
    /// <param name="storageTemperature">Specified temperature</param>
    public Cellar(StorageLightning lightning, IStorageTemperature storageTemperature) : base(lightning, storageTemperature) { }
    /// <summary>
    /// Check equality of cellar with other specified cellar
    /// </summary>
    public bool Equals(Cellar? other) => base.Equals(other);
    /// <summary>
    /// Check equality of cellar with other specified object
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Cellar);
    /// <summary>
    /// Gets hash code of cellar
    /// </summary>
    public override int GetHashCode() => base.GetHashCode();
}
