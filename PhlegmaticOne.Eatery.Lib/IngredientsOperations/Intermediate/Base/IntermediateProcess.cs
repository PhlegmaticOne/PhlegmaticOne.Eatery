using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public abstract class IntermediateProcess : DomainProductProcess
{
    protected IntermediateProcess() { }
    [Newtonsoft.Json.JsonConstructor]
    protected IntermediateProcess(TimeSpan timeToFinish, Money price) : base(timeToFinish, price) { }
    [Newtonsoft.Json.JsonProperty]
    internal List<Type>? PreferableTypesToProcess { get; set; }
    internal abstract void Update(DishBase dish);
}
