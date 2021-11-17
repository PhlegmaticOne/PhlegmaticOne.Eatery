using PhlegmaticOne.Eatery.Lib.Dishes;

namespace PhlegmaticOne.Eatery.Lib.Orders;

public class Order
{
    public Order(int id, Dish dish, DateTime orderDate)
    {
        Id = id;
        Dish = dish;
        OrderDate = orderDate;
    }

    public int Id { get; }
    public Dish Dish { get; }
    public DateTime OrderDate { get; }
}
