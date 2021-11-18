using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;
/// <summary>
/// Represents default ingredient processes json serializer/deserializer
/// </summary>
public class DefaultIngredientProcessesJsonWorker : CustomJsonWorkerBase<IngredientProcessContainerBase>
{
    /// <summary>
    /// Initializes new DefaultIngredientProcessesJsonWorker
    /// </summary>
    public DefaultIngredientProcessesJsonWorker() : base() { }
    /// <summary>
    /// Initializes new DefaultIngredientProcessesJsonWorker
    /// </summary>
    /// <param name="filePath">Path to file with data</param>
    public DefaultIngredientProcessesJsonWorker(string filePath) : base(filePath) { }

    protected override JsonConverter[] HelpingConverters => new JsonConverter[]
    { new DomainProcessesConverter(), new IngredientsConverter() };
}
