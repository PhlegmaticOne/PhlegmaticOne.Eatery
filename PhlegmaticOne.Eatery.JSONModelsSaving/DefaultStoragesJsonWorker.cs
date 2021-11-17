using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.JSONModelsSaving
{
    public class DefaultStoragesJsonWorker : CustomJsonWorkerBase<StoragesContainerBase>
    {
        public DefaultStoragesJsonWorker(string filePath) : base(filePath)
        {
        }
        protected override JsonConverter[] HelpingConverters =>
            new JsonConverter[] { new IngredientsConverter(), new StoragesConverter() };
    }
}
