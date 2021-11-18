using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.Recipies;
/// <summary>
/// Represents contract for setting sequence of processes in recipe
/// </summary>
public interface IRecipeProcessSequenceBuilder
{
    /// <summary>
    /// Inserts new ingredient process
    /// </summary>
    void InsertInSequence<TProcess, TIngredient>()
                                   where TIngredient : Ingredient, new()
                                   where TProcess : IngredientProcess, new();
    /// <summary>
    /// Inserts new intermediate process over ingredients
    /// </summary>
    void InsertInSequence<TProcess>() where TProcess : IntermediateProcess, new();
    /// <summary>
    /// Sets processes containers from which information about process will be taken
    /// </summary>
    /// <param name="ingredientProcessContainer"></param>
    /// <param name="intermideateProcessContainer"></param>
    void SetSources(IngredientProcessContainerBase ingredientProcessContainer,
                    IntermediateProcessContainerBase intermideateProcessContainer);
    /// <summary>
    /// Builds queue of processes of recipe
    /// </summary>
    Queue<DomainProductProcess> BuildRecipeSequence();
}