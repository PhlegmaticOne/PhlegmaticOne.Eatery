using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents eatery application class which is composite of all application containers
/// </summary>
public class EateryApplication
{
    internal StoragesContainerBase StorageContainer;
    internal IngredientProcessContainerBase IngredientProcessContainer;
    internal IntermediateProcessContainerBase IntermediateProcessContainer;
    internal ProductionCapacitiesContainerBase ProductionCapacityContainer;
    internal EateryMenuBase EateryMenu;
    internal EateryWorkersContainerBase EateryWorkersContainer;
    internal OrdersContainerBase OrdersContainer;
    /// <summary>
    /// Initializes new EateryApplication instance
    /// </summary>
    private EateryApplication() { }
    /// <summary>
    /// Initializes new EateryApplication instance
    /// </summary>
    /// <param name="storageContainer">Specified StoragesContainerBase</param>
    /// <param name="ingredientProcessContainer">Specified IngredientProcessContainerBase</param>
    /// <param name="intermediateProcessContainer">Specified IntermediateProcessContainerBase</param>
    /// <param name="productionCapacityContainer">Specified ProductionCapacitiesContainerBase</param>
    /// <param name="eateryMenu">Specified EateryMenuBase</param>
    /// <param name="eateryWorkersContainer">Specified EateryWorkersContainerBase</param>
    /// <param name="ordersContainer">Specified OrdersContainerBase</param>
    /// <exception cref="ArgumentNullException">Any container is null</exception>
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
    /// <summary>
    /// Creates EateryApplication instance from specified eatery application builder
    /// </summary>
    /// <param name="applicationBuilderAction">Action initializing eatery application builder</param>
    public static EateryApplication Create<TApplicationBuilder>(Action<TApplicationBuilder> applicationBuilderAction)
                                    where TApplicationBuilder : IEateryApplicationBuilder, new()
    {
        var builer = new TApplicationBuilder();
        applicationBuilderAction(builer);
        return builer.Build();
    }
    /// <summary>
    /// Creates EateryApplication instance from specified eatery application async initializer
    /// </summary>
    public static Task<EateryApplication> CreateAsync(IEateryApplicationAsyncInitializer eateryAsyncInitializer)
    {
        var instance = new EateryApplication();
        return instance.InitAsync(eateryAsyncInitializer);
    }
    /// <summary>
    /// Configures all necessary application controllers and return container with them
    /// </summary>
    public IEateryApplicationControllersContainer Run() => new DefaultEateryApplicationControllersContainer(
        new List<EateryApplicationControllerBase>()
        {
            new CommonSerializationController(this),
            new DetailedSerializationController(this),
            new EateryMenuController(EateryMenu),
            new IngredientsController(StorageContainer),
            new OrderController(EateryMenu, OrdersContainer),
            new OrderQueueController(EateryMenu),
            new PreparingDishController(StorageContainer, ProductionCapacityContainer, OrdersContainer),
            new ProductionCapacitiesController(ProductionCapacityContainer),
            new RecipeController(IngredientProcessContainer, IntermediateProcessContainer, EateryMenu),
            new StatiticsController(EateryMenu, OrdersContainer, IngredientProcessContainer, IntermediateProcessContainer),
            new StoragesController(StorageContainer),
            new WorkersController(EateryWorkersContainer),
        });
    /// <summary>
    /// Gets specified container by its type
    /// </summary>
    internal object? GetContainer<TContainer>() => typeof(TContainer).Name switch
    {
        "StoragesContainerBase" => StorageContainer,
        "IngredientProcessContainerBase" => IngredientProcessContainer,
        "IntermediateProcessContainerBase" => IntermediateProcessContainer,
        "ProductionCapacitiesContainerBase" => ProductionCapacityContainer,
        "EateryMenuBase" => EateryMenu,
        "EateryWorkersContainer" => EateryWorkersContainer,
        "OrdersContainer" => OrdersContainer,
        _ => null
    };
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
    public override string ToString() => "Eatery application is running";
}
