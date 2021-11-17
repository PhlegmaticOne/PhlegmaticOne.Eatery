using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.Dishes;

public class Drink : DishBase
{
    public Drink(Money price, double weight, string name) : base(price, weight, name)
    {
    }
}
