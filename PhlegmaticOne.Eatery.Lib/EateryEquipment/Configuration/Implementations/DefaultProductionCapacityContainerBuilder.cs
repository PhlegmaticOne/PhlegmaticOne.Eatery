namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;

public class DefaultProductionCapacityContainerBuilder : IProductionCapacityContainerBuilder
{
    private readonly Dictionary<Type, int> _processesConfiguration = new();
    public ProductionCapacitiesContainerBase Build() => new DefaultProductionCapacityContainer(_processesConfiguration);

    public IProductionCapacityContainerBuilder SetMaximalIngredientsToProcess<TProcess>(int maximalProcessedIngredients) where TProcess : IngredientsOperations.DomainProductProcess, new()
    {
        _processesConfiguration.Add(typeof(TProcess), maximalProcessedIngredients);
        return this;
    }
}
