namespace PhlegmaticOne.Eatery.Lib.Ingredients;
/// <summary>
/// Represents cucumber vegetable
/// </summary>
public class Cucumber : Ingredient, IEquatable<Cucumber>
{
    /// <summary>
    /// Initializes new cucumber instance
    /// </summary>
    public Cucumber() : base() { }
    /// <summary>
    /// Initialzies new cucumber instance
    /// </summary>
    /// <param name="weight">Specified weight</param>
    /// <param name="value">Specified value</param>
    [Newtonsoft.Json.JsonConstructor]
    public Cucumber(double weight, double value) : base(weight, value) { }
    /// <summary>
    /// Checks equality of cucumber with other specified cucumber
    /// </summary>
    public bool Equals(Cucumber? other) => base.Equals(other);
    /// <summary>
    /// Check equality of cucumber with other specified cucumber
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Cucumber);
    /// <summary>
    /// Gets hash code of cucumber
    /// </summary>
    public override int GetHashCode() => base.GetHashCode();
}