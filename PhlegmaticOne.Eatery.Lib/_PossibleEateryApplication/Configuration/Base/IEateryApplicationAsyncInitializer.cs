using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents contract for eatery application async initializers
/// </summary>
public interface IEateryApplicationAsyncInitializer
{
    /// <summary>
    /// Loads storages container from json file asynchronously
    /// </summary>
    /// <returns>Base production capacities container deserialized from file</returns>
    Task<StoragesContainerBase?> LoadStoragesAsync();
    /// <summary>
    /// Loads ingredient processes container from json file asynchronously
    /// </summary>
    /// <returns>Base ingredient processes container deserialized from file</returns>
    Task<IngredientProcessContainerBase?> LoadIngredientProcessesAsync();
    /// <summary>
    /// Loads intermediate processes container from json file asynchronously
    /// </summary>
    /// <returns>Base intermediate processes container deserialized from file</returns>
    Task<IntermediateProcessContainerBase?> LoadIntermediateProcessesAsync();
    /// <summary>
    /// Loads production capacities container from json file asynchronously
    /// </summary>
    /// <returns>Base production capacities container deserialized from file</returns>
    Task<ProductionCapacitiesContainerBase?> LoadProductionCapacitiesAsync();
    /// <summary>
    /// Loads eatery menu from json file asynchronously
    /// </summary>
    /// <returns>Base eatery menu deserialized from file</returns>
    Task<EateryMenuBase?> LoadEateryMenuAsync();
    /// <summary>
    /// Loads eatery workers container from json file asynchronously
    /// </summary>
    /// <returns>Base eatery workers container deserialized from file</returns>
    Task<EateryWorkersContainerBase?> LoadWorkersAsync();
    /// <summary>
    /// Loads orders container from json file asynchronously
    /// </summary>
    /// <returns>Base orders container deserialized from file</returns>
    Task<OrdersContainerBase?> LoadOrdersAsync();
}
