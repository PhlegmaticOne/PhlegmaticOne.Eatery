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
    void With<TIngredient>();
    /// <summary>
    /// Gets configured ingredient types
    /// </summary>
    IEnumerable<Type> Configure();
}
