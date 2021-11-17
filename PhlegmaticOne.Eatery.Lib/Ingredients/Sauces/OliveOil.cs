namespace PhlegmaticOne.Eatery.Lib.Ingredients;

public class OliveOil : Ingredient, IEquatable<OliveOil>
{
    public OliveOil()
    {
    }
    [Newtonsoft.Json.JsonConstructor]
    public OliveOil(double weight, double value) : base(weight, value)
    {
    }

    public bool Equals(OliveOil? other) => base.Equals(other);

    public override bool Equals(object? obj) => base.Equals(obj);

    public override int GetHashCode() => base.GetHashCode();
}
