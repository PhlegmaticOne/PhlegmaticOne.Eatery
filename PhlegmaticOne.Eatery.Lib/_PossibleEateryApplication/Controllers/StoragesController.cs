using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class StoragesController : EateryApplicationControllerBase
{
    private readonly StoragesContainerBase _storageContainer;
    internal StoragesController(StoragesContainerBase storageContainer) =>
        _storageContainer = storageContainer ?? throw new ArgumentNullException(nameof(storageContainer));
    [EateryWorker(typeof(Chief), typeof(Cook))]
    public IApplicationRespond<StoragesContainerBase> GetAllStorages (EmptyApplicationRequest getAllStoragesRequest)
    {
        if(IsInRole(getAllStoragesRequest.Worker, nameof(GetAllStorages)) == false)
        {
            return GetDefaultAccessDeniedRespond<StoragesContainerBase>(getAllStoragesRequest.Worker);
        }
        return new DefaultApplicationRespond<StoragesContainerBase>(_storageContainer, ApplicationRespondType.Success,
                                                                   "Storages returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<IStorageBuilder<TStorage>> GetDefaultStorageBuilder<TStorage>
                              (EmptyApplicationRequest createNewStorageRequest) where TStorage : Storage, new()
    {
        if (IsInRole(createNewStorageRequest.Worker, nameof(GetDefaultStorageBuilder)) == false)
        {
            return GetDefaultAccessDeniedRespond<IStorageBuilder<TStorage>>(createNewStorageRequest.Worker);
        }
        return new DefaultApplicationRespond<IStorageBuilder<TStorage>>(new DefaultStorageBuilder<TStorage>(),
                                                                        ApplicationRespondType.Success,
                                                                        $"Builder for {typeof(TStorage).Name} returned");
    }
    [EateryWorker(typeof (Manager))]
    public IApplicationRespond<bool> AddNewStorages<TStorage>(IApplicationRequest<IEnumerable<TStorage>> storagesAddRequest)
                                                  where TStorage : Storage, new()
    {
        if (IsInRole(storagesAddRequest.Worker, nameof(AddNewStorages)) == false)
        {
            return GetDefaultAccessDeniedRespond<bool>(storagesAddRequest.Worker);
        }
        foreach (var storage in storagesAddRequest.RequestData1)
        {
            _storageContainer.Add(storage);
        }
        return new DefaultApplicationRespond<bool>(true, ApplicationRespondType.Success,
                                                   $"{storagesAddRequest.RequestData1.Count()} Storages were added");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<bool> RemoveStorage(IApplicationRequest<Storage> storageRemoveRequest)
    {
        if (IsInRole(storageRemoveRequest.Worker, nameof(RemoveStorage)) == false)
        {
            return GetDefaultAccessDeniedRespond<bool>(storageRemoveRequest.Worker);
        }
        var isRemoved = _storageContainer.TryRemove(storageRemoveRequest.RequestData1);
        return isRemoved ? new DefaultApplicationRespond<bool>(isRemoved, ApplicationRespondType.Success,
                                                               "Storage was deleted") :
                           new DefaultApplicationRespond<bool>(isRemoved, ApplicationRespondType.InternalError,
                                                               "There are no such storage in storage container");

    }
}
