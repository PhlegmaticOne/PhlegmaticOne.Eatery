using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.Dishes;
public class Dish : DishBase
{
    public Dish(Money price, double weight, string name) : base(price, weight, name)
    {
    }
}
