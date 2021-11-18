using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents base operation over any ingredient
/// </summary>
public abstract class IngredientProcess : DomainProductProcess
{
    /// <summary>
    /// Initializes new IngredientProcess instance
    /// </summary>
    protected IngredientProcess() { }
    /// <summary>
    /// Initializes new IngredientProcess instance
    /// </summary>
    /// <param name="timeToFinish">Time to finish process</param>
    /// <param name="price">Price of process</param>
    [Newtonsoft.Json.JsonConstructor]
    protected IngredientProcess(TimeSpan timeToFinish, Money price) : base(timeToFinish, price) { }
    /// <summary>
    /// Current ingredient type
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    internal Type? CurrentIngredientType { get; set; }
    /// <summary>
    /// Updates preparing dish
    /// </summary>
    internal abstract void Update(DishBase dish, double weight);
}
