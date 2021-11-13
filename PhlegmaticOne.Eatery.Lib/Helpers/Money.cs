namespace PhlegmaticOne.Eatery.Lib.Helpers;
/// <summary>
/// Represents money for setting price to domain instances
/// </summary>
public class Money : IEquatable<Money>
{
    /// <summary>
    /// Intializes new money instance
    /// </summary>
    /// <param name="amount">Amount of money</param>
    /// <param name="currencyCode">Currency code</param>
    /// <exception cref="ArgumentException">Amount less than 0</exception>
    /// <exception cref="ArgumentNullException">Currency code is null or white space</exception>
    public Money(decimal amount, string currencyCode)
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
    public decimal Amount { get; }
    /// <summary>
    /// Currency code
    /// </summary>
    public string CurrencyCode { get; }
    public static Money operator +(Money a, Money b)
    {
        if (a.CurrencyCode != b.CurrencyCode)
        {
            throw new ArgumentException("Cannot add money with different currency codes");
        }
        return new Money(a.Amount + b.Amount, a.CurrencyCode);
    }
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
    public static Money operator *(Money a, decimal n) => new(a.Amount * n, a.CurrencyCode);
    public static Money ConvertToUSD(Money money) => money.CurrencyCode switch
    {
        "RUB" => new Money(money.Amount * 0.013m, "USD"),
        "BLR" => new Money(money.Amount * 0.37m, "USD"),
        _ => new Money(money.Amount, "USD"),
    };
    public override string ToString() => string.Format("{0:f4} {1}", Amount, CurrencyCode);
    public override bool Equals(object? obj) => Equals(obj as Money);

    public bool Equals(Money? other) => other is not null && Amount == other.Amount && CurrencyCode == other.CurrencyCode;
    public override int GetHashCode() => CurrencyCode.GetHashCode() ^ (int)Amount;
}
