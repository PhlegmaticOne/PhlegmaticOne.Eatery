namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents contract for configuring ingredients types in storage container 
/// </summary>
public interface IStorageIngredientsConfiguration
{
    /// <summary>
    /// Adds type of ingredienttto storing types of building storage
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    IStorageIngredientsConfiguration With<TIngredient>();
    /// <summary>
    /// Sets maximal value that can contain storage of building ingredient type 
    /// </summary>
    IStorageIngredientsConfiguration WithMaximalValueOfIngredient(int maximalValueOfIngredient);
    /// <summary>
    /// Gets configured ingredient types
    /// </summary>
    IDictionary<Type, double> Configure();
}
