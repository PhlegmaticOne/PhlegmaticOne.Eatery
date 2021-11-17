using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;

public class DefaultProductionCapacitiesJsonWorker : CustomJsonWorkerBase<ProductionCapacitiesContainerBase>
{
    public DefaultProductionCapacitiesJsonWorker(string filePath) : base(filePath)
    {
    }

    protected override JsonConverter[] HelpingConverters => new JsonConverter[] { };
}
