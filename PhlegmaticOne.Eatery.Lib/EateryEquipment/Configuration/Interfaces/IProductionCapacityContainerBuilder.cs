using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;

public interface IProductionCapacityContainerBuilder
{
    IProductionCapacityContainerBuilder SetMaximalIngredientsToProcess<TProcess>
                                        (int maximalProcessedIngredients)
                                        where TProcess : IngredientsOperations.DomainProductProcess, new();
    ProductionCapacitiesContainerBase Build();
}
