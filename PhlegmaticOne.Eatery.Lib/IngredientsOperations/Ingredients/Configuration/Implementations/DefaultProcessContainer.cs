using PhlegmaticOne.Eatery.Lib.Extensions;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using System.Collections;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents prepared default process container for ingredient processes
/// </summary>
public class DefaultProcessContainer : IngredientProcessContainerBase
{
    internal DefaultProcessContainer(IDictionary<Type, IList<IngredientProcess>> possibleTypesToProcess) :
        base(possibleTypesToProcess) { }
    /// <summary>
    /// Returns default builder for default container of specifiedingredient process type
    /// </summary>
    /// <typeparam name="TProcess">Ingredient process type</typeparam>
    public static IIngredientProcessContainerBuilder GetDefaultContainerBuilder() => new DefaultProcessContainerBuilder();
}
