using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public class DefaultProcessSequenceBuilder : IRecipeProcessSequenceBuilder
{
    private IProcessContainer _processContainer;
    private readonly Queue<DomainProductProcess> _processesToPrepare;
    public DefaultProcessSequenceBuilder() => _processesToPrepare = new();

    public IRecipeProcessSequenceBuilder InsertInSequence<TIngredient>()
        where TIngredient : DomainProductToPrepare, new()
    {
        var process = _processContainer.GetProcessOf<TIngredient>();
        if(process is null)
        {
            throw new ArgumentException("Process is not registered in process container");
        }
        _processesToPrepare.Enqueue(process);
        return this;
    }

    public IRecipeProcessSequenceBuilder SetSource(IProcessContainer processContainer)
    {
        _processContainer = processContainer;
        return this;
    }

    public Queue<DomainProductProcess> BuildRecipeSequence() => _processesToPrepare;
}
