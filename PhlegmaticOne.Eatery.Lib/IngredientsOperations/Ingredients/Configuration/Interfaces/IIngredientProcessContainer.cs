using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents contract for ingredient process container
/// </summary>
public interface IIngredientProcessContainer
{
    /// <summary>
    /// Tries to add new ingredient process for specified ingredient type
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    /// <param name="process">Instanceof process</param>
    /// <returns>False - ingredient type is already registered or instance of process is null</returns>
    public bool TryAdd<TProcess, TIngredient>(TProcess process) where TIngredient : Ingredient, new()
                                                                  where TProcess : IngredientProcess, new();
    /// <summary>
    /// Tries to remove ingredient type and its process
    /// </summary>
    /// <typeparam name="TIngredient">Type of ingredient</typeparam>
    public bool TryRemove<TProcess, TIngredient>() where TIngredient : Ingredient, new()
                                                     where TProcess : IngredientProcess, new();
    /// <summary>
    /// Tries to update process for ingredient type
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    /// <typeparam name="TProcess">Ingredient process type</typeparam>
    /// <param name="process">New process to set</param>
    public bool TryUpdate<TIngredient, TProcess>(TProcess process)
                  where TProcess : IngredientProcess, new()
                  where TIngredient : Ingredient, new();
    public TProcess GetProcess<TProcess, TIngredient>()
                         where TProcess : IngredientProcess, new()
                         where TIngredient : Ingredient, new();
}
