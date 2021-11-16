namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents prepared default ingredient process container builder instance
/// </summary>
/// <typeparam name="TProcess">Ingredient process type</typeparam>
public class DefaultProcessContainerBuilder : IIngredientProcessContainerBuilder
{
    private readonly Dictionary<Type, IEnumerable<IngredientProcess>> _processes = new();
    public IngredientProcessContainerBase Build()
    {
        var newProcesses = new Dictionary<Type, IList<IngredientProcess>>();
        foreach (var process in _processes)
        {
            newProcesses.Add(process.Key, process.Value.ToList());
        }
        return new DefaultProcessContainer(newProcesses);
    }

    public IIngredientProcessContainerBuilder ConfigureProcess<TProcess, TProcessBuilder>(Action<TProcessBuilder> initializerProcessAction)
                                                    where TProcess : IngredientProcess, new()
                                                    where TProcessBuilder : IIngredientProcessBuilder<TProcess>, new()
    {
        var processBuilder = new TProcessBuilder();
        initializerProcessAction(processBuilder);
        _processes.Add(typeof(TProcess), processBuilder.Build());
        return this;
    }
}
