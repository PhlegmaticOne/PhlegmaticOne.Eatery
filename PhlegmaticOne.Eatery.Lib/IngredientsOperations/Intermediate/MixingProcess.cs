using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public class MixingProcess : IntermediateProcess
{
    public MixingProcess()
    {
    }

    public MixingProcess(TimeSpan timeToFinish, Money price) : base(timeToFinish, price)
    {
    }

    internal override void Update(Dish dish)
    {
        dish.Weight *= 0.87;
        dish.Price += Price * 1.1m;
    }
}
