namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;
/// <summary>
/// Represents default production capacity container builder
/// </summary>
public class DefaultProductionCapacityContainerBuilder : IProductionCapacityContainerBuilder
{
    private readonly Dictionary<Type, int> _processesConfiguration = new();
    /// <summary>
    /// Builds ProductionCapacitiesContainerBase from configuring capacities
    /// </summary
    public ProductionCapacitiesContainerBase Build() => new DefaultProductionCapacityContainer(_processesConfiguration);
    /// <summary>
    /// Set maximal ingredients to process with specified domain process type
    /// </summary>
    /// <typeparam name="TProcess"></typeparam>
    /// <param name="maximalProcessedIngredients"></param>
    public IProductionCapacityContainerBuilder SetMaximalIngredientsToProcess<TProcess>(int maximalProcessedIngredients) where TProcess : IngredientsOperations.DomainProductProcess, new()
    {
        _processesConfiguration.Add(typeof(TProcess), maximalProcessedIngredients);
        return this;
    }
    public override string ToString() => GetType().Name;
}
