using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.Orders;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents controller which is responsible for operating with order queue
/// </summary>
public class OrderQueueController : EateryApplicationControllerBase
{
    private readonly Queue<Order> _orders;
    private readonly EateryMenuBase _eateryMenu;
    /// <summary>
    /// Initializes new OrderQueueController instance
    /// </summary>
    public OrderQueueController() { }
    /// <summary>
    /// Initializes new OrderQueueController instance
    /// </summary>
    /// <param name="eateryMenu">Specified eatery menu</param>
    internal OrderQueueController(EateryMenuBase eateryMenu)
    {
        _eateryMenu = eateryMenu;
        _orders = new();
    }
    /// <summary>
    /// Enqueues new order in queue of orders of eatery
    /// </summary>
    /// <param name="getRecipeByNameRequest">Request with order to enqueue</param>
    /// <returns>True - order was added</returns>
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
    /// <summary>
    /// Dequeues latest order
    /// </summary>
    /// <param name="getRecipeForLatestOrderRequest">Empty request</param>
    /// <returns>Respond with latest order in queue of orders</returns>
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
