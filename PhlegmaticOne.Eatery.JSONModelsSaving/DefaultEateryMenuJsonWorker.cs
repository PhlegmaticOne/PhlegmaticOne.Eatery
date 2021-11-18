using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.Dishes;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;
/// <summary>
/// Represents default eatery menu json serializer/deserializer
/// </summary>
public class DefaultEateryMenuJsonWorker : CustomJsonWorkerBase<EateryMenuBase>
{
    /// <summary>
    /// Initializes new DefaultEateryMenuJsonWorker instance
    /// </summary>
    public DefaultEateryMenuJsonWorker() : base() { }
    /// <summary>
    /// Initializes new DefaultEateryMenuJsonWorker
    /// </summary>
    /// <param name="filePath">Path to file with data</param>
    public DefaultEateryMenuJsonWorker(string filePath) : base(filePath) { }
    protected override JsonConverter[] HelpingConverters => new JsonConverter[] { new DomainProcessesConverter(),
                                                                                  new IngredientsConverter() };
}
