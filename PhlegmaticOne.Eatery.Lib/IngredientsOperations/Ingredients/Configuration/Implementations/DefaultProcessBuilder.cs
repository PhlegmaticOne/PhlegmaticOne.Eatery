using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public class DefaultProcessBuilder<TProcess> : IIngredientProcessBuilder<TProcess> where TProcess : IngredientProcess, new()
{
    private readonly List<TProcess> _processList = new();
    private Money? _money;
    private Type _ingredientType;
    public IList<TProcess> Build() => _processList;

    public IIngredientProcessBuilder<TProcess> CanProcess<TIngredient>() where TIngredient : Ingredient, new()
    {
        _ingredientType = typeof(TIngredient);
        return this;
    }
    public IIngredientProcessBuilder<TProcess> WithCost(Money money)
    {
        _money = Money.ConvertToUSD(money);
        return this;
    }
    public IIngredientProcessBuilder<TProcess> WithTimeToFinish(TimeSpan timeToFinish)
    {
        _processList.Add(new TProcess
        {
            CurrentIngredientType = _ingredientType,
            Price = _money,
            TimeToFinish = timeToFinish
        });
        return this;
    }
}

