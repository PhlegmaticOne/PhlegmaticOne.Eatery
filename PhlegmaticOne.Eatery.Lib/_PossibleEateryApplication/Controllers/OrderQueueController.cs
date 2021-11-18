using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.Orders;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class OrderQueueController : EateryApplicationControllerBase
{
    private readonly Queue<Order> _orders;
    private readonly EateryMenuBase _eateryMenu;
    internal OrderQueueController(EateryMenuBase eateryMenu)
    {
        _eateryMenu = eateryMenu;
        _orders = new();
    }

    public OrderQueueController()
    {
    }

    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<bool> EnqueueNewOrder(IApplicationRequest<Order> getRecipeByNameRequest)
    {
        if (IsInRole(getRecipeByNameRequest.Worker, nameof(EnqueueNewOrder)) == false)
        {
            return GetDefaultAccessDeniedRespond<bool>(getRecipeByNameRequest.Worker);
        }
        _orders.Enqueue(getRecipeByNameRequest.RequestData1);
        return new DefaultApplicationRespond<bool>(true, ApplicationRespondType.Success, "Order is added in queue");
    }
    [EateryWorker(typeof(Chief), typeof(Cook))]
    public IApplicationRespond<Order> DequeueLatestOrder(EmptyApplicationRequest getRecipeForLatestOrderRequest)
    {
        if (IsInRole(getRecipeForLatestOrderRequest.Worker, nameof(DequeueLatestOrder)) == false)
        {
            return GetDefaultAccessDeniedRespond<Order>(getRecipeForLatestOrderRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<Order>();
        if (_orders.Any() == false)
        {
            return respond.Update(null, ApplicationRespondType.InternalError, "No orders yet");
        }
        return respond.Update(_orders.Dequeue(), ApplicationRespondType.Success, "Order is added in queue");
    }
}
