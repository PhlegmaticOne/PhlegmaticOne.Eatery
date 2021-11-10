namespace PhlegmaticOne.Eatery.Lib.Helpers;

public abstract class Currency
{
    public abstract string CurrencyCode { get; }
    public abstract decimal GetExchangeRateFor(string currencyCode);
}
