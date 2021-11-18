using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;
/// <summary>
/// Represents default production capacity menu json serializer/deserializer
/// </summary>
public class DefaultProductionCapacitiesJsonWorker : CustomJsonWorkerBase<ProductionCapacitiesContainerBase>
{
    /// <summary>
    /// Initializes new DefaultProductionCapacitiesJsonWorker
    /// </summary>
    public DefaultProductionCapacitiesJsonWorker() : base() { }
    /// <summary>
    /// Initializes new DefaultProductionCapacitiesJsonWorker
    /// </summary>
    /// <param name="filePath">Path to file with data</param>
    public DefaultProductionCapacitiesJsonWorker(string filePath) : base(filePath) { }
    protected override JsonConverter[] HelpingConverters => new JsonConverter[] { };
}