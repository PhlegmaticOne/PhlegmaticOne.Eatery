namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents prepared default ingredient process container builder instance
/// </summary>
/// <typeparam name="TProcess">Ingredient process type</typeparam>
public class DefaultIngredientProcessContainerBuilder : IIngredientProcessContainerBuilder
{
    private readonly Dictionary<Type, IEnumerable<IngredientProcess>> _processes = new();
    /// <summary>
    /// Creates a container with registered ingredient types and their processes
    /// </summary>
    public IngredientProcessContainerBase Build()
    {
        var newProcesses = new Dictionary<Type, List<IngredientProcess>>();
        foreach (var process in _processes)
        {
            newProcesses.Add(process.Key, process.Value.ToList());
        }
        return new DefaultIngredientProcessContainer(newProcesses);
    }
    /// <summary>
    /// Configures new ingredient process
    /// </summary>
    /// <param name="initializer">Initializing process builder action</param>
    /// <returns></returns>
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
