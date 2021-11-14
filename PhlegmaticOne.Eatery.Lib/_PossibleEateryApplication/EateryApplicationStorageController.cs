using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.Storages;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class EateryApplicationStorageController : EateryApplicationControllerBase
{
    private readonly IStorageContainer _storageContainer;
    public EateryApplicationStorageController(IStorageContainer storageContainer) =>
        _storageContainer = storageContainer ?? throw new ArgumentNullException(nameof(storageContainer));


    [EateryWorker(typeof(Chief), typeof(Cook), typeof(Manager))]
    public IApplicationRespond<IReadOnlyCollection<Storage>> GetAllStorages
           (IApplicationRequest<object> applicationRequest)
    {
        var respond = new DefaultApplicationRespond<IReadOnlyCollection<Storage>>();
        if (IsInRole(applicationRequest.Worker, nameof(GetAllStorages)) == false)
        {
            return respond.Update(null, ApplicationRespondType.AccessDenied,
                                  $"{applicationRequest.Worker.GetType()} has no rights for seeing storages");
        }
        try
        {
            return respond.Update(new ReadOnlyCollection<Storage>(_storageContainer.AllStorages().ToList()),
                                  ApplicationRespondType.Success, "Storages returned");
        }
        catch(Exception ex)
        {
            return respond.Update(null, ApplicationRespondType.InternalError, ex.Message);
        }
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<IStorageBuilder<TStorage>> GetDefaultStorageBuilder<TStorage>
                              (IApplicationRequest<object> createNewStorageRequest) where TStorage : Storage, new()
    {
        var respond = new DefaultApplicationRespond<IStorageBuilder<TStorage>>();
        if(IsInRole(createNewStorageRequest.Worker, nameof(GetDefaultStorageBuilder)) == false)
        {
            return respond.Update(null, ApplicationRespondType.AccessDenied, "You have no rights to create new storage");
        }
        try
        {
            return respond.Update(new DefaultStorageBuilder<TStorage>(), ApplicationRespondType.Success, "Builder returned");
        }
        catch (Exception ex)
        {
            return respond.Update(null, ApplicationRespondType.InternalError, ex.Message);
        }
    }
    [EateryWorker(typeof (Manager))]
    public IApplicationRespond<bool> Add<TStorage> (IApplicationRequest<IEnumerable<TStorage>> storageAddRequest) where TStorage : Storage, new()
    {
        var respond = new DefaultApplicationRespond<bool>();
        if (IsInRole(storageAddRequest.Worker, nameof(Add)) == false)
        {
            return respond.Update(false, ApplicationRespondType.AccessDenied, "You have no rights to add new storage");
        }
        try
        {
            foreach (var storage in storageAddRequest.RequestData)
            {
                _storageContainer.Add(storage);
            }
            return respond.Update(true, ApplicationRespondType.Success, "Storages were added");
        }
        catch (Exception ex)
        {
            return respond.Update(false, ApplicationRespondType.InternalError, ex.Message);
        }
    }

    [EateryWorker(typeof(Chief), typeof(Cook))]
    public IApplicationRespond<IReadOnlyDictionary<Ingredient, double>>
        GetAllExistingIngredients(IApplicationRespond<object> getAllIngredientsRespond)
    {
        return null;
    }
}
