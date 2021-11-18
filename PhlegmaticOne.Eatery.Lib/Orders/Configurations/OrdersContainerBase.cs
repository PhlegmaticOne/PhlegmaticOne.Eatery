using PhlegmaticOne.Eatery.Lib.Dishes;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Orders;
/// <summary>
/// Represents base order container for other order containers 
/// </summary>
public abstract class OrdersContainerBase
{
    /// <summary>
    /// Keepeing orders
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    internal Dictionary<int, Order> _orders;
    /// <summary>
    /// Initializes new OrdersContainerBase instance
    /// </summary>
    public OrdersContainerBase() => _orders = new();
    /// <summary>
    /// Initializes new OrdersContainerBase instance
    /// </summary>
    /// <param name="orders">Specified collection of orders</param>
    [Newtonsoft.Json.JsonConstructor]
    internal OrdersContainerBase(Dictionary<int, Order> orders) => _orders = orders;
    /// <summary>
    /// Amount of orders in container
    /// </summary>
    public int Count => _orders.Count;
    /// <summary>
    /// Adds new order in container
    /// </summary>
    internal void Add(Order order) => _orders.Add(order.Id, order);
    /// <summary>
    /// Updates last order with specified dish
    /// </summary>
    internal void UpdateLastWith(DishBase dish)
    {
        _orders[Count].Dish = dish;
    }
    /// <summary>
    /// Gets all orders from container
    /// </summary>
    public IReadOnlyDictionary<int, Order> GetAllOrders() => new ReadOnlyDictionary<int, Order>(_orders);
    public override bool Equals(object? obj) => obj is OrdersContainerBase ordersContainerBase &&
                                                ordersContainerBase._orders.Except(_orders).Any() == false;
    public override int GetHashCode() => _orders.GetHashCode();
    public override string ToString() => string.Format("{0}. Count: {1}", GetType().GetType().Name, Count);
}
