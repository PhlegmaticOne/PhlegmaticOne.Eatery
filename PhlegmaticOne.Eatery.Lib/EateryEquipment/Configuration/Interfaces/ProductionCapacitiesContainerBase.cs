using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;

public abstract class ProductionCapacitiesContainerBase
{
    [Newtonsoft.Json.JsonProperty]
    protected readonly Dictionary<Type, int> MaximalCapacities;
    [Newtonsoft.Json.JsonProperty]
    protected readonly Dictionary<Type, int> CurrentCapacities;
    public int Count => CurrentCapacities.Count;
    [Newtonsoft.Json.JsonConstructor]
    internal ProductionCapacitiesContainerBase(Dictionary<Type, int> maximalCapacities,
                                               Dictionary<Type, int> currentCapacities)
    {
        MaximalCapacities = maximalCapacities;
        CurrentCapacities = currentCapacities;
    }
    internal ProductionCapacitiesContainerBase(Dictionary<Type, int> maximalCapacities)
    {
        MaximalCapacities = maximalCapacities;
        CurrentCapacities = new Dictionary<Type, int>(maximalCapacities);
    }
    internal ProductionCapacitiesContainerBase()
    {

    }
    internal virtual int GetCurrentCapacityOfProcess(Type processType)
    {
        if (CurrentCapacities.TryGetValue(processType, out var capacity))
        {
            return capacity;
        }
        return int.MinValue;
    }
    internal virtual int GetMaximalCapacityOfProcess(Type processType)
    {
        if (CurrentCapacities.TryGetValue(processType, out int maxCapacity))
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
