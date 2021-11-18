using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.Dishes;
/// <summary>
/// Represents a simple dish
/// </summary>
public class Dish : DishBase, IEquatable<Dish>
{
    /// <summary>
    /// Initializes new Dish instance
    /// </summary>
    /// <param name="price">Specified price</param>
    /// <param name="weight">Specified weight</param>
    /// <param name="name">Specified name</param>
    [Newtonsoft.Json.JsonConstructor]
    public Dish(Money price, double weight, string name) : base(price, weight, name) { }
    public bool Equals(Dish? other) => base.Equals(other);

    public override bool Equals(object obj) => Equals(obj as Drink);

    public override int GetHashCode() => base.GetHashCode();
}
