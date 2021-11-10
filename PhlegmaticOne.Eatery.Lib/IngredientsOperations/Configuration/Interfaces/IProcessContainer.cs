using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents contract for ingredient process container
/// </summary>
public interface IProcessContainer
{
    /// <summary>
    /// Gets process for ingredient type
    /// </summary>
    /// <typeparam name="TIngredient">Type of ingredient</typeparam>
    IngredientProcess GetProcessOf<TIngredient>() where TIngredient : Ingredient;
}
