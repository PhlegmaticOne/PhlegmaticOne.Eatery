using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.Dishes;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;

public class DefaultEateryMenuJsonWorker : CustomJsonWorkerBase<EateryMenuBase>
{
    public DefaultEateryMenuJsonWorker(string filePath) : base(filePath)
    {
    }
    protected override JsonConverter[] HelpingConverters => new JsonConverter[] { new DomainProcessesConverter(),
                                                                                  new IngredientsConverter() };
}
