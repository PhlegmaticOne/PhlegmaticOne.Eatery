namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents prepared default process container for ingredient processes
/// </summary>
public class DefaultIngredientProcessContainer : IngredientProcessContainerBase
{
    /// <summary>
    /// Initializes new DefaultProcessContainer instance
    /// </summary>
    public DefaultIngredientProcessContainer() { }
    /// <summary>
    /// Initializes new DefaultProcessContainer instance
    /// </summary>
    /// <param name="possibleTypesToProcess">Specified ingredient processes</param>
    [Newtonsoft.Json.JsonConstructor]
    internal DefaultIngredientProcessContainer(Dictionary<Type, List<IngredientProcess>> possibleTypesToProcess) :
        base(possibleTypesToProcess) { }
    /// <summary>
    /// Returns default builder for default container of specifiedingredient process type
    /// </summary>
    /// <typeparam name="TProcess">Ingredient process type</typeparam>
    public static IIngredientProcessContainerBuilder GetDefaultContainerBuilder() => new DefaultIngredientProcessContainerBuilder();
}
