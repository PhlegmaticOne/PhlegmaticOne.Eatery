using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Recipies;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents controller which is responsible for operating with recipies
/// </summary>
public class RecipeController : EateryApplicationControllerBase
{
    private readonly IngredientProcessContainerBase _ingredientProcessContainer;
    private readonly IntermediateProcessContainerBase _intermediateProcessContainer;
    private readonly EateryMenuBase _eateryMenuBase;
    /// <summary>
    /// Initializes new RecipeController instance
    /// </summary>
    public RecipeController() { }
    /// <summary>
    /// Initializes new RecipeController instance
    /// </summary>
    /// <param name="ingredientProcessContainer">Specified ingredient process container</param>
    /// <param name="intermediateProcessContainer">Specified intermediate process container</param>
    /// <param name="eateryMenuBase">Specified eatery menu</param>
    internal RecipeController(IngredientProcessContainerBase ingredientProcessContainer,
                              IntermediateProcessContainerBase intermediateProcessContainer,
                              EateryMenuBase eateryMenuBase)
    {
        _ingredientProcessContainer = ingredientProcessContainer;
        _intermediateProcessContainer = intermediateProcessContainer;
        _eateryMenuBase = eateryMenuBase;
    }
    /// <summary>
    /// Gets default recipe builder for creating new recipe
    /// </summary>
    /// <param name="getRecipeBuilderRequest">Request with new recipe name</param>
    /// <returns>Respond with DefaultRecipeBuilder</returns>
    [EateryWorker(typeof(Chief))]
    public IApplicationRespond<IRecipeBuilder> GetRecipeBuilder(IApplicationRequest<string> getRecipeBuilderRequest)
    {
        if (IsInRole(getRecipeBuilderRequest.Worker, nameof(GetRecipeBuilder)) == false)
        {
            return GetDefaultAccessDeniedRespond<IRecipeBuilder>(getRecipeBuilderRequest.Worker);
        }
        return new DefaultApplicationRespond<IRecipeBuilder>
              (new DefaultRecipeBuilder(_ingredientProcessContainer,
                                        _intermediateProcessContainer,
                                        getRecipeBuilderRequest.RequestData1),
                                        ApplicationRespondType.Success,
                                        $"Builder for {getRecipeBuilderRequest.RequestData1} returned");
    }
    /// <summary>
    /// Gets recipe for dish in order
    /// </summary>
    /// <param name="getRecipeByNameRequest">Request with order</param>
    /// <returns>Respond with recipe for dosh in order</returns>
    [EateryWorker(typeof(Chief), typeof(Cook))]
    public IApplicationRespond<Order, Recipe> GetRecipeForDishInOrder(IApplicationRequest<Order> getRecipeByNameRequest)
    {
        if (IsInRole(getRecipeByNameRequest.Worker, nameof(GetRecipeForDishInOrder)) == false)
        {
            return GetDefaultAccessDeniedRespond<Order, Recipe>(getRecipeByNameRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<Order, Recipe>();
        if (_eateryMenuBase.TryGetRecipe(getRecipeByNameRequest.RequestData1.DishName, out Recipe recipe) == false)
        {
            return respond.Update(getRecipeByNameRequest.RequestData1, recipe, ApplicationRespondType.InternalError,
                   $"Recipe {getRecipeByNameRequest.RequestData1} wasn't finded");
        }
        return respond.Update(getRecipeByNameRequest.RequestData1, recipe, ApplicationRespondType.Success, "Recipe returned");
    }
}
