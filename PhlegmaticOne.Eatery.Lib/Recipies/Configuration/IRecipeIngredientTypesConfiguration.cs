using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.Recipies;
/// <summary>
/// Represents contract for configurating ingredients using in recipe
/// </summary>
public interface IRecipeIngredientTypesConfiguration
{
    /// <summary>
    /// Sets needed ingredient
    /// </summary>
    /// <typeparam name="TIngredient">Concrete ingredient type</typeparam>
    /// <returns></returns>
    IRecipeIngredientTypesConfiguration NeedIngredient<TIngredient>() where TIngredient : Ingredient, new();
    /// <summary>
    /// Sets necessary weight of ingredient
    /// </summary>
    /// <param name="weight"></param>
    void InWeightOf(double weight);
    /// <summary>
    /// Creates dictionary with configured ingredient types and their necessary weights
    /// </summary>
    /// <returns></returns>
    IDictionary<Type, double> Configure();
}
