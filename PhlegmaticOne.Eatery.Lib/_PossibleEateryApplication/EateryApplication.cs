using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class EateryApplication
{
    internal readonly StoragesContainerBase StorageContainer;
    internal readonly IngredientProcessContainerBase IngredientProcessContainer;
    internal readonly IntermediateProcessContainerBase IntermediateProcessContainer;
    internal readonly ProductionCapacityContainerBase ProductionCapacityContainer;
    internal readonly EateryMenuBase EateryMenu;
    internal readonly EateryWorkersContainerBase EateryWorkersContainer;
    internal EateryApplication(StoragesContainerBase storageContainer,
                               IngredientProcessContainerBase ingredientProcessContainer,
                               IntermediateProcessContainerBase intermediateProcessContainer,
                               ProductionCapacityContainerBase productionCapacityContainer,
                               EateryMenuBase eateryMenu,
                               EateryWorkersContainerBase eateryWorkersContainer)
    {
        StorageContainer = storageContainer ?? throw new ArgumentNullException(nameof(storageContainer));
        IngredientProcessContainer = ingredientProcessContainer ?? throw new ArgumentNullException(nameof(ingredientProcessContainer));
        IntermediateProcessContainer = intermediateProcessContainer ?? throw new ArgumentNullException(nameof(intermediateProcessContainer));
        ProductionCapacityContainer = productionCapacityContainer ?? throw new ArgumentNullException(nameof(productionCapacityContainer));
        EateryMenu = eateryMenu;
        EateryWorkersContainer = eateryWorkersContainer;
    }

    public static EateryApplication Create<TApplicationBuilder>(Action<TApplicationBuilder> applicationBuilderAction)
                                    where TApplicationBuilder : IEateryApplicationBuilder, new()
    {
        var builer = new TApplicationBuilder();
        applicationBuilderAction(builer);
        return builer.Build();
    }
    public IEateryApplicationControllersContainer Run()
    {
        return new DefaultEateryApplicationControllersContainer(new List<EateryApplicationControllerBase>()
        {
            new WorkersController(EateryWorkersContainer),
            new ProductionCapacitiesController(ProductionCapacityContainer),
            new StoragesController(StorageContainer),
            new IngredientsController(StorageContainer),
            new EateryMenuController(EateryMenu)
        });
    }
}
