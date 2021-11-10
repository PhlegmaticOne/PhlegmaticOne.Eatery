namespace PhlegmaticOne.Eatery.Lib.Ingredients;
/// <summary>
/// Represents tomato vagetable
/// </summary>
public class Tomato : Ingredient, IEquatable<Tomato>
{
    /// <summary>
    /// Initialzies new tomato instance
    /// </summary>
    public Tomato() { }
    /// <summary>
    /// Initialzies new tomato instance
    /// </summary>
    /// <param name="weight">Specified weight</param>
    /// <param name="value">Specified value</param>
    public Tomato(double weight, double value) : base(weight, value) { }
    /// <summary>
    /// Checks equality of tomato with other specified tomato
    /// </summary>
    public bool Equals(Tomato? other) => base.Equals(other);
    /// <summary>
    /// Check equality of tomato with other specified tomato
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as Tomato);
    /// <summary>
    /// Gets hash code of tomato
    /// </summary>
    public override int GetHashCode() => base.GetHashCode();
}
