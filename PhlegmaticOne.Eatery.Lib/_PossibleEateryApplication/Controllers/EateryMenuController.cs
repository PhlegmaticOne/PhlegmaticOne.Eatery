using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.Recipies;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class EateryMenuController : EateryApplicationControllerBase
{
    private readonly EateryMenuBase _eateryMenu;
    internal EateryMenuController(EateryMenuBase eateryMenu) => _eateryMenu = eateryMenu;
    [EateryWorker(typeof(Chief))]
    public IApplicationRespond<bool> AddRecipeInMenu(IApplicationRequest<Recipe> addRecipeRequest)
    {
        if (IsInRole(addRecipeRequest.Worker, nameof(AddRecipeInMenu)) == false)
        {
            return GetDefaultAccessDeniedRespond<bool>(addRecipeRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<bool>();
        var recipe = addRecipeRequest.RequestData1;
        if (_eateryMenu.TryConnectNameOfDishWithRecipe(recipe.Name, recipe) == false)
        {
            return respond.Update(false, ApplicationRespondType.InternalError, $"Menu contains recipe for dish {recipe.Name}");
        }
        return respond.Update(true, ApplicationRespondType.Success, "Recipe was added");
    }
    [EateryWorker(typeof(Chief))]
    public IApplicationRespond<bool> RemoveRecipeFromMenu(IApplicationRequest<string> removeRecipeRequest)
    {
        if (IsInRole(removeRecipeRequest.Worker, nameof(RemoveRecipeFromMenu)) == false)
        {
            return GetDefaultAccessDeniedRespond<bool>(removeRecipeRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<bool>();
        var recipeName = removeRecipeRequest.RequestData1;
        if (_eateryMenu.TryRemoveRecipe(recipeName) == false)
        {
            return respond.Update(false, ApplicationRespondType.InternalError, $"Menu has no recipe {recipeName}");
        }
        return respond.Update(true, ApplicationRespondType.Success, "Recipe was removed");
    }
    [EateryWorker(typeof(Chief), typeof(Cook), typeof(Manager))]
    public IApplicationRespond<IReadOnlyDictionary<string, Recipe>>
        GetAllRecipies(EmptyApplicationRequest getAllRecipiesRequest)
    {
        if (IsInRole(getAllRecipiesRequest.Worker, nameof(RemoveRecipeFromMenu)) == false)
        {
            return GetDefaultAccessDeniedRespond<IReadOnlyDictionary<string, Recipe>>(getAllRecipiesRequest.Worker);
        }
        return new DefaultApplicationRespond<IReadOnlyDictionary<string, Recipe>>(_eateryMenu.GetAllRecipes(),
                                                                                  ApplicationRespondType.Success,
                                                                                  "Recipies was returned");
    }
    [EateryWorker(typeof(Chief), typeof(Cook), typeof(Manager))]
    public IApplicationRespond<Recipe> GetRecipeByName(IApplicationRequest<string> getRecipeByNameRequest)
    {
        if (IsInRole(getRecipeByNameRequest.Worker, nameof(RemoveRecipeFromMenu)) == false)
        {
            return GetDefaultAccessDeniedRespond<Recipe>(getRecipeByNameRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<Recipe>();
        if (_eateryMenu.TryGetRecipe(getRecipeByNameRequest.RequestData1, out Recipe recipe) == false)
        {
            return respond.Update(recipe, ApplicationRespondType.InternalError,
                   $"Recipe {getRecipeByNameRequest.RequestData1} wasn't finded");
        }
        return respond.Update(recipe, ApplicationRespondType.Success, "Recipe returned");
    }
}
