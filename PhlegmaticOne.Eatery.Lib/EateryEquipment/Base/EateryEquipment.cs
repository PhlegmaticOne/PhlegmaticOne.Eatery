using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;

public abstract class EateryEquipment
{
    protected EateryEquipment(int maxDomainProductsToPrepare, IDictionary<Type, int> possibleDishesToPrepare)
    {
        MaxDomainProductsToPrepare = maxDomainProductsToPrepare;
        PossibleDishesToPrepare = possibleDishesToPrepare ?? throw new ArgumentNullException(nameof(possibleDishesToPrepare));
    }

    public int MaxDomainProductsToPrepare { get; internal set; }
    internal IDictionary<Type, int> PossibleDishesToPrepare { get; set; }
    public IReadOnlyDictionary<Type, int> GetPossibleDishesToPrepare() => new ReadOnlyDictionary<Type, int>(PossibleDishesToPrepare);
}
