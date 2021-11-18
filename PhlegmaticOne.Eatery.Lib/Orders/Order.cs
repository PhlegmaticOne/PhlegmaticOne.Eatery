using PhlegmaticOne.Eatery.Lib.Dishes;

namespace PhlegmaticOne.Eatery.Lib.Orders;

public class Order
{
    [Newtonsoft.Json.JsonConstructor]
    public Order(int id, DishBase dish, DateTime orderDate, string dishName)
    {
        Id = id;
        Dish = dish;
        OrderDate = orderDate;
        DishName = dishName;
    }
    [Newtonsoft.Json.JsonProperty]
    public int Id { get; }
    [Newtonsoft.Json.JsonProperty]
    public DishBase Dish { get; internal set; }
    [Newtonsoft.Json.JsonProperty]
    public DateTime OrderDate { get; }
    [Newtonsoft.Json.JsonProperty]
    public string DishName { get; }
    [Newtonsoft.Json.JsonIgnore]
    internal Type DishType { get; set; }
}
