using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents controller which is responsible for operating with storages
/// </summary>
public class StoragesController : EateryApplicationControllerBase
{
    private readonly StoragesContainerBase _storageContainer;
    /// <summary>
    /// Initializes new StoragesController instance
    /// </summary>
    public StoragesController() { }
    /// <summary>
    /// Initializes new StoragesController instance
    /// </summary>
    /// <param name="storageContainer">Specified storage container</param>
    internal StoragesController(StoragesContainerBase storageContainer) =>
        _storageContainer = storageContainer;
    /// <summary>
    /// Gets all storages 
    /// </summary>
    /// <param name="getAllStoragesRequest">Empty request</param>
    /// <returns>Respond with storage container with all storages</returns>
    [EateryWorker(typeof(Chief), typeof(Cook))]
    public IApplicationRespond<StoragesContainerBase> GetAllStorages(EmptyApplicationRequest getAllStoragesRequest)
    {
        if (IsInRole(getAllStoragesRequest.Worker, nameof(GetAllStorages)) == false)
        {
            return GetDefaultAccessDeniedRespond<StoragesContainerBase>(getAllStoragesRequest.Worker);
        }
        return new DefaultApplicationRespond<StoragesContainerBase>(_storageContainer, ApplicationRespondType.Success,
                                                                   "Storages returned");
    }
    /// <summary>
    /// Get default storage builder for creatin new storage
    /// </summary>
    /// <typeparam name="TStorage">Type of storage to create</typeparam>
    /// <param name="createNewStorageRequest">Empty request</param>
    /// <returns>Respond with default storage builder</returns>
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
    /// <summary>
    /// Adds new storages in application storage container
    /// </summary>
    /// <typeparam name="TStorage">Type of storages to add</typeparam>
    /// <param name="storagesAddRequest">Request with collection storages to add</param>
    /// <returns>True - all storages were added</returns>
    [EateryWorker(typeof(Manager))]
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
    /// <summary>
    /// Removes storage from application storage container
    /// </summary>
    /// <param name="storageRemoveRequest">Request with storage to remove</param>
    /// <returns>True - storage was removed</returns>
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
