namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;
/// <summary>
/// Represents default production capacity container
/// </summary>
public class DefaultProductionCapacityContainer : ProductionCapacitiesContainerBase
{
    /// <summary>
    /// Initializes new DefaultProductionCapacityContainer instance
    /// </summary>
    public DefaultProductionCapacityContainer() { }
    /// <summary>
    /// Initializes new DefaultProductionCapacityContainer instance
    /// </summary>
    /// <param name="maximalCapacities">Specified maximal capacities</param>
    internal DefaultProductionCapacityContainer(Dictionary<Type, int> maximalCapacities) : base(maximalCapacities) { }
    /// <summary>
    /// Initializes new DefaultProductionCapacityContainer instance
    /// </summary>
    /// <param name="maximalCapacities">Specified maximal capacities</param>
    /// <param name="currentCapacities">Specified current capacities</param>
    [Newtonsoft.Json.JsonConstructor]
    internal DefaultProductionCapacityContainer(Dictionary<Type, int> maximalCapacities,
                                                Dictionary<Type, int> currentCapacities) :
                                                base(maximalCapacities, currentCapacities)
    { }
    /// <summary>
    /// Gets default production capacity container builder
    /// </summary>
    /// <returns></returns>
    public static IProductionCapacityContainerBuilder GetDefaultProductionCapacityContainerBuilder() =>
        new DefaultProductionCapacityContainerBuilder();
}
