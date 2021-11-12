using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents prepared default ingredient process builder
/// </summary>
/// <typeparam name="TProcess">Ingredient process type</typeparam>
public class DefaultProcessBuilder<TProcess> : IProcessBuilder<TProcess> where TProcess : DomainProductProcess, new()
{
    private Money?_money;
    private TimeSpan _timeToFinish;
    /// <summary>
    /// Sets price for ingredient process
    /// </summary>
    /// <returns>Instance of current process builder</returns>
    public IProcessBuilder<TProcess> WithCost(Money price)
    {
        _money = price;
        return this;
    }
    /// <summary>
    /// Sets duration of ingredient process
    /// </summary>
    /// <returns>Instance of current process builder</returns>
    public IProcessBuilder<TProcess> WithTimeToFinish(TimeSpan timeToFinish)
    {
        _timeToFinish = timeToFinish;
        return this;
    }
    /// <summary>
    /// Builds ingredient process with setted process values
    /// </summary>
    /// <returns></returns>
    public TProcess Build() => new()
    {
        Price = _money,
        TimeToFinish = _timeToFinish,
    };
    /// <summary>
    /// Returns string representation of default process builder
    /// </summary>
    /// <returns></returns>
    public override string ToString() => string.Format("Default process builder for {0}", typeof(TProcess).Name);
}
