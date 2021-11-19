using PhlegmaticOne.Eatery.Lib.Dishes;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public class CoolingProcess : IntermediateProcess
{
    internal override void Update(DishBase dish)
    {
        dish.Price += Price * 1.2;
    }
}
