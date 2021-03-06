using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents controller which is responsible for operating with production capacities
/// </summary>
public class ProductionCapacitiesController : EateryApplicationControllerBase
{
    private readonly ProductionCapacitiesContainerBase _productionCapacityContainer;
    /// <summary>
    /// Initializes new ProductionCapacitiesController instance
    /// </summary>
    public ProductionCapacitiesController() { }
    /// <summary>
    /// Initializes new ProductionCapacitiesController instance
    /// </summary>
    /// <param name="productionCapacityContainer">Specified production capacity container</param>
    internal ProductionCapacitiesController(ProductionCapacitiesContainerBase productionCapacityContainer)
    {
        _productionCapacityContainer = productionCapacityContainer;
    }
    /// <summary>
    /// Gets production capacities container
    /// </summary>
    /// <param name="prepareByRecipeRequest">Empty request</param>
    /// <returns>Respond with production capacities container</returns>
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
