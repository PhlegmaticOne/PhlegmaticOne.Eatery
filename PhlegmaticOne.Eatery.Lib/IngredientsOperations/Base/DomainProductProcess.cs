using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents base instance for processes over ingredients
/// </summary>
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
    /// <summary>
    /// Check equality of ingredient process with specified object
    /// </summary>
    public override bool Equals(object? obj) => obj is DomainProductProcess ingredientProcess &&
                                                TimeToFinish == ingredientProcess.TimeToFinish &&
                                                Price == ingredientProcess.Price;
    /// <summary>
    /// Gets hash code of ingredient process
    /// </summary>
    override public int GetHashCode() => TimeToFinish.Milliseconds ^ (int)Price.Amount ^ Price.CurrencyCode.GetHashCode();
    /// <summary>
    /// Gets string representation of ingredient process
    /// </summary>
    /// <returns></returns>
    public override string ToString() => string.Format("Process is {0}. Price: {1}. Time to finish: {2}",
                                                        GetType().Name, Price, TimeToFinish.ToString());
}
