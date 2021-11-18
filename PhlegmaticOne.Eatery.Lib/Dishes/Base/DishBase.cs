using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.Dishes;
/// <summary>
/// Represents base dish for other dishes
/// </summary>
public abstract class DishBase
{
    /// <summary>
    /// Initializes new DishBase instance
    /// </summary>
    /// <param name="price">Specified price</param>
    /// <param name="weight">Specified weight</param>
    /// <param name="name">Specified name</param>
    [Newtonsoft.Json.JsonConstructor]
    public DishBase(Money price, double weight, string name)
    {
        Price = price;
        Weight = weight;
        Name = name;
    }
    /// <summary>
    /// Price of dish
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public Money Price { get; internal set; }
    /// <summary>
    /// Weight of dish
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public double Weight { get; internal set; }
    /// <summary>
    /// Name of dish
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public string Name { get; internal set; }
    public override bool Equals(object? obj) => obj is DishBase other &&
                                                other.Name == Name && other.Price.Equals(Price) &&
                                                other.Weight == Weight;
    override public int GetHashCode() => Name.GetHashCode() ^ Weight.GetHashCode() ^ Price.GetHashCode();
    public override string ToString() => string.Format("{0}. Price: {1}. Weight: {2}", Name, Price, Weight);
}
