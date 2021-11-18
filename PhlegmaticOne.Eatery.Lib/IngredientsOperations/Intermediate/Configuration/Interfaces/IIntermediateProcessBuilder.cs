using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents contract for intermediate processes builders
/// </summary>
/// <typeparam name="TProcess"></typeparam>
public interface IIntermediateProcessBuilder<TProcess> where TProcess : IntermediateProcess, new()
{
    /// <summary>
    /// Sets default intemediate process
    /// </summary>
    /// <param name="money"></param>
    /// <param name="timeSpan"></param>
    void SetDefaultProcess(Money money, TimeSpan timeSpan);
    /// <summary>
    /// Set preferable types of ingredients for configuring process
    /// </summary>
    IIntermediateProcessBuilder<TProcess> DishMayContain<TIngredient>();
    /// <summary>
    /// Sets cost to a building ingredient process
    /// </summary>
    /// <param name="money">Price of process</param>
    /// <returns>Current instance of builder</returns>
    IIntermediateProcessBuilder<TProcess> WithCost(Money money);
    /// <summary>
    /// Sets duration of ingredient process
    /// </summary>
    /// <param name="timeToFinish">Specified duration</param>
    /// <returns>Current instance of process builder</returns>
    void WithTimeToFinish(TimeSpan timeToFinish);
    /// <summary>
    /// Builds list of configurd intermediate processes
    /// </summary>
    IList<TProcess> Build();
}
