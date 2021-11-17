namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;

public class DefaultProductionCapacityContainer : ProductionCapacitiesContainerBase
{
    public DefaultProductionCapacityContainer() { }
    internal DefaultProductionCapacityContainer(Dictionary<Type, int> maximalCapacities) : base(maximalCapacities) { }
    [Newtonsoft.Json.JsonConstructor]
    internal DefaultProductionCapacityContainer(Dictionary<Type, int> maximalCapacities,
                                                Dictionary<Type, int> currentCapacities) :
                                                base(maximalCapacities, currentCapacities) { }
    public static IProductionCapacityContainerBuilder GetDefaultProductionCapacityContainerBuilder() =>
        new DefaultProductionCapacityContainerBuilder();
}
