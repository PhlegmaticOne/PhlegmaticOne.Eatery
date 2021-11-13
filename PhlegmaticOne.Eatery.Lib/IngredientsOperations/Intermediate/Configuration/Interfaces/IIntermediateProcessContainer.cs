namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public interface IIntermediateProcessContainer
{
    public bool TryAdd<TProcess>(TProcess process) where TProcess : IntermediateProcess, new();
    public bool TryRemove<TProcess>() where TProcess : IntermediateProcess, new();
    public bool TryUpdate<TProcess>(TProcess process) where TProcess : IntermediateProcess, new();
    public TProcess GetProcess<TProcess>(IEnumerable<Type> preferableTypesToProcess) where TProcess : IntermediateProcess, new();
}
