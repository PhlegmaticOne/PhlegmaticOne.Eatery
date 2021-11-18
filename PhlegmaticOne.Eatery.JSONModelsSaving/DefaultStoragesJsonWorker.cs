using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;
/// <summary>
/// Represents default eatery menu json serializer/deserializer
/// </summary>
public class DefaultStoragesJsonWorker : CustomJsonWorkerBase<StoragesContainerBase>
{
    /// <summary>
    /// Initializes new DefaultStoragesJsonWorker
    /// </summary>
    public DefaultStoragesJsonWorker() : base() { }
    /// <summary>
    /// Initializes new DefaultStoragesJsonWorker
    /// </summary>
    /// <param name="filePath">Path to file with data</param>
    public DefaultStoragesJsonWorker(string filePath) : base(filePath) { }
    protected override JsonConverter[] HelpingConverters =>
        new JsonConverter[] { new IngredientsConverter(), new StoragesConverter() };
}
