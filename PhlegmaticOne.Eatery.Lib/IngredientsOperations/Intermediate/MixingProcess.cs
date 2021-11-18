using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;
/// <summary>
/// Represents mixing process of ingredients
/// </summary>
public class MixingProcess : IntermediateProcess
{
    /// <summary>
    /// Initializes new MixingProcess instance
    /// </summary>
    public MixingProcess() { }
    /// <summary>
    /// Initializes new MixingProcess instance
    /// </summary>
    /// <param name="timeToFinish">Specified duration</param>
    /// <param name="price">Specified price</param>
    [Newtonsoft.Json.JsonConstructor]
    internal MixingProcess(TimeSpan timeToFinish, Money price) : base(timeToFinish, price) { }
    internal override void Update(DishBase dish)
    {
        dish.Weight *= 0.87;
        dish.Price += Price * 1.1;
    }
}
