using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public class DefaultProcessSequenceBuilder : IRecipeProcessSequenceBuilder
{
    private IngredientProcessContainerBase _ingredientProcessContainer;
    private IntermediateProcessContainerBase _intermediateProcessContainer;
    private readonly Queue<IngredientsOperations.DomainProductProcess> _processesToPrepare;
    public DefaultProcessSequenceBuilder() => _processesToPrepare = new();
    public void InsertInSequence<TProcess, TIngredient>()
        where TProcess : IngredientProcess, new()
        where TIngredient : Ingredient, new()
    {

        _processesToPrepare.Enqueue(_ingredientProcessContainer.GetProcess<TProcess, TIngredient>());
    }
    public void InsertInSequence<TProcess>() where TProcess : IntermediateProcess, new()
    {
        var ingredientTypes = new List<Type>();
        foreach (var process in _processesToPrepare)
        {
            if (process is IngredientProcess ingredientProcess)
            {
                ingredientTypes.Add(ingredientProcess.CurrentIngredientType);
            }
        }
        _processesToPrepare.Enqueue(_intermediateProcessContainer.GetProcess<TProcess>(ingredientTypes));
    }
    public void SetSources(IngredientProcessContainerBase ingredientProcessContainer,
                       IntermediateProcessContainerBase intermideateProcessContainer)
    {
        _ingredientProcessContainer = ingredientProcessContainer;
        _intermediateProcessContainer = intermideateProcessContainer;
    }
    public Queue<IngredientsOperations.DomainProductProcess> BuildRecipeSequence() => _processesToPrepare;
}
