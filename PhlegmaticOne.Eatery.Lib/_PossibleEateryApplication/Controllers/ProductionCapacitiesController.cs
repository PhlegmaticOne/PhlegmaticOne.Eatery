using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class ProductionCapacitiesController : EateryApplicationControllerBase
{
    private readonly ProductionCapacitiesContainerBase _productionCapacityContainer;

    public ProductionCapacitiesController()
    {
    }

    internal ProductionCapacitiesController(ProductionCapacitiesContainerBase productionCapacityContainer)
    {
        _productionCapacityContainer = productionCapacityContainer;
    }
    [EateryWorker(typeof(Chief), typeof(Cook))]
    public IApplicationRespond<ProductionCapacitiesContainerBase> GetProductionCapacityContainer
           (EmptyApplicationRequest prepareByRecipeRequest)
    {
        if (IsInRole(prepareByRecipeRequest.Worker, nameof(GetProductionCapacityContainer)) == false)
        {
            return GetDefaultAccessDeniedRespond<ProductionCapacitiesContainerBase>(prepareByRecipeRequest.Worker);
        }
        return new DefaultApplicationRespond<ProductionCapacitiesContainerBase>(_productionCapacityContainer,
                                                                              ApplicationRespondType.Success,
                                                                              "Capacities returned");
    }
}
