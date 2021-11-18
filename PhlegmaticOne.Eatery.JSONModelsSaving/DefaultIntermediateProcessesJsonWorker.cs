using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;
/// <summary>
/// Represents default intermediate processes json serializer/deserializer
/// </summary>
public class DefaultIntermediateProcessesJsonWorker : CustomJsonWorkerBase<IntermediateProcessContainerBase>
{
    /// <summary>
    /// Initializes new DefaultIntermediateProcessesJsonWorker
    /// </summary>
    public DefaultIntermediateProcessesJsonWorker() : base() { }
    /// <summary>
    /// Initializes new DefaultIntermediateProcessesJsonWorker
    /// </summary>
    /// <param name="filePath">Path to file with data</param>
    public DefaultIntermediateProcessesJsonWorker(string filePath) : base(filePath) { }

    protected override JsonConverter[] HelpingConverters => new JsonConverter[]
                                        { new DomainProcessesConverter(), new IngredientsConverter() };
}