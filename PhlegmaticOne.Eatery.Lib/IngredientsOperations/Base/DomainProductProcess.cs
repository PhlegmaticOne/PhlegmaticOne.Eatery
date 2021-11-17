using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public abstract class DomainProductProcess
{
    /// <summary>
    /// Initialzies new ingredient process
    /// </summary>
    protected DomainProductProcess() => Price = new Money(0, "USD");
    /// <summary>
    /// Initializes new ingredient process
    /// </summary>
    /// <param name="timeToFinish">Specified duration of process</param>
    /// <param name="price">Price of process</param>
    /// <exception cref="ArgumentNullException">Money is null</exception>
    /// <exception cref="ArgumentException">Time is 0</exception>
    [Newtonsoft.Json.JsonConstructor]
    protected DomainProductProcess(TimeSpan timeToFinish, Money price)
    {
        TimeToFinish = timeToFinish != TimeSpan.Zero ? timeToFinish :
            throw new ArgumentException("Process cannot last 0 time", nameof(timeToFinish));
        Price = price ?? throw new ArgumentNullException(nameof(price));
    }
    /// <summary>
    /// Diration of process
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public TimeSpan TimeToFinish { get; internal set; }
    /// <summary>
    /// Cost of process
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public Money Price { get; internal set; }
}
