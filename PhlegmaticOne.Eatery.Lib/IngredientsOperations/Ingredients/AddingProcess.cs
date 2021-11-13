using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public class AddingProcess : IngredientProcess
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

    public override IEnumerable<Ingredient> ProcessOver(Ingredient ingredient)
    {
        throw new NotImplementedException();
    }

    internal DomainProductToPrepare GetResult() => _productToPrepare;
}
