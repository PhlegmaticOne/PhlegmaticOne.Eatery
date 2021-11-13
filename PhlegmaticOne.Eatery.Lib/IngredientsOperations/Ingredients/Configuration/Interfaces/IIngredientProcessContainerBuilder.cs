namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents contract for ingredient process container builder
/// </summary>
public interface IIngredientProcessContainerBuilder
{
    IIngredientProcessContainerBuilder ConfigureProcess<TProcess, TProcessBuilder>
                             (Action<TProcessBuilder> initializer)
                             where TProcess : IngredientProcess, new()
                             where TProcessBuilder : IIngredientProcessBuilder<TProcess>, new();
    /// <summary>
    /// Creates a container with registered ingredient types and their processes
    /// </summary>
    /// <returns></returns>
    IIngredientProcessContainer Build();
}
