namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public class DefaultIntermediateProcessContainer : IIntermediateProcessContainer
{
    private readonly IDictionary<Type, IList<IntermediateProcess>> _intermediateProcesses;
    public DefaultIntermediateProcessContainer(IDictionary<Type, IList<IntermediateProcess>> intermediateProcesses)
    {
        _intermediateProcesses = intermediateProcesses;
    }
    public static IIntermediateProcessContainerBuilder GetDefaultIntermediateProcessContainerBuilder() =>
        new DefaultIntermediateProcessContainerBuilder();
    public TProcess GetProcess<TProcess>(IEnumerable<Type> preferableTypesToProcess)
                                        where TProcess : IntermediateProcess, new()
    {
        if (_intermediateProcesses.TryGetValue(typeof(TProcess), out var intermediateProcesses))
        {
            var maxFittedTypes = int.MaxValue;
            var fittedProcess = intermediateProcesses.Last();
            for (int i = 0; i < intermediateProcesses.Count - 1; i++)
            {
                var curentFittedTypesCount = intermediateProcesses.ElementAt(i)
                                             .PreferableTypesToProcess.Except(preferableTypesToProcess).Count();
                if (curentFittedTypesCount < maxFittedTypes)
                {
                    fittedProcess = intermediateProcesses.ElementAt(i);
                }
            }
            return fittedProcess as TProcess;
        }
        return default;
    }

    public bool TryAdd<TProcess>(TProcess process) where TProcess : IntermediateProcess, new()
    {
        if (_intermediateProcesses.TryGetValue(typeof(TProcess), out var intermediateProcess))
        {
            if (intermediateProcess.Contains(process) == false)
            {
                intermediateProcess.Add(process);
                return true;
            }
        }
        return false;
    }

    public bool TryRemove<TProcess>() where TProcess : IntermediateProcess, new()
    {
        return false;
    }

    public bool TryUpdate<TProcess>(TProcess process) where TProcess : IntermediateProcess, new()
    {
        if (_intermediateProcesses.TryGetValue(typeof(TProcess), out var intermediateProcess))
        {
            var fitted = intermediateProcess.FirstOrDefault(p => p.Price == process.Price && p.TimeToFinish == process.TimeToFinish);
            if (fitted is not null)
            {
                intermediateProcess.Remove(fitted);
                intermediateProcess.Add(process);
                return true;
            }
        }
        return false;
    }
}
