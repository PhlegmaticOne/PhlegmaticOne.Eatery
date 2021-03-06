namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents default intermediate process container builder
/// </summary>
public class DefaultIntermediateProcessContainerBuilder : IIntermediateProcessContainerBuilder
{
    private readonly Dictionary<Type, IEnumerable<IntermediateProcess>> _intermediateProcesses = new();
    public IIntermediateProcessContainerBuilder ConfigureProcess<TProcess, TProcessBuilder>
                                                (Action<TProcessBuilder> initializer)
                                                where TProcess : IntermediateProcess, new()
                                                where TProcessBuilder : IIntermediateProcessBuilder<TProcess>, new()
    {
        var builder = new TProcessBuilder();
        initializer(builder);
        _intermediateProcesses.Add(typeof(TProcess), builder.Build());
        return this;
    }
    public IntermediateProcessContainerBase Build()
    {
        var newProcesses = new Dictionary<Type, List<IntermediateProcess>>();
        foreach (var process in _intermediateProcesses)
        {
            newProcesses.Add(process.Key, process.Value.ToList());
        }
        return new DefaultIntermediateProcessContainer(newProcesses);
    }
    public override string ToString() => GetType().Name;
}
