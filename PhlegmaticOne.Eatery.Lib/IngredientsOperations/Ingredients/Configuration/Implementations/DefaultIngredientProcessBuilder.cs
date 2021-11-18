using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents default ingredient process builder
/// </summary>
/// <typeparam name="TProcess"></typeparam>
public class DefaultIngredientProcessBuilder<TProcess> : IIngredientProcessBuilder<TProcess> where TProcess : IngredientProcess, new()
{
    private readonly List<TProcess> _processList = new();
    private Money? _money;
    private Type _ingredientType;
    /// <summary>
    /// Bilds collection of configured ingredient processes
    /// </summary>
    /// <returns></returns>
    public IList<TProcess> Build() => _processList;
    /// <summary>
    /// Sets ingredient type which can be processed by configuring process
    /// </summary>
    public IIngredientProcessBuilder<TProcess> CanProcess<TIngredient>() where TIngredient : Ingredient, new()
    {
        _ingredientType = typeof(TIngredient);
        return this;
    }
    /// <summary>
    /// Sets cost to a building ingredient process
    /// </summary>
    /// <param name="money">Price of process</param>
    /// <returns>Current instance of builder</returns>
    public IIngredientProcessBuilder<TProcess> WithCost(Money money)
    {
        _money = Money.ConvertToUSD(money);
        return this;
    }
    /// <summary>
    /// Sets duration of ingredient process
    /// </summary>
    /// <param name="timeToFinish">Specified duration</param>
    /// <returns>Current instance of process builder</returns>
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

