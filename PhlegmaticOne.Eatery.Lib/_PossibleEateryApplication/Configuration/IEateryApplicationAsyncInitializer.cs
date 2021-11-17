using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public interface IEateryApplicationAsyncInitializer
{
    Task<StoragesContainerBase> LoadStoragesAsync();
    Task<IngredientProcessContainerBase> LoadIngredientProcessesAsync();
    Task<IntermediateProcessContainerBase> LoadIntermediateProcessesAsync();
    Task<ProductionCapacitiesContainerBase> LoadProductionCapacitiesAsync();
    Task<EateryMenuBase> LoadEateryMenuAsync();
    Task<EateryWorkersContainerBase> LoadWorkersAsync();
    Task<OrdersContainerBase> LoadOrdersAsync();
}
