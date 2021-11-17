using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public class DefaultProcessSequenceBuilder : IRecipeProcessSequenceBuilder
{
    private IngredientProcessContainerBase _ingredientProcessContainer;
    private IntermediateProcessContainerBase _intermediateProcessContainer;
    private readonly Queue<DomainProductProcess> _processesToPrepare;
    public DefaultProcessSequenceBuilder() => _processesToPrepare = new();
    public void InsertInSequence<TProcess, TIngredient>()
        where TProcess : IngredientProcess, new()
        where TIngredient : Ingredient, new()
    {
        var ingredientProcess = _ingredientProcessContainer.GetProcess<TProcess, TIngredient>();
        if(ingredientProcess is null)
        {
            throw new InvalidOperationException($"{typeof(TProcess).Name} not configured with {typeof(TIngredient).Name}");
        }
        _processesToPrepare.Enqueue(ingredientProcess);
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
    public Queue<DomainProductProcess> BuildRecipeSequence() => _processesToPrepare;
}
