using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;
/// <summary>
/// Represents default eatery workers json serializer/deserializer
/// </summary>
public class DefaultWorkersJsonWorker : CustomJsonWorkerBase<EateryWorkersContainerBase>
{
    /// <summary>
    /// Initializes new DefaultWorkersJsonWorker instance
    /// </summary>
    public DefaultWorkersJsonWorker() : base() { }
    /// <summary>
    /// Initializes new DefaultWorkersJsonWorker
    /// </summary>
    /// <param name="filePath">Path to file with data</param>
    public DefaultWorkersJsonWorker(string filePath) : base(filePath) { }
    protected override JsonConverter[] HelpingConverters => new JsonConverter[] { new WorkersConverter() };
}