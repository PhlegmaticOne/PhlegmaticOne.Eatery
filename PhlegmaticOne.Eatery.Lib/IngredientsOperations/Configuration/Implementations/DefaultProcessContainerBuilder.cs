using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents prepared default ingredient process container builder instance
/// </summary>
/// <typeparam name="TProcess">Ingredient process type</typeparam>
public class DefaultProcessContainerBuilder<TProcess> : IProcessContainerBuilder<TProcess> where TProcess : DomainProductProcess, new()
{
    private readonly Dictionary<Type, DomainProductProcess> _possibleTypesToProcess;
    private readonly IProcessBuilder<TProcess> _processIntializer;
    /// <summary>
    /// Initializes new default ingredient process container builder instance
    /// </summary>
    /// <param name="processBuilder">Specified ingredient process builder</param>
    /// <exception cref="ArgumentNullException">Process builder is null</exception>
    public DefaultProcessContainerBuilder(IProcessBuilder<TProcess> processBuilder)
    {
        _processIntializer = processBuilder ?? throw new ArgumentNullException(nameof(processBuilder));
        _possibleTypesToProcess = new();
    }
    /// <summary>
    /// Registers new ingredient type as possible to be operated with building process
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    /// <param name="processBuilderAction">Initializing of new ingredient process of specified type</param>
    /// <returns>Current default ingredient process container builder instance</returns>
    public IProcessContainerBuilder<TProcess> RegisterAsPossibleToProcess<TIngredient>(
        Action<IProcessBuilder<TProcess>> processBuilderAction) where TIngredient : DomainProductToPrepare
    {
        processBuilderAction.Invoke(_processIntializer);
        _possibleTypesToProcess.Add(typeof(TIngredient), _processIntializer.Build());
        return this;
    }
    /// <summary>
    /// Builds new default process container
    /// </summary>
    public IProcessContainer Build() => new DefaultProcessContainer(_possibleTypesToProcess);
    /// <summary>
    /// Gets string representation of default ingredient process container builder instance
    /// </summary>
    public override string ToString() => string.Format("Default ingredient process container builder for {0}", typeof(TProcess).Name);
}
