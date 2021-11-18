using PhlegmaticOne.Eatery.Lib.Dishes;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Orders;

public abstract class OrdersContainerBase
{
    [Newtonsoft.Json.JsonProperty]
    internal Dictionary<int, Order> _orders;
    public OrdersContainerBase() => _orders = new();
    [Newtonsoft.Json.JsonConstructor]
    public OrdersContainerBase(Dictionary<int, Order> orders) => _orders = orders;
    internal void Add(Order order) => _orders.Add(order.Id, order);
    internal void UpdateLastWith(DishBase dish)
    {
        _orders[Count].Dish = dish;
    }
    public int Count => _orders.Count;
    public IReadOnlyDictionary<int, Order> GetAllOrders() => new ReadOnlyDictionary<int, Order>(_orders);
}
