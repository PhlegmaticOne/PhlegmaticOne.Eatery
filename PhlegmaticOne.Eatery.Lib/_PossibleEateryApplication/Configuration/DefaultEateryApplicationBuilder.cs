using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class DefaultEateryApplicationBuilder : IEateryApplicationBuilder
{
    private StoragesContainerBase _storageContainer;
    private IngredientProcessContainerBase _ingredientProcessContainer;
    private IntermediateProcessContainerBase _intermediateProcessContainer;
    private ProductionCapacityContainerBase _productionCapacityContainer;
    private EateryMenuBase _eateryMenu;
    private EateryWorkersContainerBase _eateryWorkersContainer;
    public EateryApplication Build() =>
        new(_storageContainer, _ingredientProcessContainer, _intermediateProcessContainer,
            _productionCapacityContainer, _eateryMenu, _eateryWorkersContainer);

    public void UseEateryMenu(EateryMenuBase eateryMenu) =>
        _eateryMenu = eateryMenu;

    public void UseEateryWorkersContainer(EateryWorkersContainerBase eateryWorkersContainer) => 
        _eateryWorkersContainer = eateryWorkersContainer;

    public void UseIngredientProcessesContainer(IngredientProcessContainerBase ingredientProcessContainer) =>
        _ingredientProcessContainer = ingredientProcessContainer;

    public void UseIntermediateProcessContainer(IntermediateProcessContainerBase intermediateProcessContainer) =>
        _intermediateProcessContainer = intermediateProcessContainer;

    public void UseProductionCapacityContainer(ProductionCapacityContainerBase productionCapacityContainer) =>
        _productionCapacityContainer = productionCapacityContainer;

    public void UseStorageContainer(StoragesContainerBase storageContainer) =>
        _storageContainer = storageContainer;
}
