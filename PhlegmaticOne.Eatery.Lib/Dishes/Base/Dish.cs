using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.Dishes;

public class Dish
{
    [Newtonsoft.Json.JsonConstructor]
    public Dish(Money price, double weight, string name)
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
