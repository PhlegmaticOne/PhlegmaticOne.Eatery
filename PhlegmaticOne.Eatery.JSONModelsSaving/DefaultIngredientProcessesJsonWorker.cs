using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;

public class DefaultIngredientProcessesJsonWorker : CustomJsonWorkerBase<IngredientProcessContainerBase>
{
    public DefaultIngredientProcessesJsonWorker(string filePath) : base(filePath)
    {
    }

    protected override JsonConverter[] HelpingConverters => new JsonConverter[]
    { new DomainProcessesConverter(), new IngredientsConverter() };
}
