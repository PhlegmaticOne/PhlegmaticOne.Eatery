using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class ProductionCapacitiesController : EateryApplicationControllerBase
{
    private readonly ProductionCapacityContainerBase _productionCapacityContainer;
    internal ProductionCapacitiesController(ProductionCapacityContainerBase productionCapacityContainer)
    {
        _productionCapacityContainer = productionCapacityContainer;
    }
    [EateryWorker(typeof(Chief), typeof(Cook))]
    public IApplicationRespond<ProductionCapacityContainerBase> GetProductionCapacityContainer
           (EmptyApplicationRequest prepareByRecipeRequest)
    {
        if (IsInRole(prepareByRecipeRequest.Worker, nameof(GetProductionCapacityContainer)) == false)
        {
            return GetDefaultAccessDeniedRespond<ProductionCapacityContainerBase>(prepareByRecipeRequest.Worker);
        }
        return new DefaultApplicationRespond<ProductionCapacityContainerBase>(_productionCapacityContainer,
                                                                              ApplicationRespondType.Success,
                                                                              "Capacities returned");
    }
}
