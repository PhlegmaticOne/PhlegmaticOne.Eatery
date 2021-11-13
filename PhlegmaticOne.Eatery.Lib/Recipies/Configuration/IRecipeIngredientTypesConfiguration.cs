using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public interface IRecipeIngredientTypesConfiguration
{
    IRecipeIngredientTypesConfiguration NeedIngredient<TIngredient>() where TIngredient : Ingredient, new();
    void InWeightOf(double weight);
    IDictionary<Type, double> Configure();
}
