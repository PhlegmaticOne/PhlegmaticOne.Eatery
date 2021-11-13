using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;

public class DefaultProductionCapacityContainerBuilder : IProductionCapacityContainerBuilder
{
    private readonly Dictionary<Type, int> _processesConfiguration = new();
    public IProductionCapacityContainer Build() => new DefaultProductionCapacityContainer(_processesConfiguration);

    public IProductionCapacityContainerBuilder SetMaximalIngredientsToProcess<TProcess>(int maximalProcessedIngredients) where TProcess : DomainProductProcess, new()
    {
        _processesConfiguration.Add(typeof(TProcess), maximalProcessedIngredients);
        return this;
    }
}
