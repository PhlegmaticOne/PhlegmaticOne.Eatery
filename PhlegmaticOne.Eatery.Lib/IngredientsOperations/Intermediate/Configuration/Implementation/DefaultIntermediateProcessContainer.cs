namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public class DefaultIntermediateProcessContainer : IntermediateProcessContainerBase
{
    public DefaultIntermediateProcessContainer()
    {

    }
    [Newtonsoft.Json.JsonConstructor]
    public DefaultIntermediateProcessContainer(Dictionary<Type, List<IntermediateProcess>> intermediateProcesses) :
        base(intermediateProcesses)
    { }
    public static IIntermediateProcessContainerBuilder GetDefaultIntermediateProcessContainerBuilder() =>
        new DefaultIntermediateProcessContainerBuilder();
}
