namespace PhlegmaticOne.Eatery.Lib;

public abstract class DomainProductToPrepare
{
    /// <summary>
    /// Initialzies new ingredient instance
    /// </summary>
    protected DomainProductToPrepare() { }
    /// <summary>
    /// Initialzies new ingredient instance
    /// </summary>
    /// <param name="weight">Specified weight</param>
    /// <param name="value">Specified value</param>
    protected DomainProductToPrepare(double weight, double value) => (Weight, Value) = (weight, value);
    /// <summary>
    /// Weight of ingredient
    /// </summary>
    public double Weight { get; internal set; }
    /// <summary>
    /// Value of ingredient
    /// </summary>
    public double Value { get; internal set; }
    /// <summary>
    /// Gets string representation of domain product to prepare
    /// </summary>
    /// <returns></returns>
    public override string ToString() => GetType().Name;
}
