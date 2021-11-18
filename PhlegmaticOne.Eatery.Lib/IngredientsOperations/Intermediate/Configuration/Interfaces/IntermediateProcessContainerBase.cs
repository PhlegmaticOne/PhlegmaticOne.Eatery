namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public abstract class IntermediateProcessContainerBase
{
    public IntermediateProcessContainerBase()
    {

    }
    [Newtonsoft.Json.JsonProperty]
    internal readonly Dictionary<Type, List<IntermediateProcess>> IntermediateProcesses;
    [Newtonsoft.Json.JsonConstructor]
    public IntermediateProcessContainerBase(Dictionary<Type, List<IntermediateProcess>> intermediateProcesses)
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
    public IntermediateProcess MinPriceProcess()
    {
        IntermediateProcess result = null;
        foreach (var process in IntermediateProcesses.Values)
        {
            var temp = process.MaxBy(x => x.Price.Amount);
            if (result is null || result.Price.Amount < temp.Price.Amount)
            {
                result = temp;
            }
        }
        return null;
    }
    public IntermediateProcess MaxPriceProcess()
    {
        IntermediateProcess result = null;
        foreach (var process in IntermediateProcesses.Values)
        {
            var temp = process.MinBy(x => x.Price.Amount);
            if (result is null || result.Price.Amount > temp.Price.Amount)
            {
                result = temp;
            }
        }
        return null;
    }
    public IntermediateProcess MinTimeProcess()
    {
        IntermediateProcess result = null;
        foreach (var process in IntermediateProcesses.Values)
        {
            var temp = process.MinBy(x => x.TimeToFinish);
            if (result is null || result.TimeToFinish > temp.TimeToFinish)
            {
                result = temp;
            }
        }
        return null;
    }
    public IntermediateProcess MaxTimeProcess()
    {
        IntermediateProcess result = null;
        foreach (var process in IntermediateProcesses.Values)
        {
            var temp = process.MaxBy(x => x.TimeToFinish);
            if (result is null || result.TimeToFinish < temp.TimeToFinish)
            {
                result = temp;
            }
        }
        return null;
    }
}
