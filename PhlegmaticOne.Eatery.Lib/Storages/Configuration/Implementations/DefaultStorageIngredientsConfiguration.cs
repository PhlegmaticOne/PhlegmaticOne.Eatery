namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents prepared default storage ingredients configuration
/// </summary>
public class DefaultStorageIngredientsConfiguration : IStorageIngredientsConfiguration
{
    private Type _currentTypeToConfigure;
    private readonly Dictionary<Type, double> _ingredientsTypes;
    /// <summary>
    /// Initializes new default storage ingredients configuration
    /// </summary>
    public DefaultStorageIngredientsConfiguration() => _ingredientsTypes = new();
    /// <summary>
    /// Adds type of ingredienttto storing types of building storage
    /// </summary>
    /// <typeparam name="TIngredient">Ingredient type</typeparam>
    public IStorageIngredientsConfiguration With<TIngredient>()
    {
        _currentTypeToConfigure = typeof(TIngredient);
        return this;
    }
    /// <summary>
    /// Sets maximal value that can contain storage of building ingredient type 
    /// </summary>
    public IStorageIngredientsConfiguration WithMaximalValueOfIngredient(int maximalValueOfIngredient)
    {
        _ingredientsTypes.Add(_currentTypeToConfigure, maximalValueOfIngredient);
        return this;
    }
    /// <summary>
    /// Gets configured ingredient types
    /// </summary>
    public IDictionary<Type, double> Configure() => _ingredientsTypes;
    /// <summary>
    /// Gets string representation of default storage ingredients configuration
    /// </summary>
    public override string ToString() => string.Format("Default storage ingredients configuration");
}
