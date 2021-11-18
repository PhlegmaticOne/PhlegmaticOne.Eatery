namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents contract for intermediate process containers builders
/// </summary>
public interface IIntermediateProcessContainerBuilder
{
    /// <summary>
    /// Configures intermediate process
    /// </summary>
    /// <typeparam name="TProcess">Type of configuring intermediate process</typeparam>
    /// <typeparam name="TProcessBuilder">Builder for configuring process</typeparam>
    /// <param name="initializer">Initializing intermediate process builder action</param>
    /// <returns></returns>
    IIntermediateProcessContainerBuilder ConfigureProcess<TProcess, TProcessBuilder>
                                        (Action<TProcessBuilder> initializer)
                                        where TProcess : IntermediateProcess, new()
                                        where TProcessBuilder : IIntermediateProcessBuilder<TProcess>, new();
    /// <summary>
    /// Builds intermediate process container
    /// </summary>
    /// <returns></returns>
    IntermediateProcessContainerBase Build();
}
