using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents contract for ingredient process container builder
/// </summary>
/// <typeparam name="TProcess">Ingredient process type</typeparam>
public interface IProcessContainerBuilder<TProcess> where TProcess : DomainProductProcess, new()
{
    /// <summary>
    /// Registers ingredient type as possible to be processed by process of TProcess type
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    /// <param name="initializer">Process builder for ingredient process</param>
    /// <returns>Instance of current container builder</returns>
    IProcessContainerBuilder<TProcess> RegisterAsPossibleToProcess<TIngredient>
        (Action<IProcessBuilder<TProcess>> initializer) where TIngredient : DomainProductToPrepare;
    /// <summary>
    /// Creates a container with registered ingredient types and their processes
    /// </summary>
    /// <returns></returns>
    IProcessContainer Build();
}
