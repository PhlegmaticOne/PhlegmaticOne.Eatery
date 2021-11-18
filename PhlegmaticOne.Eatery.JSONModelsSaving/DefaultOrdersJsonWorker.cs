using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib.Orders;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;
/// <summary>
/// Represents default orders json serializer/deserializer
/// </summary>
public class DefaultOrdersJsonWorker : CustomJsonWorkerBase<OrdersContainerBase>
{
    /// <summary>
    /// Initializes new DefaultOrdersJsonWorker
    /// </summary>
    public DefaultOrdersJsonWorker() : base() { }
    /// <summary>
    /// Initializes new DefaultOrdersJsonWorker
    /// </summary>
    /// <param name="filePath">Path to file with data</param>
    public DefaultOrdersJsonWorker(string filePath) : base(filePath) { }
    protected override JsonConverter[] HelpingConverters => new JsonConverter[] { new DishesConverter() };
}
