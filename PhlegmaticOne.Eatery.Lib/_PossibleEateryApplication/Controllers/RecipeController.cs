using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Recipies;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class RecipeController : EateryApplicationControllerBase
{
    private readonly IngredientProcessContainerBase _ingredientProcessContainer;
    private readonly IntermediateProcessContainerBase _intermediateProcessContainer;

    internal RecipeController(IngredientProcessContainerBase ingredientProcessContainer,
                              IntermediateProcessContainerBase intermediateProcessContainer)
    {
        _ingredientProcessContainer = ingredientProcessContainer;
        _intermediateProcessContainer = intermediateProcessContainer;
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
}
