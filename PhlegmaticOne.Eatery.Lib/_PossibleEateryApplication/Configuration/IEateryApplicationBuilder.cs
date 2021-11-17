using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public interface IEateryApplicationBuilder
{
    void UseStorageContainer(StoragesContainerBase storageContainer);
    void UseIngredientProcessesContainer(IngredientProcessContainerBase ingredientProcessContainer);
    void UseIntermediateProcessContainer(IntermediateProcessContainerBase intermediateProcessContainer);
    void UseProductionCapacityContainer(ProductionCapacitiesContainerBase productionCapacityContainer);
    void UseEateryWorkersContainer(EateryWorkersContainerBase eateryWorkersContainer);
    void UseEateryMenu(EateryMenuBase eateryMenu);
    void UseOrdersContainer(OrdersContainerBase ordersContainer);
    EateryApplication Build();
}
