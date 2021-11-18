using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents controller which is responsible for detailed serialization of applization containers
/// </summary>
public class DetailedSerializationController : EateryApplicationControllerBase
{
    private readonly EateryApplication _eateryApplication;
    /// <summary>
    /// Initializes new DetailedSerializationController instance
    /// </summary>
    public DetailedSerializationController() { }
    /// <summary>
    /// Initializes new DetailedSerializationController instance
    /// </summary>
    /// <param name="eateryApplication">Specified eatery application</param>
    public DetailedSerializationController(EateryApplication eateryApplication) => _eateryApplication = eateryApplication;
    /// <summary>
    /// Serializes application menu
    /// </summary>
    /// <typeparam name="TMenuSerializer">Serializer of menu</typeparam>
    /// <param name="serializeMenuRequest">Request with path to file to serialize menu</param>
    [EateryWorker(typeof(Manager))]
    public void SerializeMenuState<TMenuSerializer>(IApplicationRequest<string> serializeMenuRequest)
    where TMenuSerializer : IEateryApplicationSerializer<EateryMenuBase>, new()
    {
        if (IsInRole(serializeMenuRequest.Worker, nameof(SerializeMenuState)) == false)
        {
            return;
        }
        var serializer = new TMenuSerializer()
        {
            SavingPlacePath = serializeMenuRequest.RequestData1
        };
        serializer.Save(_eateryApplication.EateryMenu);
    }
    /// <summary>
    /// Serializes application menu async
    /// </summary>
    /// <typeparam name="TMenuSerializer">Serializer of menu</typeparam>
    /// <param name="serializeMenuAsyncRequest">Request with path to file to serialize menu</param>
    [EateryWorker(typeof(Manager))]
    public async Task SerializeMenuStateAsync<TMenuSerializer>(IApplicationRequest<string> serializeMenuAsyncRequest)
        where TMenuSerializer : IEateryApplicationSerializer<EateryMenuBase>, new()
    {
        if (IsInRole(serializeMenuAsyncRequest.Worker, nameof(SerializeMenuStateAsync)) == false)
        {
            return;
        }
        var serializer = new TMenuSerializer()
        {
            SavingPlacePath = serializeMenuAsyncRequest.RequestData1
        };
        await serializer.SaveAsync(_eateryApplication.EateryMenu);
    }
    /// <summary>
    /// Serializes application storages
    /// </summary>
    /// <typeparam name="TStorageSerializer">Serializer of storage container</typeparam>
    /// <param name="serializeStoragesRequest">Request with path to file to serialize storage container</param>
    [EateryWorker(typeof(Manager))]
    public void SerializeStoragesState<TStorageSerializer>(IApplicationRequest<string> serializeStoragesRequest)
        where TStorageSerializer : IEateryApplicationSerializer<StoragesContainerBase>, new()
    {
        if (IsInRole(serializeStoragesRequest.Worker, nameof(SerializeStoragesState)) == false)
        {
            return;
        }
        var serializer = new TStorageSerializer()
        {
            SavingPlacePath = serializeStoragesRequest.RequestData1
        };
        serializer.Save(_eateryApplication.StorageContainer);
    }
    /// <summary>
    /// Serializes application storages async
    /// </summary>
    /// <typeparam name="TStorageSerializer">Serializer of storage container</typeparam>
    /// <param name="serializeStoragesAsyncRequest">Request with path to file to serialize storage container</param>
    [EateryWorker(typeof(Manager))]
    public async Task SerializeStoragesStateAsync<TStorageSerializer>
        (IApplicationRequest<string> serializeStoragesAsyncRequest)
        where TStorageSerializer : IEateryApplicationSerializer<StoragesContainerBase>, new()
    {
        if (IsInRole(serializeStoragesAsyncRequest.Worker, nameof(SerializeStoragesStateAsync)) == false)
        {
            return;
        }
        var serializer = new TStorageSerializer()
        {
            SavingPlacePath = serializeStoragesAsyncRequest.RequestData1
        };
        await serializer.SaveAsync(_eateryApplication.StorageContainer);
    }
    /// <summary>
    /// Serializes application ingredient processes
    /// </summary>
    /// <typeparam name="TIngredientProcessesSerializer">Serializer of ingredient processes container</typeparam>
    /// <param name="serializeIngredientsProcessesRequest">Request with path to file to serialize ingredient processes container</param>
    [EateryWorker(typeof(Manager))]
    public void SerializeIngredientProcessesState<TIngredientProcessesSerializer>
        (IApplicationRequest<string> serializeIngredientsProcessesRequest)
        where TIngredientProcessesSerializer : IEateryApplicationSerializer<IngredientProcessContainerBase>, new()
    {
        if (IsInRole(serializeIngredientsProcessesRequest.Worker, nameof(SerializeIngredientProcessesState)) == false)
        {
            return;
        }
        var serializer = new TIngredientProcessesSerializer()
        {
            SavingPlacePath = serializeIngredientsProcessesRequest.RequestData1
        };
        serializer.Save(_eateryApplication.IngredientProcessContainer);
    }
    /// <summary>
    /// Serializes application ingredient processes async
    /// </summary>
    /// <typeparam name="TIngredientProcessesSerializer">Serializer of ingredient processes container</typeparam>
    /// <param name="serializeIngredientsProcessesAsyncRequest">Request with path to file to serialize ingredient processes container</param>
    [EateryWorker(typeof(Manager))]
    public async Task SerializeIngredientProcessesStateAsync<TIngredientProcessesSerializer>
        (IApplicationRequest<string> serializeIngredientsProcessesAsyncRequest)
        where TIngredientProcessesSerializer : IEateryApplicationSerializer<IngredientProcessContainerBase>, new()
    {
        if (IsInRole(serializeIngredientsProcessesAsyncRequest.Worker, nameof(SerializeIngredientProcessesStateAsync)) == false)
        {
            return;
        }
        var serializer = new TIngredientProcessesSerializer()
        {
            SavingPlacePath = serializeIngredientsProcessesAsyncRequest.RequestData1
        };
        await serializer.SaveAsync(_eateryApplication.IngredientProcessContainer);
    }
    /// <summary>
    /// Serializes application intermediate processes
    /// </summary>
    /// <typeparam name="TIntermediateProcessesSerializer">Serializer of intermediate processes container</typeparam>
    /// <param name="serializeIntermadiateProcessesRequest">Request with path to file to serialize intermediate processes container</param>
    [EateryWorker(typeof(Manager))]
    public void SerializeIntermediateProcessesState<TIntermediateProcessesSerializer>
        (IApplicationRequest<string> serializeIntermadiateProcessesRequest)
        where TIntermediateProcessesSerializer : IEateryApplicationSerializer<IntermediateProcessContainerBase>, new()
    {
        if (IsInRole(serializeIntermadiateProcessesRequest.Worker, nameof(SerializeIntermediateProcessesState)) == false)
        {
            return;
        }
        var serializer = new TIntermediateProcessesSerializer()
        {
            SavingPlacePath = serializeIntermadiateProcessesRequest.RequestData1
        };
        serializer.Save(_eateryApplication.IntermediateProcessContainer);
    }
    /// <summary>
    /// Serializes application intermediate processes async
    /// </summary>
    /// <typeparam name="TIntermediateProcessesSerializer">Serializer of intermediate processes container</typeparam>
    /// <param name="serializeIntermadiateProcessesAsyncRequest">Request with path to file to serialize intermediate processes container</param>
    [EateryWorker(typeof(Manager))]
    public async Task SerializeIntermediateProcessesStateAsync<TIntermediateProcessesSerializer>
        (IApplicationRequest<string> serializeIntermadiateProcessesAsyncRequest)
        where TIntermediateProcessesSerializer : IEateryApplicationSerializer<IntermediateProcessContainerBase>, new()
    {
        if (IsInRole(serializeIntermadiateProcessesAsyncRequest.Worker, nameof(SerializeIntermediateProcessesStateAsync)) == false)
        {
            return;
        }
        var serializer = new TIntermediateProcessesSerializer()
        {
            SavingPlacePath = serializeIntermadiateProcessesAsyncRequest.RequestData1
        };
        await serializer.SaveAsync(_eateryApplication.IntermediateProcessContainer);
    }
    /// <summary>
    /// Serializes application production capacities
    /// </summary>
    /// <typeparam name="TCapacitiesSerializer">Serializer of production capacities container</typeparam>
    /// <param name="serializeCapacitiesRequest">Request with path to file to serialize production capacities container</param>
    [EateryWorker(typeof(Manager))]
    public void SerializeProductionCapacitiesState<TCapacitiesSerializer>
        (IApplicationRequest<string> serializeCapacitiesRequest)
        where TCapacitiesSerializer : IEateryApplicationSerializer<ProductionCapacitiesContainerBase>, new()
    {
        if (IsInRole(serializeCapacitiesRequest.Worker, nameof(SerializeProductionCapacitiesState)) == false)
        {
            return;
        }
        var serializer = new TCapacitiesSerializer()
        {
            SavingPlacePath = serializeCapacitiesRequest.RequestData1
        };
        serializer.Save(_eateryApplication.ProductionCapacityContainer);
    }
    /// <summary>
    /// Serializes application production capacities async
    /// </summary>
    /// <typeparam name="TCapacitiesSerializer">Serializer of production capacities container</typeparam>
    /// <param name="serializeCapacitiesAsyncRequest">Request with path to file to serialize production capacities container</param>
    [EateryWorker(typeof(Manager))]
    public async Task SerializeProductionCapacitiesStateAsync<TCapacitiesSerializer>
        (IApplicationRequest<string> serializeCapacitiesAsyncRequest)
        where TCapacitiesSerializer : IEateryApplicationSerializer<ProductionCapacitiesContainerBase>, new()
    {
        if (IsInRole(serializeCapacitiesAsyncRequest.Worker, nameof(SerializeProductionCapacitiesStateAsync)) == false)
        {
            return;
        }
        var serializer = new TCapacitiesSerializer()
        {
            SavingPlacePath = serializeCapacitiesAsyncRequest.RequestData1
        };
        await serializer.SaveAsync(_eateryApplication.ProductionCapacityContainer);
    }
    /// <summary>
    /// Serializes application workers
    /// </summary>
    /// <typeparam name="TWorkersSerializer">Serializer of workers container</typeparam>
    /// <param name="serializeWorkersRequest">Request with path to file to serialize workers container</param>
    [EateryWorker(typeof(Manager))]
    public void SerializeWorkersState<TWorkersSerializer>(IApplicationRequest<string> serializeWorkersRequest)
        where TWorkersSerializer : IEateryApplicationSerializer<EateryWorkersContainerBase>, new()
    {
        if (IsInRole(serializeWorkersRequest.Worker, nameof(SerializeWorkersState)) == false)
        {
            return;
        }
        var serializer = new TWorkersSerializer()
        {
            SavingPlacePath = serializeWorkersRequest.RequestData1
        };
        serializer.Save(_eateryApplication.EateryWorkersContainer);
    }
    /// <summary>
    /// Serializes application workers async
    /// </summary>
    /// <typeparam name="TWorkersSerializer">Serializer of workers container</typeparam>
    /// <param name="serializeWorkersAsyncRequest">Request with path to file to serialize workers container</param>
    [EateryWorker(typeof(Manager))]
    public async Task SerializeWorkersStateAsync<TWorkersSerializer>(IApplicationRequest<string> serializeWorkersAsyncRequest)
        where TWorkersSerializer : IEateryApplicationSerializer<EateryWorkersContainerBase>, new()
    {
        if (IsInRole(serializeWorkersAsyncRequest.Worker, nameof(SerializeWorkersStateAsync)) == false)
        {
            return;
        }
        var serializer = new TWorkersSerializer()
        {
            SavingPlacePath = serializeWorkersAsyncRequest.RequestData1
        };
        await serializer.SaveAsync(_eateryApplication.EateryWorkersContainer);
    }
    /// <summary>
    /// Serializes application orders
    /// </summary>
    /// <typeparam name="TOrdersSerializer">Serializer of orders container</typeparam>
    /// <param name="serializeOrdersRequest">Request with path to file to serialize orders container</param>
    [EateryWorker(typeof(Manager))]
    public void SerializeOrdersState<TOrdersSerializer>(IApplicationRequest<string> serializeOrdersRequest)
        where TOrdersSerializer : IEateryApplicationSerializer<OrdersContainerBase>, new()
    {
        if (IsInRole(serializeOrdersRequest.Worker, nameof(SerializeOrdersState)) == false)
        {
            return;
        }
        var serializer = new TOrdersSerializer()
        {
            SavingPlacePath = serializeOrdersRequest.RequestData1
        };
        serializer.Save(_eateryApplication.OrdersContainer);
    }
    /// <summary>
    /// Serializes application orders async
    /// </summary>
    /// <typeparam name="TOrdersSerializer">Serializer of orders container</typeparam>
    /// <param name="serializeOrdersAsyncRequest">Request with path to file to serialize orders container</param>
    [EateryWorker(typeof(Manager))]
    public async Task SerializeOrdersStateAsync<TOrdersSerializer>(IApplicationRequest<string> serializeOrdersAsyncRequest)
        where TOrdersSerializer : IEateryApplicationSerializer<OrdersContainerBase>, new()
    {
        if (IsInRole(serializeOrdersAsyncRequest.Worker, nameof(SerializeOrdersStateAsync)) == false)
        {
            return;
        }
        var serializer = new TOrdersSerializer()
        {
            SavingPlacePath = serializeOrdersAsyncRequest.RequestData1
        };
        await serializer.SaveAsync(_eateryApplication.OrdersContainer);
    }
}
