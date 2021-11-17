using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Recipies;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class RecipeController : EateryApplicationControllerBase
{
    private readonly IngredientProcessContainerBase _ingredientProcessContainer;
    private readonly IntermediateProcessContainerBase _intermediateProcessContainer;
    private readonly EateryMenuBase _eateryMenuBase;

    internal RecipeController(IngredientProcessContainerBase ingredientProcessContainer,
                              IntermediateProcessContainerBase intermediateProcessContainer,
                              EateryMenuBase eateryMenuBase)
    {
        _ingredientProcessContainer = ingredientProcessContainer;
        _intermediateProcessContainer = intermediateProcessContainer;
        _eateryMenuBase = eateryMenuBase;
    }

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
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DomainProductProcess> GetMostExpensiveProcessOverDish(EmptyApplicationRequest getRecipeByNameRequest)
    {
        if (IsInRole(getRecipeByNameRequest.Worker, nameof(GetMostExpensiveProcessOverDish)) == false)
        {
            return GetDefaultAccessDeniedRespond<DomainProductProcess>(getRecipeByNameRequest.Worker);
        }
        var maxPriceIngredientProcess = _ingredientProcessContainer.MaxPriceProcess();
        var maxPriceIntemediateProcess = _intermediateProcessContainer.MaxPriceProcess();
        return new DefaultApplicationRespond<DomainProductProcess>
            (maxPriceIngredientProcess.Price.Amount > maxPriceIntemediateProcess.Price.Amount ? maxPriceIngredientProcess : maxPriceIntemediateProcess,
            ApplicationRespondType.Success, "Most expensive process returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DomainProductProcess> GetLeastExpensiveProcessOverDish(EmptyApplicationRequest getRecipeByNameRequest)
    {
        if (IsInRole(getRecipeByNameRequest.Worker, nameof(GetMostExpensiveProcessOverDish)) == false)
        {
            return GetDefaultAccessDeniedRespond<DomainProductProcess>(getRecipeByNameRequest.Worker);
        }
        var maxPriceIngredientProcess = _ingredientProcessContainer.MinPriceProcess();
        var maxPriceIntemediateProcess = _intermediateProcessContainer.MinPriceProcess();
        return new DefaultApplicationRespond<DomainProductProcess>
            (maxPriceIngredientProcess.Price.Amount < maxPriceIntemediateProcess.Price.Amount ? maxPriceIngredientProcess : maxPriceIntemediateProcess,
            ApplicationRespondType.Success, "Least expensive process returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DomainProductProcess> GetLongestProcessOverDish(EmptyApplicationRequest getRecipeByNameRequest)
    {
        if (IsInRole(getRecipeByNameRequest.Worker, nameof(GetMostExpensiveProcessOverDish)) == false)
        {
            return GetDefaultAccessDeniedRespond<DomainProductProcess>(getRecipeByNameRequest.Worker);
        }
        var maxPriceIngredientProcess = _ingredientProcessContainer.MaxTimeProcess();
        var maxPriceIntemediateProcess = _intermediateProcessContainer.MaxTimeProcess();
        return new DefaultApplicationRespond<DomainProductProcess>
            (maxPriceIngredientProcess.Price.Amount > maxPriceIntemediateProcess.Price.Amount ? maxPriceIngredientProcess : maxPriceIntemediateProcess,
            ApplicationRespondType.Success, "Longest process returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DomainProductProcess> GetShortestProcessOverDish(EmptyApplicationRequest getRecipeByNameRequest)
    {
        if (IsInRole(getRecipeByNameRequest.Worker, nameof(GetMostExpensiveProcessOverDish)) == false)
        {
            return GetDefaultAccessDeniedRespond<DomainProductProcess>(getRecipeByNameRequest.Worker);
        }
        var maxPriceIngredientProcess = _ingredientProcessContainer.MinTimeProcess();
        var maxPriceIntemediateProcess = _intermediateProcessContainer.MinTimeProcess();
        return new DefaultApplicationRespond<DomainProductProcess>
            (maxPriceIngredientProcess.Price.Amount < maxPriceIntemediateProcess.Price.Amount ? maxPriceIngredientProcess : maxPriceIntemediateProcess,
            ApplicationRespondType.Success, "Shortest process returned");
    }
}
