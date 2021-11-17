using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;

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

    internal override void Update(Dish dish, Ingredient ingredient)
    {
        dish.Price += Price * 1.1;
        dish.Weight += ingredient.Weight;
    }
}
