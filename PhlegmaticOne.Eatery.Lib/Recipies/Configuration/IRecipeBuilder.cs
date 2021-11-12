namespace PhlegmaticOne.Eatery.Lib.Recipies;

public interface IRecipeBuilder
{
    IRecipeBuilder Configure<TRecipeTypesConfiguration, TRecipeProcessSequenceBuilder>
                   (Action<TRecipeTypesConfiguration> configureIngredientsAction,
                    Action<TRecipeProcessSequenceBuilder> configureProcessSequenceAction)
                    where TRecipeTypesConfiguration : IRecipeIngredientTypesConfiguration, new()
                    where TRecipeProcessSequenceBuilder : IRecipeProcessSequenceBuilder, new();
    Recipe Create();
}
