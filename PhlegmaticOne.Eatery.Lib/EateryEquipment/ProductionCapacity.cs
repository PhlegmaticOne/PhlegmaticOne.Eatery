using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;

public class ProductionCapacity
{
    public ProductionCapacity(DomainProductProcess domainProductProcess, int capacity)
    {
        DomainProductProcess = domainProductProcess;
        Capacity = capacity;
    }

    public DomainProductProcess DomainProductProcess { get; set; }
    public int Capacity { get; set; }
}
