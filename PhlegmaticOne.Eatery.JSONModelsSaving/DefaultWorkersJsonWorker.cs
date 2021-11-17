using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;

public class DefaultWorkersJsonWorker : CustomJsonWorkerBase<EateryWorkersContainerBase>
{
    public DefaultWorkersJsonWorker(string filePath) : base(filePath)
    {
    }
    protected override JsonConverter[] HelpingConverters => new JsonConverter[] { new WorkersConverter() };
}
