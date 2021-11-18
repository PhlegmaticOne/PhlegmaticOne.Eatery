using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.Recipies;
/// <summary>
/// Represents default recipe types configurator
/// </summary>
public class DefaultRecipeIngredientTypesConfiguration : IRecipeIngredientTypesConfiguration
{
    private Type _configuringIngredientType;
    private readonly Dictionary<Type, double>? _recipeTypes = new();
    /// <summary>
    /// Sets needed ingredient
    /// </summary>
    /// <typeparam name="TIngredient">Concrete ingredient type</typeparam>
    public IRecipeIngredientTypesConfiguration NeedIngredient<TIngredient>() where TIngredient : Ingredient, new()
    {
        _configuringIngredientType = typeof(TIngredient);
        return this;
    }
    /// <summary>
    /// Sets necessary weight of ingredient
    /// </summary>
    /// <param name="weight"></param>
    public void InWeightOf(double weight)
    {
        if (weight <= 0)
        {
            throw new ArgumentException("You can't ask value less or equal to 0");
        }
        _recipeTypes.Add(_configuringIngredientType, weight);
    }
    /// <summary>
    /// Creates dictionary with configured ingredient types and their necessary weights
    /// </summary>
    /// <returns></returns>
    public IDictionary<Type, double> Configure() => _recipeTypes;
}