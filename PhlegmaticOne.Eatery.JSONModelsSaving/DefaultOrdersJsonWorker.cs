using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.Orders;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;

public class DefaultOrdersJsonWorker : CustomJsonWorkerBase<OrdersContainerBase>
{
    public DefaultOrdersJsonWorker(string filePath) : base(filePath)
    {
    }

    protected override JsonConverter[] HelpingConverters => new JsonConverter[] { };
}
