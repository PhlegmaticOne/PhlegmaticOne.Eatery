namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;

public abstract class EateryEquipment
{
    protected EateryEquipment(int maxDomainProductsToPrepare, IEnumerable<Type> possibleDishesToPrepare)
    {
        MaxDomainProductsToPrepare = maxDomainProductsToPrepare;
        PossibleDishesToPrepare = possibleDishesToPrepare ?? throw new ArgumentNullException(nameof(possibleDishesToPrepare));
    }

    public int MaxDomainProductsToPrepare { get; internal set; }
    public IEnumerable<Type> PossibleDishesToPrepare { get; internal set; }
}
