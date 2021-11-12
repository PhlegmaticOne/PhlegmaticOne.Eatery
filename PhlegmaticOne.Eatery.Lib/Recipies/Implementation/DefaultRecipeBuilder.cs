namespace PhlegmaticOne.Eatery.Lib.Recipies;

public class DefaultRecipeBuilder : IRecipeBuilder
{
    private readonly Recipe _recipe = new();
    public IRecipeBuilder Configure<TRecipeTypesConfiguration, TRecipeProcessSequenceBuilder>
        (Action<TRecipeTypesConfiguration> configureIngredientsAction,
         Action<TRecipeProcessSequenceBuilder> configureProcessSequenceAction)
         where TRecipeTypesConfiguration : IRecipeIngredientTypesConfiguration, new()
         where TRecipeProcessSequenceBuilder : IRecipeProcessSequenceBuilder, new()
    {
        var configureIngredients = new TRecipeTypesConfiguration();
        configureIngredientsAction.Invoke(configureIngredients);
        _recipe.IngredientsTakesPartInPreparing = configureIngredients.Configure();
        var configureProcesses = new TRecipeProcessSequenceBuilder();
        configureProcessSequenceAction.Invoke(configureProcesses);
        _recipe.ProcessesQueueToPrepareDish = configureProcesses.BuildRecipeSequence();
        return this;
    }

    public Recipe Create() => _recipe;
}
