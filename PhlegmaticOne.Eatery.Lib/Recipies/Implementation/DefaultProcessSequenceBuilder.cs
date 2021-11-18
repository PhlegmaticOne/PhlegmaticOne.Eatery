using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.Recipies;
/// <summary>
/// Represents default process sequence builder
/// </summary>
public class DefaultProcessSequenceBuilder : IRecipeProcessSequenceBuilder
{
    private IngredientProcessContainerBase _ingredientProcessContainer;
    private IntermediateProcessContainerBase _intermediateProcessContainer;
    private readonly Queue<DomainProductProcess> _processesToPrepare;
    public DefaultProcessSequenceBuilder() => _processesToPrepare = new();
    /// <summary>
    /// Inserts new ingredient process
    /// </summary>
    public void InsertInSequence<TProcess, TIngredient>()
        where TProcess : IngredientProcess, new()
        where TIngredient : Ingredient, new()
    {
        var ingredientProcess = _ingredientProcessContainer.GetProcess<TProcess, TIngredient>();
        if (ingredientProcess is null)
        {
            throw new InvalidOperationException($"{typeof(TProcess).Name} not configured with {typeof(TIngredient).Name}");
        }
        _processesToPrepare.Enqueue(ingredientProcess);
    }
    /// <summary>
    /// Inserts new intermediate process over ingredients
    /// </summary>
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
    /// <summary>
    /// Sets processes containers from which information about process will be taken
    /// </summary>
    /// <param name="ingredientProcessContainer"></param>
    /// <param name="intermideateProcessContainer"></param>
    public void SetSources(IngredientProcessContainerBase ingredientProcessContainer,
                           IntermediateProcessContainerBase intermideateProcessContainer)
    {
        _ingredientProcessContainer = ingredientProcessContainer;
        _intermediateProcessContainer = intermideateProcessContainer;
    }
    /// <summary>
    /// Builds queue of processes of recipe
    /// </summary>
    public Queue<DomainProductProcess> BuildRecipeSequence() => _processesToPrepare;
}
