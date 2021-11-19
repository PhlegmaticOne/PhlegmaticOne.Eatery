using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;
/// <summary>
/// Represents base production capacities container for other containers
/// </summary>
public abstract class ProductionCapacitiesContainerBase
{
    [Newtonsoft.Json.JsonProperty]
    protected readonly Dictionary<Type, int> MaximalCapacities;
    [Newtonsoft.Json.JsonProperty]
    protected readonly Dictionary<Type, int> CurrentCapacities;
    /// <summary>
    /// Initializes new ProductionCapacitiesContainerBase instance
    /// </summary>
    protected ProductionCapacitiesContainerBase() { }
    /// <summary>
    /// Initializes new ProductionCapacitiesContainerBase instance
    /// </summary>
    /// <param name="maximalCapacities">Specified maximal capacities</param>
    /// <param name="currentCapacities">Specified current capacities</param>
    [Newtonsoft.Json.JsonConstructor]
    internal ProductionCapacitiesContainerBase(Dictionary<Type, int> maximalCapacities,
                                               Dictionary<Type, int> currentCapacities)
    {
        MaximalCapacities = maximalCapacities;
        CurrentCapacities = currentCapacities;
    }
    /// <summary>
    /// Initializes new ProductionCapacitiesContainerBase instance
    /// </summary>
    /// <param name="maximalCapacities">Specified maximal capacities</param>
    internal ProductionCapacitiesContainerBase(Dictionary<Type, int> maximalCapacities)
    {
        MaximalCapacities = maximalCapacities;
        CurrentCapacities = new Dictionary<Type, int>(maximalCapacities);
    }
    /// <summary>
    /// Configured capacities count
    /// </summary>
    public int Count => CurrentCapacities.Count;
    /// <summary>
    /// Gets current capacity of domain process specified by its type
    /// </summary>
    internal virtual int GetCurrentCapacityOfProcess(Type processType)
    {
        if (CurrentCapacities.TryGetValue(processType, out var capacity))
        {
            return capacity;
        }
        return int.MinValue;
    }
    /// <summary>
    /// Gets maximal capacity of process specified by its type
    /// </summary>
    internal virtual int GetMaximalCapacityOfProcess(Type processType)
    {
        if (CurrentCapacities.TryGetValue(processType, out int maxCapacity))
        {
            return maxCapacity;
        }
        return int.MinValue;
    }
    /// <summary>
    /// Decreases capacity of process specified by its type on specified value
    /// </summary>
    internal virtual void DecreaseCapacity(Type processType, int decreasingCapacity)
    {
        if (CurrentCapacities.ContainsKey(processType))
        {
            CurrentCapacities[processType] -= decreasingCapacity;
        }
    }
    /// <summary>
    /// Increases capacity of process specified by its type on specified value
    /// </summary>
    internal virtual void IncreaseCapacity(Type processType, int increasingCapacity)
    {
        if (CurrentCapacities.ContainsKey(processType))
        {
            CurrentCapacities[processType] += increasingCapacity;
        }
    }
    /// <summary>
    /// Gets current capacities in container
    /// </summary>
    public virtual IReadOnlyDictionary<Type, int> GetCurrentCapacities() =>
        new ReadOnlyDictionary<Type, int>(CurrentCapacities);
    /// <summary>
    /// Gets maximal capacities in container
    /// </summary>
    public virtual IReadOnlyDictionary<Type, int> GetPossibleCapacities() => 
        new ReadOnlyDictionary<Type, int>(MaximalCapacities);
    public override string ToString() => string.Format("{0}. Count: {1}", GetType().Name, Count);
}
