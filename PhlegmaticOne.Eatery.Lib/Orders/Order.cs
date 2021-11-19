using PhlegmaticOne.Eatery.Lib.Dishes;

namespace PhlegmaticOne.Eatery.Lib.Orders;
/// <summary>
/// Represents instance for order
/// </summary>
public class Order
{
    /// <summary>
    /// Initializes new order instance
    /// </summary>
    /// <param name="id">Specified id</param>
    /// <param name="dish">Specified dish</param>
    /// <param name="orderDate">Specified order date</param>
    /// <param name="dishName">Specified dish name</param>
    public Order(int id, DishBase dish, DateTime orderDate, string dishName)
    {
        Id = id;
        Dish = dish;
        OrderDate = orderDate;
        DishName = dishName;
    }
    [Newtonsoft.Json.JsonConstructor]
    public Order(int id, DishBase dish, DateTime orderDate, string dishName, Type dishType)
    {
        Id = id;
        Dish = dish;
        OrderDate = orderDate;
        DishName = dishName;
        DishType = dishType;
    }
    /// <summary>
    /// Id of order
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public int Id { get; }
    /// <summary>
    /// Ordered dish
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public DishBase Dish { get; internal set; }
    /// <summary>
    /// Date of order
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public DateTime OrderDate { get; }
    /// <summary>
    /// Dish name
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    public string DishName { get; }
    /// <summary>
    /// Dish type of dish preparing
    /// </summary>
    /// [Newtonsoft.Json.JsonProperty]
    internal Type DishType { get; set; } = typeof(Dish);
    public override string ToString() => string.Format("Order №{0}. Ordered at: {1}. Dish: {2} - {3}",
                                         Id, OrderDate.ToString("dddd, dd MMMM yyyy HH:mm:ss"), DishType.Name, DishName);
}