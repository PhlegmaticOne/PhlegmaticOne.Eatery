namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations;

public interface IMixingProcess
{
    void Mix(IEnumerable<DomainProductToPrepare> productsToPrepare);
}
