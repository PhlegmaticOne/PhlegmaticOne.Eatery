namespace PhlegmaticOne.Eatery.Lib.Orders;
/// <summary>
/// Represents default orders container
/// </summary>
public class DefaultOrderContainer : OrdersContainerBase
{
    /// <summary>
    /// Initializes new DefaultOrderContainer
    /// </summary>
    public DefaultOrderContainer() { }
    /// <summary>
    /// Initializes new DefaultOrderContainer
    /// </summary>
    /// <param name="orders">Sprcified orders collection</param>
    [Newtonsoft.Json.JsonConstructor]
    public DefaultOrderContainer(Dictionary<int, Order> orders) : base(orders) { }
}
