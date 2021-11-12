using PhlegmaticOne.Eatery.Lib.Helpers;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations.Adding;

public class AddingProcess : DomainProductProcess, IAddingProcess
{
    private DomainProductToPrepare _productToPrepare;
    public AddingProcess()
    {
    }

    public AddingProcess(TimeSpan timeToFinish, Money price) : base(timeToFinish, price)
    {
    }

    public void Add(DomainProductToPrepare domainProductToPrepare)
    {
        _productToPrepare = domainProductToPrepare;
    }
    internal DomainProductToPrepare GetResult() => _productToPrepare;
}
