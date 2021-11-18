namespace PhlegmaticOne.Eatery.Lib.Ingredients;
/// <summary>
/// Represents olive oil sauce
/// </summary>
public class OliveOil : Ingredient, IEquatable<OliveOil>
{
    /// <summary>
    /// Initializes new cucumber instance
    /// </summary>
    public OliveOil() { }
    /// <summary>
    /// Initialzies new olive oil instance
    /// </summary>
    /// <param name="weight">Specified weight</param>
    /// <param name="value">Specified value</param>
    [Newtonsoft.Json.JsonConstructor]
    public OliveOil(double weight, double value) : base(weight, value) { }
    public bool Equals(OliveOil? other) => base.Equals(other);
    public override bool Equals(object? obj) => base.Equals(obj);
    public override int GetHashCode() => base.GetHashCode();
}
