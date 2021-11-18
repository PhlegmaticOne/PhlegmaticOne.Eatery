using PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;
/// <summary>
/// Represents json application initializer from json files with application needed containers
/// </summary>
public class JsonEateryApplicationAsyncInitializer : IEateryApplicationAsyncInitializer
{
    /// <summary>
    /// Pathes to files with data for application containers
    /// </summary>
    private readonly IDictionary<Type, string> _pathesToJsonFilesWithData;
    /// <summary>
    /// Initializes new JsonEateryApplicationAsyncInitializer instance
    /// </summary>
    /// <param name="pathesToJsonFilesWithData">Pathes to files with data for application containers</param>
    /// <exception cref="ArgumentNullException">Pathes to files with data for application containers is null</exception>
    public JsonEateryApplicationAsyncInitializer(IDictionary<Type, string> pathesToJsonFilesWithData) =>
        _pathesToJsonFilesWithData = pathesToJsonFilesWithData ?? throw new ArgumentNullException(nameof(pathesToJsonFilesWithData));
    /// <summary>
    /// Loads eatery menu from json file asynchronously
    /// </summary>
    /// <returns>Base eatery menu deserialized from file</returns>
    public async Task<EateryMenuBase?> LoadEateryMenuAsync() =>
        await new DefaultEateryMenuJsonWorker(_pathesToJsonFilesWithData[typeof(EateryMenuBase)])
        .LoadAsync<EateryMenu>();
    /// <summary>
    /// Loads ingredient processes container from json file asynchronously
    /// </summary>
    /// <returns>Base ingredient processes container deserialized from file</returns>
    public async Task<IngredientProcessContainerBase?> LoadIngredientProcessesAsync() =>
        await new DefaultIngredientProcessesJsonWorker(_pathesToJsonFilesWithData[typeof(IngredientProcessContainerBase)])
        .LoadAsync<DefaultIngredientProcessContainer>();
    /// <summary>
    /// Loads intermediate processes container from json file asynchronously
    /// </summary>
    /// <returns>Base intermediate processes container deserialized from file</returns>
    public async Task<IntermediateProcessContainerBase?> LoadIntermediateProcessesAsync() =>
        await new DefaultIntermediateProcessesJsonWorker(_pathesToJsonFilesWithData[typeof(IntermediateProcessContainerBase)])
        .LoadAsync<DefaultIntermediateProcessContainer>();
    /// <summary>
    /// Loads orders container from json file asynchronously
    /// </summary>
    /// <returns>Base orders container deserialized from file</returns>
    public async Task<OrdersContainerBase?> LoadOrdersAsync() =>
        await new DefaultOrdersJsonWorker(_pathesToJsonFilesWithData[typeof(OrdersContainerBase)])
        .LoadAsync<DefaultOrderContainer>();
    /// <summary>
    /// Loads production capacities container from json file asynchronously
    /// </summary>
    /// <returns>Base production capacities container deserialized from file</returns>
    public async Task<ProductionCapacitiesContainerBase?> LoadProductionCapacitiesAsync() =>
        await new DefaultProductionCapacitiesJsonWorker(_pathesToJsonFilesWithData[typeof(ProductionCapacitiesContainerBase)])
        .LoadAsync<DefaultProductionCapacityContainer>();
    /// <summary>
    /// Loads storages container from json file asynchronously
    /// </summary>
    /// <returns>Base production capacities container deserialized from file</returns>
    public async Task<StoragesContainerBase?> LoadStoragesAsync() =>
        await new DefaultStoragesJsonWorker(_pathesToJsonFilesWithData[typeof(StoragesContainerBase)])
        .LoadAsync<DefaultStorageContainer>();
    /// <summary>
    /// Loads eatery workers container from json file asynchronously
    /// </summary>
    /// <returns>Base eatery workers container deserialized from file</returns>
    public async Task<EateryWorkersContainerBase?> LoadWorkersAsync() =>
        await new DefaultWorkersJsonWorker(_pathesToJsonFilesWithData[typeof(EateryWorkersContainerBase)])
        .LoadAsync<DefaultEateryWorkersContainer>();
    public override string ToString() => GetType().Name;
}
