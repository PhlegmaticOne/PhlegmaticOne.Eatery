using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.Dishes;

public abstract class DishBase
{
    [Newtonsoft.Json.JsonConstructor]
    public DishBase(Money price, double weight, string name)
    {
        Price = price;
        Weight = weight;
        Name = name;
    }
    [Newtonsoft.Json.JsonProperty]
    public Money Price { get; internal set; }
    [Newtonsoft.Json.JsonProperty]
    public double Weight { get; internal set; }
    [Newtonsoft.Json.JsonProperty]
    public string Name { get; internal set; }
}
