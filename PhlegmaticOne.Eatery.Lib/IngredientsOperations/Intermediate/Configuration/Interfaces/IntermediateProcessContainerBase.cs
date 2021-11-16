namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public abstract class IntermediateProcessContainerBase
{
    internal readonly IDictionary<Type, IList<IntermediateProcess>> IntermediateProcesses;
    public IntermediateProcessContainerBase(IDictionary<Type, IList<IntermediateProcess>> intermediateProcesses)
    {
        IntermediateProcesses = intermediateProcesses;
    }
    internal virtual TProcess GetProcess<TProcess>(IEnumerable<Type> preferableTypesToProcess)
                                                  where TProcess : IntermediateProcess, new()
    {
        if (IntermediateProcesses.TryGetValue(typeof(TProcess), out var intermediateProcesses))
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
    internal virtual bool TryUpdate<TProcess>(TProcess process) where TProcess : IntermediateProcess, new()
    {
        if (IntermediateProcesses.TryGetValue(typeof(TProcess), out var intermediateProcess))
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
