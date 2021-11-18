using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Recipies;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class OrderController : EateryApplicationControllerBase
{
    private static int _orderId = 0;
    private readonly EateryMenuBase _eateryMenu;
    private readonly OrdersContainerBase _ordersContainerBase;

    public OrderController()
    {
    }

    internal OrderController(EateryMenuBase eateryMenu, OrdersContainerBase ordersContainerBase)
    {
        _eateryMenu = eateryMenu;
        _ordersContainerBase = ordersContainerBase;
        _orderId = _ordersContainerBase.Count;
    }

    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<Order> CreateNewOrder(IApplicationRequest<string> createNewOrderRequest)
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
        var order = new Order(++_orderId, null, DateTime.Now, createNewOrderRequest.RequestData1);
        return respond.Update(order, ApplicationRespondType.Success, "Order was maded");
    }

}