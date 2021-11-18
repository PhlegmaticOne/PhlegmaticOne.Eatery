using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.Recipies;
/// <summary>
/// Represents default ingredient recipe builder
/// </summary>
public class DefaultRecipeBuilder : IRecipeBuilder
{
    private readonly Recipe _recipe = new();
    private readonly IngredientProcessContainerBase _ingredientProcessContainer;
    private readonly IntermediateProcessContainerBase _intermediateProcessContainer;
    /// <summary>
    /// Initializes new DefaultRecipeBuilder
    /// </summary>
    /// <param name="recipeName">Recipe name</param>
    public DefaultRecipeBuilder(string recipeName)
    {
        _recipe = new();
        _recipe.Name = recipeName;
    }
    /// <summary>
    /// Initializes new DefaultRecipeBuilder
    /// </summary>
    /// <param name="ingredientProcessContainer">Specified ingredient process container</param>
    /// <param name="intermediateProcessContainer">Specified intermediate process container</param>
    /// <param name="recipeName">Recipe name</param>
    public DefaultRecipeBuilder(IngredientProcessContainerBase ingredientProcessContainer,
                                IntermediateProcessContainerBase intermediateProcessContainer,
                                string recipeName) : this(recipeName)
    {
        _ingredientProcessContainer = ingredientProcessContainer;
        _intermediateProcessContainer = intermediateProcessContainer;
    }
    /// <summary>
    /// Configures builder for recipe creating
    /// </summary>
    /// <typeparam name="TRecipeTypesConfiguration">Type configurating using in recipe ingredients</typeparam>
    /// <typeparam name="TRecipeProcessSequenceBuilder">Type configurating sequence of recipe preparing</typeparam>
    /// <param name="configureIngredientsAction">Configurating types action</param>
    /// <param name="configureProcessSequenceAction">Configurating sequence action</param>
    /// <returns></returns>
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
    /// <summary>
    /// Creates recipe fron configuring builder
    /// </summary>
    /// <returns></returns>
    public Recipe Create() => _recipe;
}
