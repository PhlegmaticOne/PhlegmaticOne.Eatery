using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;

public abstract class ProductionCapacityContainerBase
{
    protected readonly IDictionary<Type, int> MaximalCapacities;
    protected readonly IDictionary<Type, int> CurrentCapacities;
    public int Count => CurrentCapacities.Count;
    internal ProductionCapacityContainerBase(IDictionary<Type, int> maximalCapacities,
                                             IDictionary<Type, int> currentCapacities)
    {
        MaximalCapacities = maximalCapacities;
        CurrentCapacities = currentCapacities;
    }
    internal ProductionCapacityContainerBase(IDictionary<Type, int> maximalCapacities)
    {
        MaximalCapacities = maximalCapacities;
        CurrentCapacities= new Dictionary<Type, int>(maximalCapacities);
    }
    internal virtual int GetCurrentCapacityOfProcess(Type processType)
    {
        if(CurrentCapacities.TryGetValue(processType, out var capacity))
        {
            return capacity;
        }
        return int.MinValue;
    }
    internal virtual int GetMaximalCapacityOfProcess(Type processType)
    {
        if(CurrentCapacities.TryGetValue(processType, out int maxCapacity))
        {
            return maxCapacity;
        }
        return int.MinValue;
    }
    internal virtual void DecreaseCapacity(Type processType, int decreasingCapacity)
    {
        if (CurrentCapacities.ContainsKey(processType))
        {
            CurrentCapacities[processType] -= decreasingCapacity;
        }
    }
    internal virtual void IncreaseCapacity(Type processType, int increasingCapacity)
    {
        if (CurrentCapacities.ContainsKey(processType))
        {
            CurrentCapacities[processType] += increasingCapacity;
        }
    }
    public virtual IReadOnlyDictionary<Type, int> GetCurrentCapacities() => new ReadOnlyDictionary<Type, int>(CurrentCapacities);
    public virtual IReadOnlyDictionary<Type, int> GetPossibleCapacities() => new ReadOnlyDictionary<Type, int>(MaximalCapacities);
}
