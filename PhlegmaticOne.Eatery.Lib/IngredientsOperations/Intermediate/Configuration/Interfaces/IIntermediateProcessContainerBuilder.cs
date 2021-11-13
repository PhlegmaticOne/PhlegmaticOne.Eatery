namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public interface IIntermediateProcessContainerBuilder
{
    IIntermediateProcessContainerBuilder ConfigureProcess<TProcess, TProcessBuilder>
                                        (Action<TProcessBuilder> initializer)
                                        where TProcess : IntermediateProcess, new()
                                        where TProcessBuilder : IIntermediateProcessBuilder<TProcess>, new();
    IIntermediateProcessContainer Build();
}
