using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.Recipies;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents controller which is responsible for operating with eatery menu
/// </summary>
public class EateryMenuController : EateryApplicationControllerBase
{
    private readonly EateryMenuBase _eateryMenu;
    /// <summary>
    /// initializes new EateryMenuController instance
    /// </summary>
    public EateryMenuController() { }
    /// <summary>
    /// initializes new EateryMenuController instance
    /// </summary>
    /// <param name="eateryMenu">Specified eatery menu</param>
    internal EateryMenuController(EateryMenuBase eateryMenu) => _eateryMenu = eateryMenu;
    /// <summary>
    /// Adds new recipe in menu
    /// </summary>
    /// <param name="addRecipeRequest">Request with recipe to add</param>
    /// <returns>True - recipe was added</returns>
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
    /// <summary>
    /// Removes recipe from menu
    /// </summary>
    /// <param name="removeRecipeRequest">Request with recipe name to remove</param>
    /// <returns>True - recipe was removed</returns>
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
    /// <summary>
    /// Gets all recipies in menu 
    /// </summary>
    /// <param name="getAllRecipiesRequest">Empty request</param>
    /// <returns>Read-only dictionary: keys - recipe names, values - recipies</returns>
    [EateryWorker(typeof(Chief), typeof(Cook))]
    public IApplicationRespond<IReadOnlyDictionary<string, Recipe>>
        GetAllRecipies(EmptyApplicationRequest getAllRecipiesRequest)
    {
        if (IsInRole(getAllRecipiesRequest.Worker, nameof(GetAllRecipies)) == false)
        {
            return GetDefaultAccessDeniedRespond<IReadOnlyDictionary<string, Recipe>>(getAllRecipiesRequest.Worker);
        }
        return new DefaultApplicationRespond<IReadOnlyDictionary<string, Recipe>>(_eateryMenu.GetAllRecipes(),
                                                                                  ApplicationRespondType.Success,
                                                                                  "Recipies was returned");
    }
    /// <summary>
    /// Gets whole eatery menu
    /// </summary>
    /// <param name="getAllRecipiesRequest">Empty request</param>
    /// <returns>Eatery menu</returns>
    [EateryWorker(typeof(Chief), typeof(Cook), typeof(Manager))]
    public IApplicationRespond<EateryMenuBase>
        GetEateryMenu(EmptyApplicationRequest getAllRecipiesRequest)
    {
        if (IsInRole(getAllRecipiesRequest.Worker, nameof(GetAllRecipies)) == false)
        {
            return GetDefaultAccessDeniedRespond<EateryMenuBase>(getAllRecipiesRequest.Worker);
        }
        return new DefaultApplicationRespond<EateryMenuBase>(_eateryMenu, ApplicationRespondType.Success,
                                                                                  "Menu was returned");
    }
}
