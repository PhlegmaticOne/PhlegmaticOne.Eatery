using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class DetailedSerializationController : EateryApplicationControllerBase
{
    private readonly EateryApplication _eateryApplication;
    public DetailedSerializationController()
    {
    }
    public DetailedSerializationController(EateryApplication eateryApplication) => _eateryApplication = eateryApplication;
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
    [EateryWorker(typeof(Manager))]
    public async Task SerializeStoragesStateAsync<TStorageSerializer>(IApplicationRequest<string> serializeStoragesAsyncRequest)
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
    [EateryWorker(typeof(Manager))]
    public void SerializeIngredientProcessesState<TIngredientProcessesSerializer>(IApplicationRequest<string> serializeIngredientsProcessesRequest)
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
    [EateryWorker(typeof(Manager))]
    public async Task SerializeIngredientProcessesStateAsync<TIngredientProcessesSerializer>(IApplicationRequest<string> serializeIngredientsProcessesAsyncRequest)
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
    [EateryWorker(typeof(Manager))]
    public void SerializeIntermediateProcessesState<TIntermediateProcessesSerializer>(IApplicationRequest<string> serializeIntermadiateProcessesRequest)
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
    [EateryWorker(typeof(Manager))]
    public async Task SerializeIntermediateProcessesStateAsync<TIntermediateProcessesSerializer>(IApplicationRequest<string> serializeIntermadiateProcessesAsyncRequest)
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
    [EateryWorker(typeof(Manager))]
    public void SerializeProductionCapacitiesState<TCapacitiesSerializer>(IApplicationRequest<string> serializeCapacitiesRequest)
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
    [EateryWorker(typeof(Manager))]
    public async Task SerializeProductionCapacitiesStateAsync<TCapacitiesSerializer>(IApplicationRequest<string> serializeCapacitiesAsyncRequest)
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
