using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents cutting process for ingredients
/// </summary>
public class CuttingProcess : IngredientProcess, ICuttingProcess, IEquatable<CuttingProcess>
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
    public CuttingProcess(TimeSpan timeToFinish, Money price) :
                         base(timeToFinish, price)
    { }
    /// <summary>
    /// Cuts an ingredient into ingredients with smaller values
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    /// <param name="ingredient">Ingredient entity</param>
    /// <param name="value">New ingredient value to cut</param>
    /// <returns>Enumerable of ingredients with values equal to specified value and remaining part of ingredient</returns>
    /// <exception cref="ArgumentException">Value is greater than ingredient to cut value</exception>
    /// <exception cref="ArgumentNullException">Ingredient instance to cut is null</exception>
    public IEnumerable<TIngredient> CutTo<TIngredient>(TIngredient ingredient, double value)
                                                       where TIngredient : Ingredient, new()
    {
        if (ingredient is null)
        {
            throw new ArgumentNullException(nameof(ingredient));
        }
        if (value <= 0)
        {
            throw new ArgumentException("Cannot cut ingredient to zero value or to negative value", nameof(value));
        }
        if (value > ingredient.Value)
        {
            throw new ArgumentException("Cannot get ingredients with value more than current ingredient value",
                                         nameof(value));
        }
        var timesToCut = (int)Math.Floor(ingredient.Value / value);
        var remainingValue = ingredient.Value % value;
        var valuePerFittedIngredient = (ingredient.Value - remainingValue) / timesToCut;
        var weightPerFittedIngredient = (ingredient.Weight * value) / ingredient.Value;
        var remaingWeight = ingredient.Weight % weightPerFittedIngredient;
        var cuttedToFittedSizesIngredient = new TIngredient()
        {
            Weight = weightPerFittedIngredient,
            Value = valuePerFittedIngredient
        };
        var remainingIngredientPart = new TIngredient()
        {
            Weight = remaingWeight,
            Value = remainingValue
        };
        return Enumerable.Repeat(cuttedToFittedSizesIngredient, timesToCut)
                         .Append(remainingIngredientPart)
                         .ToList();
    }
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
}
