using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents cutting process for ingredients
/// </summary>
public class CuttingProcess : IngredientProcess, IEquatable<CuttingProcess>
{
    /// <summary>
    /// Initialzes new cutting process
    /// </summary>
    public CuttingProcess() { }
    /// <summary>
    /// Initialzes new cutting process
    /// </summary>
    /// <param name="timeToFinish">Specified diration of process</param>
    /// <param name="price">Cost of process</param>
    [Newtonsoft.Json.JsonConstructor]
    public CuttingProcess(TimeSpan timeToFinish, Money price) : base(timeToFinish, price) { }
    /// <summary>
    /// Checks equality of cutting process with other specified cutting process
    /// </summary>
    public bool Equals(CuttingProcess? other) => base.Equals(other);
    /// <summary>
    /// Check equality of cutting process with other specified object
    /// </summary>
    public override bool Equals(object? obj) => Equals(obj as CuttingProcess);
    /// <summary>
    /// Gets hash code of cutting process
    /// </summary>
    public override int GetHashCode() => base.GetHashCode();

    internal override void Update(DishBase dish, double weight)
    {
        dish.Price += Price;
        dish.Weight -= weight * 0.02;
    }
}
