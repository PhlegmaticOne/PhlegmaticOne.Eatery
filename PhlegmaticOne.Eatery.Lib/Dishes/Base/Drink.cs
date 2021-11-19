using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.Dishes;
/// <summary>
/// Represents drinkable dish
/// </summary>
public class Drink : DishBase
{
    /// <summary>
    /// Initializes new Drink instance
    /// </summary>
    /// <param name="price">Specified price</param>
    /// <param name="weight">Specified weight</param>
    /// <param name="name">Specified name</param>
    [Newtonsoft.Json.JsonConstructor]
    public Drink(Money price, double weight, string name) : base(price, weight, name) { }

}
