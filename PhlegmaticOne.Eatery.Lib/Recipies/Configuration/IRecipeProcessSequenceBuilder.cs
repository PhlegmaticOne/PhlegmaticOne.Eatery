using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public interface IRecipeProcessSequenceBuilder
{
    void InsertInSequence<TProcess, TIngredient>()
                                   where TIngredient : Ingredient, new()
                                   where TProcess : IngredientProcess, new();
    void InsertInSequence<TProcess>() where TProcess : IntermediateProcess, new();
    void SetSources(IngredientProcessContainerBase ingredientProcessContainer,
                    IntermediateProcessContainerBase intermideateProcessContainer);
    Queue<IngredientsOperations.DomainProductProcess> BuildRecipeSequence();
}