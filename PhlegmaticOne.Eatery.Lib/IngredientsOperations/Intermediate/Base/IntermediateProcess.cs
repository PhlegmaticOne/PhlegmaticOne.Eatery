using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents base instance for intermediate processes
/// </summary>
public abstract class IntermediateProcess : DomainProductProcess
{
    /// <summary>
    /// Initializes new IntermediateProcess instance
    /// </summary>
    protected IntermediateProcess() { }
    /// <summary>
    /// Initializes new IntermediateProcess instance
    /// </summary>
    /// <param name="timeToFinish">Specified time to process</param>
    /// <param name="price">Specified price</param>
    [Newtonsoft.Json.JsonConstructor]
    protected IntermediateProcess(TimeSpan timeToFinish, Money price) : base(timeToFinish, price) { }
    /// <summary>
    /// Preferable ingredient types for process
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    internal List<Type>? PreferableTypesToProcess { get; set; }
    /// <summary>
    /// Updates preparing dish
    /// </summary>
    internal abstract void Update(DishBase dish);
}
