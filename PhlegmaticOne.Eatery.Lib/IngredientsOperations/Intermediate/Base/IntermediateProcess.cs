using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public abstract class IntermediateProcess : DomainProductProcess
{
    protected IntermediateProcess()
    {
    }

    protected IntermediateProcess(TimeSpan timeToFinish, Money price) : base(timeToFinish, price)
    {
    }
    internal IList<Type> PreferableTypesToProcess { get; set; }
    public abstract void ProcessOver(IEnumerable<DomainProductToPrepare> productToPrepare);

}
