using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents contract for eatery applications builders
/// </summary>
public interface IEateryApplicationBuilder
{
    /// <summary>
    /// Sets specified storage container in use of application
    /// </summary>
    void UseStorageContainer(StoragesContainerBase storageContainer);
    /// <summary>
    /// Sets specified ingredient processes container in use of application
    /// </summary>
    void UseIngredientProcessesContainer(IngredientProcessContainerBase ingredientProcessContainer);
    /// <summary>
    /// Sets specified intermediate processes container in use of application
    /// </summary>
    void UseIntermediateProcessContainer(IntermediateProcessContainerBase intermediateProcessContainer);
    /// <summary>
    /// Sets specified production capacities container in use of application
    /// </summary>
    void UseProductionCapacityContainer(ProductionCapacitiesContainerBase productionCapacityContainer);
    /// <summary>
    /// Sets specified eatery workers container in use of application
    /// </summary>
    void UseEateryWorkersContainer(EateryWorkersContainerBase eateryWorkersContainer);
    /// <summary>
    /// Sets specified eatery menu in use of application
    /// </summary>
    void UseEateryMenu(EateryMenuBase eateryMenu);
    /// <summary>
    /// Sets specified orders container in use of application
    /// </summary>
    void UseOrdersContainer(OrdersContainerBase ordersContainer);
    /// <summary>
    /// Builds eatery application from configured data
    /// </summary>
    EateryApplication Build();
}
