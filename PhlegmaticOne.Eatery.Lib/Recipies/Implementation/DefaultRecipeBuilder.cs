using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.Recipies;

public class DefaultRecipeBuilder : IRecipeBuilder
{
    private readonly Recipe _recipe = new();
    private readonly IngredientProcessContainerBase _ingredientProcessContainer;
    private readonly IntermediateProcessContainerBase _intermediateProcessContainer;

    public DefaultRecipeBuilder(string recipeName)
    {
        _recipe = new();
        _recipe.Name = recipeName;
    }
    public DefaultRecipeBuilder(IngredientProcessContainerBase ingredientProcessContainer,
                                IntermediateProcessContainerBase intermediateProcessContainer,
                                string recipeName) : this(recipeName)
    {
        _ingredientProcessContainer = ingredientProcessContainer;
        _intermediateProcessContainer = intermediateProcessContainer;
    }
    public IRecipeBuilder Configure<TRecipeTypesConfiguration, TRecipeProcessSequenceBuilder>
        (Action<TRecipeTypesConfiguration> configureIngredientsAction,
         Action<TRecipeProcessSequenceBuilder> configureProcessSequenceAction)
         where TRecipeTypesConfiguration : IRecipeIngredientTypesConfiguration, new()
         where TRecipeProcessSequenceBuilder : IRecipeProcessSequenceBuilder, new()
    {
        var configureIngredients = new TRecipeTypesConfiguration();
        configureIngredientsAction.Invoke(configureIngredients);
        _recipe.IngredientsTakesPartInPreparing = configureIngredients.Configure() as Dictionary<Type, double>;
        var configureProcesses = new TRecipeProcessSequenceBuilder();
        configureProcesses.SetSources(_ingredientProcessContainer, _intermediateProcessContainer);
        configureProcessSequenceAction.Invoke(configureProcesses);
        _recipe.ProcessesQueueToPrepareDish = configureProcesses.BuildRecipeSequence();
        return this;
    }

    public Recipe Create() => _recipe;
}
