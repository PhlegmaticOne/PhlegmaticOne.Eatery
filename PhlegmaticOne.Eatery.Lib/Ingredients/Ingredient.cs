namespace PhlegmaticOne.Eatery.Lib.Ingredients;
/// <summary>
/// Represents base ingredient for all other ingredients
/// </summary>
public abstract class Ingredient
{
    /// <summary>
    /// Initialzies new ingredient instance
    /// </summary>
    protected Ingredient() { }
    /// <summary>
    /// Initialzies new ingredient instance
    /// </summary>
    /// <param name="weight">Specified weight</param>
    /// <param name="value">Specified value</param>
    [Newtonsoft.Json.JsonConstructor]
    protected Ingredient(double weight, double value) => (Weight, Value) = (weight, value);
    /// <summary>
    /// Weight of ingredient
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public double Weight { get; internal set; }
    /// <summary>
    /// Value of ingredient
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public double Value { get; internal set; }
    /// <summary>
    /// Gets string representation of domain product to prepare
    /// </summary>
    /// <returns></returns>
    public override string ToString() => string.Format("{0}. Weight: {1:f4}. Value: {2:f4}", GetType().Name, Weight, Value);
    /// <summary>
    /// Check equality of ingredient with specified object
    /// </summary>
    public override bool Equals(object? obj) => obj is Ingredient ingredient && Weight == ingredient.Weight && ingredient.Value == Value;
    /// <summary>
    /// Gets hash code of ingredient
    /// </summary>
    public override int GetHashCode() => Weight.GetHashCode() ^ Value.GetHashCode();
}
