namespace PhlegmaticOne.Eatery.Lib.Orders;

public class DefaultOrderContainer : OrdersContainerBase
{
    public DefaultOrderContainer()
    {
    }
    [Newtonsoft.Json.JsonConstructor]
    public DefaultOrderContainer(Dictionary<int, Order> orders) : base(orders)
    {
    }
}
