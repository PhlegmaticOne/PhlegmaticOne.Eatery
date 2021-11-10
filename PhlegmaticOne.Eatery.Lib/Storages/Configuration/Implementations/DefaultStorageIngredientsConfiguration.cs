namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents prepared default storage ingredients configuration
/// </summary>
public class DefaultStorageIngredientsConfiguration : IStorageIngredientsConfiguration
{
    private readonly List<Type> _ingredientsTypes;
    /// <summary>
    /// Initializes new default storage ingredients configuration
    /// </summary>
    public DefaultStorageIngredientsConfiguration() => _ingredientsTypes = new();
    /// <summary>
    /// Adds type of ingredient to storing types of building storage
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    public void With<TIngredient>() => _ingredientsTypes.Add(typeof(TIngredient));
    /// <summary>
    /// Gets configured ingredient types
    /// </summary>
    public IEnumerable<Type> Configure() => _ingredientsTypes;
    /// <summary>
    /// Gets string representation of default storage ingredients configuration
    /// </summary>
    public override string ToString() => string.Format("Default storage ingredients configuration");
}
