namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents default intermediate process container
/// </summary>
public class DefaultIntermediateProcessContainer : IntermediateProcessContainerBase
{
    /// <summary>
    /// Initializes new DefaultIntermediateProcessContainer
    /// </summary>
    public DefaultIntermediateProcessContainer() { }
    /// <summary>
    /// Initializes new DefaultIntermediateProcessContainer
    /// </summary>
    /// <param name="intermediateProcesses">Specified intermediate processes</param>
    [Newtonsoft.Json.JsonConstructor]
    public DefaultIntermediateProcessContainer(Dictionary<Type, List<IntermediateProcess>> intermediateProcesses) :
        base(intermediateProcesses) { }
    /// <summary>
    /// Gets default intermediate process container builder
    /// </summary>
    public static IIntermediateProcessContainerBuilder GetDefaultIntermediateProcessContainerBuilder() =>
        new DefaultIntermediateProcessContainerBuilder();
}
