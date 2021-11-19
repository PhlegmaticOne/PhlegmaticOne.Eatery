using PhlegmaticOne.Eatery.Lib.Dishes;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public class ShakeUpProcess : IntermediateProcess
{
    internal override void Update(DishBase dish)
    {
        dish.Price += Price * 1.03;
        dish.Weight *= 0.99;
    }
}
