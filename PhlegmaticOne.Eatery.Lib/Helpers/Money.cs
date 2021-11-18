namespace PhlegmaticOne.Eatery.Lib.Helpers;
/// <summary>
/// Represents money for setting price to domain instances
/// </summary>
public class Money : IEquatable<Money>
{
    /// <summary>
    /// Initializes new Money instance
    /// </summary>
    public Money() { }
    /// <summary>
    /// Intializes new money instance
    /// </summary>
    /// <param name="amount">Amount of money</param>
    /// <param name="currencyCode">Currency code</param>
    /// <exception cref="ArgumentException">Amount less than 0</exception>
    /// <exception cref="ArgumentNullException">Currency code is null or white space</exception>
    [Newtonsoft.Json.JsonConstructor]
    public Money(double amount, string currencyCode)
    {
        if (string.IsNullOrWhiteSpace(currencyCode))
        {
            throw new ArgumentNullException(nameof(currencyCode), $"\"{nameof(currencyCode)}\" не может быть пустым или содержать только пробел.");
        }
        Amount = amount >= 0 ? amount :
                 throw new ArgumentException("Money amount cannot be less than zero", nameof(amount));
        CurrencyCode = currencyCode;
    }
    /// <summary>
    /// Amount of money
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public double Amount { get; }
    /// <summary>
    /// Currency code of money
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public string CurrencyCode { get; }
    /// <summary>
    /// Overloading sum for money
    /// </summary>
    /// <exception cref="ArgumentException">Currency codes of money are diferrent</exception>
    public static Money operator +(Money a, Money b)
    {
        if (a.CurrencyCode != b.CurrencyCode)
        {
            throw new ArgumentException("Cannot add money with different currency codes");
        }
        return new Money(a.Amount + b.Amount, a.CurrencyCode);
    }
    /// <summary>
    /// Overloading difference for money
    /// </summary>
    /// <exception cref="ArgumentException">Currency codes of money are diferrent</exception>
    public static Money operator -(Money a, Money b)
    {
        if (a.CurrencyCode != b.CurrencyCode)
        {
            throw new ArgumentException("Cannot add money with different currency codes");
        }
        var difference = a.Amount - b.Amount;
        if (difference < 0)
        {
            throw new ArgumentException("Difference of money cannot be less to zero");
        }
        return new Money(difference, a.CurrencyCode);
    }
    /// <summary>
    /// Overloading multiplication for money and number
    /// </summary>
    public static Money operator *(Money a, double n) => new(a.Amount * n, a.CurrencyCode);
    public static Money ConvertToUSD(Money money) => money.CurrencyCode switch
    {
        "RUB" => new Money(money.Amount * 0.013, "USD"),
        "BLR" => new Money(money.Amount * 0.37, "USD"),
        _ => new Money(money.Amount, "USD"),
    };
    public override string ToString() => string.Format("{0:f4} {1}", Amount, CurrencyCode);
    public override bool Equals(object? obj) => Equals(obj as Money);
    public bool Equals(Money? other) => other is not null && Amount == other.Amount && CurrencyCode == other.CurrencyCode;
    public override int GetHashCode() => CurrencyCode.GetHashCode() ^ (int)Amount;
}
