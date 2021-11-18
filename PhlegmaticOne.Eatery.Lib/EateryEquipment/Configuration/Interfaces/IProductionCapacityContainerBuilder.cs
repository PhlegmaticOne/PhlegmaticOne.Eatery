using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;
/// <summary>
/// Repreents contract for production capacities container builders
/// </summary>
public interface IProductionCapacityContainerBuilder
{
    /// <summary>
    /// Set maximal ingredients to process with specified domain process type
    /// </summary>
    /// <typeparam name="TProcess"></typeparam>
    /// <param name="maximalProcessedIngredients"></param>
    IProductionCapacityContainerBuilder SetMaximalIngredientsToProcess<TProcess>
                                        (int maximalProcessedIngredients)
                                        where TProcess : DomainProductProcess, new();
    /// <summary>
    /// Builds ProductionCapacitiesContainerBase from configuring capacities
    /// </summary>
    ProductionCapacitiesContainerBase Build();
}
