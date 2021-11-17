using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class EateryApplication
{
    internal StoragesContainerBase StorageContainer;
    internal IngredientProcessContainerBase IngredientProcessContainer;
    internal IntermediateProcessContainerBase IntermediateProcessContainer;
    internal ProductionCapacitiesContainerBase ProductionCapacityContainer;
    internal EateryMenuBase EateryMenu;
    internal EateryWorkersContainerBase EateryWorkersContainer;
    internal OrdersContainerBase OrdersContainer;

    internal EateryApplication(StoragesContainerBase storageContainer,
                               IngredientProcessContainerBase ingredientProcessContainer,
                               IntermediateProcessContainerBase intermediateProcessContainer,
                               ProductionCapacitiesContainerBase productionCapacityContainer,
                               EateryMenuBase eateryMenu,
                               EateryWorkersContainerBase eateryWorkersContainer,
                               OrdersContainerBase ordersContainer)
    {
        StorageContainer = storageContainer ?? throw new ArgumentNullException(nameof(storageContainer));
        IngredientProcessContainer = ingredientProcessContainer ?? throw new ArgumentNullException(nameof(ingredientProcessContainer));
        IntermediateProcessContainer = intermediateProcessContainer ?? throw new ArgumentNullException(nameof(intermediateProcessContainer));
        ProductionCapacityContainer = productionCapacityContainer ?? throw new ArgumentNullException(nameof(productionCapacityContainer));
        EateryMenu = eateryMenu;
        EateryWorkersContainer = eateryWorkersContainer;
        OrdersContainer = ordersContainer;
    }
    private EateryApplication() { }

    public static EateryApplication Create<TApplicationBuilder>(Action<TApplicationBuilder> applicationBuilderAction)
                                    where TApplicationBuilder : IEateryApplicationBuilder, new()
    {
        var builer = new TApplicationBuilder();
        applicationBuilderAction(builer);
        return builer.Build();
    }
    private async Task<EateryApplication> InitAsync(IEateryApplicationAsyncInitializer eateryAsyncInitializer)
    {
        StorageContainer = await eateryAsyncInitializer.LoadStoragesAsync();
        IngredientProcessContainer = await eateryAsyncInitializer.LoadIngredientProcessesAsync();
        IntermediateProcessContainer = await eateryAsyncInitializer.LoadIntermediateProcessesAsync();
        ProductionCapacityContainer = await eateryAsyncInitializer.LoadProductionCapacitiesAsync();
        EateryMenu = await eateryAsyncInitializer.LoadEateryMenuAsync();
        EateryWorkersContainer = await eateryAsyncInitializer.LoadWorkersAsync();
        OrdersContainer = await eateryAsyncInitializer.LoadOrdersAsync();
        return this;
    }
    public static Task<EateryApplication> CreateAsync(IEateryApplicationAsyncInitializer eateryAsyncInitializer)
    {
        var instance = new EateryApplication();
        return instance.InitAsync(eateryAsyncInitializer);
    }
    public IEateryApplicationControllersContainer Run()
    {
        return new DefaultEateryApplicationControllersContainer(new List<EateryApplicationControllerBase>()
        {
            new WorkersController(EateryWorkersContainer),
            new ProductionCapacitiesController(ProductionCapacityContainer),
            new StoragesController(StorageContainer),
            new IngredientsController(StorageContainer),
            new EateryMenuController(EateryMenu),
            new OrderController(EateryMenu),
            new OrderQueueController(EateryMenu),
            new PreparingDishController(StorageContainer, ProductionCapacityContainer, OrdersContainer)
        });
    }
}
