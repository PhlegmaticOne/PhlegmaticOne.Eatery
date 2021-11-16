namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public class DefaultIntermediateProcessContainer : IntermediateProcessContainerBase
{
    public DefaultIntermediateProcessContainer(IDictionary<Type, IList<IntermediateProcess>> intermediateProcesses) :
        base(intermediateProcesses) { }
    public static IIntermediateProcessContainerBuilder GetDefaultIntermediateProcessContainerBuilder() =>
        new DefaultIntermediateProcessContainerBuilder();
}
