using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.Dishes;

public class Dish
{
    public Dish(Money price, double weight, string name)
    {
        Price = price;
        Weight = weight;
        Name = name;
    }

    public Money Price { get; set; }
    public double Weight { get; set; }
    public string Name { get; set; }
}
