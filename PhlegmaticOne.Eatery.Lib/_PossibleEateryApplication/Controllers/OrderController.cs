using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Recipies;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents controller which is responsible for operating with orders
/// </summary>
public class OrderController : EateryApplicationControllerBase
{
    private static int _orderId = 0;
    private readonly EateryMenuBase _eateryMenu;
    private readonly OrdersContainerBase _ordersContainerBase;
    /// <summary>
    /// Initializes new OrderController instance
    /// </summary>
    public OrderController() { }
    /// <summary>
    /// Initializes new OrderController instance
    /// </summary>
    /// <param name="eateryMenu">Specified eatery menu</param>
    /// <param name="ordersContainerBase">Specified orders container</param>
    internal OrderController(EateryMenuBase eateryMenu, OrdersContainerBase ordersContainerBase)
    {
        _eateryMenu = eateryMenu;
        _ordersContainerBase = ordersContainerBase;
        _orderId = _ordersContainerBase.Count;
    }
    /// <summary>
    /// Creates new order
    /// </summary>
    /// <param name="createNewOrderRequest">Request with dish name and dish type to prepare</param>
    /// <returns>Respond with maded order</returns>
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<Order> CreateNewOrder(IApplicationRequest<string, Type> createNewOrderRequest)
    {
        if (IsInRole(createNewOrderRequest.Worker, nameof(CreateNewOrder)) == false)
        {
            return GetDefaultAccessDeniedRespond<Order>(createNewOrderRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<Order>();
        if (_eateryMenu.TryGetRecipe(createNewOrderRequest.RequestData1, out Recipe recipe) == false)
        {
            return respond.Update(null, ApplicationRespondType.InternalError,
                   $"Recipe for {createNewOrderRequest.RequestData1} dish wasn't finded");
        }
        var order = new Order(++_orderId, null, DateTime.Now, createNewOrderRequest.RequestData1)
        {
            DishType = createNewOrderRequest.RequestData2
        };
        return respond.Update(order, ApplicationRespondType.Success, "Order was maded");
    }
}