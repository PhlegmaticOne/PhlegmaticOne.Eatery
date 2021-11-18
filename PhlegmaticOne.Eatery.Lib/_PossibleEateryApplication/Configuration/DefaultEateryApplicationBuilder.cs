using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents default eatery application builder
/// </summary>
public class DefaultEateryApplicationBuilder : IEateryApplicationBuilder
{
    private StoragesContainerBase _storageContainer;
    private IngredientProcessContainerBase _ingredientProcessContainer;
    private IntermediateProcessContainerBase _intermediateProcessContainer;
    private ProductionCapacitiesContainerBase _productionCapacityContainer;
    private EateryMenuBase _eateryMenu;
    private EateryWorkersContainerBase _eateryWorkersContainer;
    private OrdersContainerBase _ordersContainer;
    public EateryApplication Build() =>
        new(_storageContainer, _ingredientProcessContainer, _intermediateProcessContainer,
            _productionCapacityContainer, _eateryMenu, _eateryWorkersContainer, _ordersContainer);

    public void UseEateryMenu(EateryMenuBase eateryMenu) =>
        _eateryMenu = eateryMenu;

    public void UseEateryWorkersContainer(EateryWorkersContainerBase eateryWorkersContainer) =>
        _eateryWorkersContainer = eateryWorkersContainer;

    public void UseIngredientProcessesContainer(IngredientProcessContainerBase ingredientProcessContainer) =>
        _ingredientProcessContainer = ingredientProcessContainer;

    public void UseIntermediateProcessContainer(IntermediateProcessContainerBase intermediateProcessContainer) =>
        _intermediateProcessContainer = intermediateProcessContainer;

    public void UseOrdersContainer(OrdersContainerBase ordersContainer) => _ordersContainer = ordersContainer;

    public void UseProductionCapacityContainer(ProductionCapacitiesContainerBase productionCapacityContainer) =>
        _productionCapacityContainer = productionCapacityContainer;

    public void UseStorageContainer(StoragesContainerBase storageContainer) =>
        _storageContainer = storageContainer;
    public override string ToString() => GetType().Name;
}
