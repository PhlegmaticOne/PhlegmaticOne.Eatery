using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public class AddingProcess : IngredientProcess
{
    public AddingProcess()
    {
    }
    [Newtonsoft.Json.JsonConstructor]
    public AddingProcess(TimeSpan timeToFinish, Money price) : base(timeToFinish, price)
    {
    }

    internal override void Update(DishBase dish, double weight)
    {
        dish.Price += Price * 1.1;
        dish.Weight += weight;
    }
}
