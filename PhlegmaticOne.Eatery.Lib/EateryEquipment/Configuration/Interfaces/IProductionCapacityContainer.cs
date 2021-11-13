namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;

public interface IProductionCapacityContainer
{
    int GetMaximalProductsToProcess(Type processType);
    int GetCurrentCapacityOfProcess(Type processType);
    void DecreaseCapacity(Type processType);
    void IncreaseCapacity(Type processType);
}
