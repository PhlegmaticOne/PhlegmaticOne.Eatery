using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents contract for cutting processes
/// </summary>
public interface ICuttingProcess
{
    /// <summary>
    /// Cuts ingredient of type TIngredient into smaller ingredients with values equal to specified value
    /// </summary>
    IEnumerable<TIngredient> CutTo<TIngredient>(TIngredient ingredient, double value)
                                                where TIngredient : DomainProductToPrepare, new();
}
