using System.Collections;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.EateryEquipment;

public class DefaultProductionCapacityContainer : ProductionCapacityContainerBase
{
    internal DefaultProductionCapacityContainer(IDictionary<Type, int> maximalCapacities) : base(maximalCapacities)
    {
    }
    internal DefaultProductionCapacityContainer(IDictionary<Type, int> maximalCapacities,
                                              IDictionary<Type, int> currentCapacities) :
                                              base(maximalCapacities, currentCapacities)
    {
    }
    public static IProductionCapacityContainerBuilder GetDefaultProductionCapacityContainerBuilder() =>
        new DefaultProductionCapacityContainerBuilder();
}
