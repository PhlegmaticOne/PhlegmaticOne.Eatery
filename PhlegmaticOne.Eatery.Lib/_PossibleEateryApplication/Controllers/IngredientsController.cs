using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class IngredientsController : EateryApplicationControllerBase
{
    private readonly StoragesContainerBase _storageContainer;
    internal IngredientsController(StoragesContainerBase storageContainer)
    {
        _storageContainer = storageContainer;
    }
    [EateryWorker(typeof(Chief), typeof(Cook))]
    public IApplicationRespond<IReadOnlyDictionary<Type, double>> GetAllExistingIngredients
                              (EmptyApplicationRequest getAllIngredientsRequest)
    {
        if (IsInRole(getAllIngredientsRequest.Worker, nameof(GetAllExistingIngredients)) == false)
        {
            return GetDefaultAccessDeniedRespond<IReadOnlyDictionary<Type, double>>(getAllIngredientsRequest.Worker);
        }
        return new DefaultApplicationRespond<IReadOnlyDictionary<Type, double>>
            (_storageContainer.GetAllExistingIngredients(), ApplicationRespondType.Success, "All ingredients returned");
    }
    [EateryWorker(typeof(Cook), typeof(Chief))]
    public IApplicationRespond<bool> AddIngredientsInStorage
           (IApplicationRequest<Func<Storage, bool>, IEnumerable<Ingredient>> addingIngredientsRequest)
    {
        if (IsInRole(addingIngredientsRequest.Worker, nameof(AddIngredientsInStorage)) == false)
        {
            return GetDefaultAccessDeniedRespond<bool>(addingIngredientsRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<bool>();
        var storage = _storageContainer.FirstOrDefaultStorage(addingIngredientsRequest.RequestData1);
        if (storage is null)
        {
            return respond.Update(false, ApplicationRespondType.InternalError, "There are no such storages in eatery");
        }
        var addedIngredients = 0;
        foreach (var ingredient in addingIngredientsRequest.RequestData2)
        {
            var ingredientType = ingredient.GetType();
            if (storage.ContainsIngredient(ingredientType))
            {
                addedIngredients += Convert.ToInt32(storage.TryAdd(ingredientType, ingredient.Weight));
            }
            else
            {
                return respond.Update(false, ApplicationRespondType.InternalError,
                                   $"This container cannot keep {ingredientType.Name}");
            }
        }
        return respond.Update(true, ApplicationRespondType.Success,
                             $"{addedIngredients} of {addingIngredientsRequest.RequestData2.Count()} ingredients was added");
    }

    internal IEnumerable<Storage> GetStoragesContainingIngredientType(Type ingredientType) =>
        _storageContainer.AllStorages().Where(s => s.ContainsIngredient(ingredientType));
}
