namespace PhlegmaticOne.Eatery.Lib.Recipies;
/// <summary>
/// Represents contract for recipe builders
/// </summary>
public interface IRecipeBuilder
{
    /// <summary>
    /// Configures builder for recipe creating
    /// </summary>
    /// <typeparam name="TRecipeTypesConfiguration">Type configurating using in recipe ingredients</typeparam>
    /// <typeparam name="TRecipeProcessSequenceBuilder">Type configurating sequence of recipe preparing</typeparam>
    /// <param name="configureIngredientsAction">Configurating types action</param>
    /// <param name="configureProcessSequenceAction">Configurating sequence action</param>
    /// <returns></returns>
    IRecipeBuilder Configure<TRecipeTypesConfiguration, TRecipeProcessSequenceBuilder>
                   (Action<TRecipeTypesConfiguration> configureIngredientsAction,
                    Action<TRecipeProcessSequenceBuilder> configureProcessSequenceAction)
                    where TRecipeTypesConfiguration : IRecipeIngredientTypesConfiguration, new()
                    where TRecipeProcessSequenceBuilder : IRecipeProcessSequenceBuilder, new();
    /// <summary>
    /// Creates recipe fron configuring builder
    /// </summary>
    /// <returns></returns>
    Recipe Create();
}
