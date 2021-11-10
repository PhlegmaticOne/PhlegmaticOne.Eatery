namespace PhlegmaticOne.Eatery.Lib.Helpers;
/// <summary>
/// Represents money for setting price to domain instances
/// </summary>
public class Money
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

}
