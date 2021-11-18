using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents contract for ingredient process builder
/// </summary>
/// <typeparam name="TProcess">Ingredient process type</typeparam>
public interface IIngredientProcessBuilder<TProcess> where TProcess : IngredientProcess, new()
{
    /// <summary>
    /// Sets ingredient type which can be processed by configuring process
    /// </summary>
    IIngredientProcessBuilder<TProcess> CanProcess<TIngredient>() where TIngredient : Ingredient, new();
    /// <summary>
    /// Sets cost to a building ingredient process
    /// </summary>
    /// <param name="money">Price of process</param>
    /// <returns>Current instance of builder</returns>
    IIngredientProcessBuilder<TProcess> WithCost(Money money);
    /// <summary>
    /// Sets duration of ingredient process
    /// </summary>
    /// <param name="timeToFinish">Specified duration</param>
    /// <returns>Current instance of process builder</returns>
    IIngredientProcessBuilder<TProcess> WithTimeToFinish(TimeSpan timeToFinish);
    /// <summary>
    /// Bilds collection of configured ingredient processes
    /// </summary>
    /// <returns></returns>
    IList<TProcess> Build();
}
