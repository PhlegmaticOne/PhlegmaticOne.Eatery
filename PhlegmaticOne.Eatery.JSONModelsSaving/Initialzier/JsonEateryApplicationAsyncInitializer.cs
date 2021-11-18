using PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;

public class JsonEateryApplicationAsyncInitializer : IEateryApplicationAsyncInitializer
{
    private readonly IDictionary<Type, string> _pathesToJsonFilesWithData;
    public JsonEateryApplicationAsyncInitializer(IDictionary<Type, string> pathesToJsonFilesWithData) =>
        _pathesToJsonFilesWithData = pathesToJsonFilesWithData ?? throw new ArgumentNullException(nameof(pathesToJsonFilesWithData));
    public async Task<EateryMenuBase> LoadEateryMenuAsync() =>
        await new DefaultEateryMenuJsonWorker(_pathesToJsonFilesWithData[typeof(EateryMenuBase)])
        .LoadAsync<EateryMenu>();

    public async Task<IngredientProcessContainerBase> LoadIngredientProcessesAsync() =>
        await new DefaultIngredientProcessesJsonWorker(_pathesToJsonFilesWithData[typeof(IngredientProcessContainerBase)])
        .LoadAsync<DefaultProcessContainer>();

    public async Task<IntermediateProcessContainerBase> LoadIntermediateProcessesAsync() =>
        await new DefaultIntermediateProcessesJsonWorker(_pathesToJsonFilesWithData[typeof(IntermediateProcessContainerBase)])
        .LoadAsync<DefaultIntermediateProcessContainer>();

    public async Task<OrdersContainerBase> LoadOrdersAsync() =>
        await new DefaultOrdersJsonWorker(_pathesToJsonFilesWithData[typeof(OrdersContainerBase)])
        .LoadAsync<DefaultOrderContainer>();

    public async Task<ProductionCapacitiesContainerBase> LoadProductionCapacitiesAsync() =>
        await new DefaultProductionCapacitiesJsonWorker(_pathesToJsonFilesWithData[typeof(ProductionCapacitiesContainerBase)])
        .LoadAsync<DefaultProductionCapacityContainer>();

    public async Task<StoragesContainerBase> LoadStoragesAsync() =>
        await new DefaultStoragesJsonWorker(_pathesToJsonFilesWithData[typeof(StoragesContainerBase)])
        .LoadAsync<DefaultStorageContainer>();

    public async Task<EateryWorkersContainerBase> LoadWorkersAsync() =>
        await new DefaultWorkersJsonWorker(_pathesToJsonFilesWithData[typeof(EateryWorkersContainerBase)])
        .LoadAsync<DefaultEateryWorkersContainer>();
}
