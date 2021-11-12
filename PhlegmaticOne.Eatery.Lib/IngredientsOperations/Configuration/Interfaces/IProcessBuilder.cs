using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents contract for ingredient process builder
/// </summary>
/// <typeparam name="TProcess">Ingredient process type</typeparam>
public interface IProcessBuilder<TProcess> where TProcess : DomainProductProcess
{
    /// <summary>
    /// Sets cost to a building ingredient process
    /// </summary>
    /// <param name="money">Price of process</param>
    /// <returns>Current instance of builder</returns>
    IProcessBuilder<TProcess> WithCost(Money money);
    /// <summary>
    /// Sets duration of ingredient process
    /// </summary>
    /// <param name="timeToFinish">Specified duration</param>
    /// <returns>Current instance of process builder</returns>
    IProcessBuilder<TProcess> WithTimeToFinish(TimeSpan timeToFinish);
    /// <summary>
    /// Builds ingredient process from setted parameters of it
    /// </summary>
    /// <returns></returns>
    TProcess Build();
}
