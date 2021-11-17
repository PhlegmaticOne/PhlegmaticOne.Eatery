using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Recipies;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class OrderController : EateryApplicationControllerBase
{
    private static int _orderId = 0;
    private readonly EateryMenuBase _eateryMenu;
    private readonly OrdersContainerBase _ordersContainerBase;

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
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<IReadOnlyCollection<Order>> GetOrdersInDataRange(IApplicationRequest<DateTime, DateTime> getOrdersInDataRangeRequest)
    {
        if (IsInRole(getOrdersInDataRangeRequest.Worker, nameof(GetOrdersInDataRange)) == false)
        {
            return GetDefaultAccessDeniedRespond<IReadOnlyCollection<Order>>(getOrdersInDataRangeRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<IReadOnlyCollection<Order>>();
        var result = new List<Order>();
        foreach (var order in _ordersContainerBase.GetAllOrders().Values)
        {
            if(order.OrderDate >= getOrdersInDataRangeRequest.RequestData1 && order.OrderDate <= getOrdersInDataRangeRequest.RequestData2)
            {
                result.Add(order);
            }
        }
        return respond.Update(new ReadOnlyCollection<Order>(result), ApplicationRespondType.Success, "Orders were returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<IngredientUsageInfo> GetMostUsedIngredientInWeight(EmptyApplicationRequest getOrdersInDataRangeRequest)
    {
        if (IsInRole(getOrdersInDataRangeRequest.Worker, nameof(GetMostUsedIngredientInWeight)) == false)
        {
            return GetDefaultAccessDeniedRespond<IngredientUsageInfo>(getOrdersInDataRangeRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<IngredientUsageInfo>();
        var dictionaryWithWeightsOfIngredients = GetAllEverUsedIngredientInfo();
        var resultIngredient = dictionaryWithWeightsOfIngredients.MaxBy(x => x.Value);
        return respond.Update(new IngredientUsageInfo(resultIngredient.Key, resultIngredient.Value),
                              ApplicationRespondType.Success, "Maximal usage ingredient was returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<IngredientUsageInfo> GetLeastUsedIngredientInWeight(EmptyApplicationRequest getOrdersInDataRangeRequest)
    {
        if (IsInRole(getOrdersInDataRangeRequest.Worker, nameof(GetLeastUsedIngredientInWeight)) == false)
        {
            return GetDefaultAccessDeniedRespond<IngredientUsageInfo>(getOrdersInDataRangeRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<IngredientUsageInfo>();
        var dictionaryWithWeightsOfIngredients = GetAllEverUsedIngredientInfo();
        var resultIngredient = dictionaryWithWeightsOfIngredients.MinBy(x => x.Value);
        return respond.Update(new IngredientUsageInfo(resultIngredient.Key, resultIngredient.Value),
                              ApplicationRespondType.Success, "Maximal usage ingredient was returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<IngredientUsageInfo> GetMostUsedIngredientInCount(EmptyApplicationRequest getOrdersInDataRangeRequest)
    {
        if (IsInRole(getOrdersInDataRangeRequest.Worker, nameof(GetMostUsedIngredientInCount)) == false)
        {
            return GetDefaultAccessDeniedRespond<IngredientUsageInfo>(getOrdersInDataRangeRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<IngredientUsageInfo>();
        var dictionaryWithWeightsOfIngredients = GetUsageInCountIngredientInfo();
        var resultIngredient = dictionaryWithWeightsOfIngredients.MaxBy(x => x.Value);
        return respond.Update(new IngredientUsageInfo(resultIngredient.Key, resultIngredient.Value),
                              ApplicationRespondType.Success, "Maximal usage ingredient was returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<IngredientUsageInfo> GetLeastUsedIngredientInCount(EmptyApplicationRequest getOrdersInDataRangeRequest)
    {
        if (IsInRole(getOrdersInDataRangeRequest.Worker, nameof(GetLeastUsedIngredientInCount)) == false)
        {
            return GetDefaultAccessDeniedRespond<IngredientUsageInfo>(getOrdersInDataRangeRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<IngredientUsageInfo>();
        var dictionaryWithWeightsOfIngredients = GetUsageInCountIngredientInfo();
        var resultIngredient = dictionaryWithWeightsOfIngredients.MinBy(x => x.Value);
        return respond.Update(new IngredientUsageInfo(resultIngredient.Key, resultIngredient.Value),
                              ApplicationRespondType.Success, "Maximal usage ingredient was returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DishUsageInfo> GetUsageOfDrinks(EmptyApplicationRequest getOrdersInDataRangeRequest)
    {
        if (IsInRole(getOrdersInDataRangeRequest.Worker, nameof(GetLeastUsedIngredientInCount)) == false)
        {
            return GetDefaultAccessDeniedRespond<DishUsageInfo>(getOrdersInDataRangeRequest.Worker);
        }
        return new DefaultApplicationRespond<DishUsageInfo>(GetDishUsageInfo(typeof(Drink)), ApplicationRespondType.Success,
                                                           "Info about usage of drinks was returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DishUsageInfo> GetUsageOfDishes(EmptyApplicationRequest getOrdersInDataRangeRequest)
    {
        if (IsInRole(getOrdersInDataRangeRequest.Worker, nameof(GetLeastUsedIngredientInCount)) == false)
        {
            return GetDefaultAccessDeniedRespond<DishUsageInfo>(getOrdersInDataRangeRequest.Worker);
        }
        return new DefaultApplicationRespond<DishUsageInfo>(GetDishUsageInfo(typeof(Dish)), ApplicationRespondType.Success,
                                                           "Info about usage of drinks was returned");
    }

    private Dictionary<Type, double> GetAllEverUsedIngredientInfo()
    {
        var dictionaryWithWeightsOfIngredients = new Dictionary<Type, double>();
        foreach (var order in _ordersContainerBase.GetAllOrders().Values)
        {
            _eateryMenu.TryGetRecipe(order.Dish.Name, out Recipe recipe);
            foreach (var ingredient in recipe.IngredientsTakesPartInPreparing)
            {
                var ingredientType = ingredient.GetType();
                if (dictionaryWithWeightsOfIngredients.TryGetValue(ingredientType, out double necessaryIngredientWeight))
                {
                    dictionaryWithWeightsOfIngredients[ingredientType] += necessaryIngredientWeight;
                }
                else
                {
                    dictionaryWithWeightsOfIngredients.Add(ingredientType, ingredient.Value);
                }
            }

        }
        return dictionaryWithWeightsOfIngredients;
    }
    private Dictionary<Type, int> GetUsageInCountIngredientInfo()
    {
        var dictionaryWithWeightsOfIngredients = new Dictionary<Type, int>();
        foreach (var order in _ordersContainerBase.GetAllOrders().Values)
        {
            _eateryMenu.TryGetRecipe(order.Dish.Name, out Recipe recipe);
            foreach (var ingredient in recipe.IngredientsTakesPartInPreparing)
            {
                var ingredientType = ingredient.GetType();
                if (dictionaryWithWeightsOfIngredients.TryGetValue(ingredientType, out int alreadyUsed))
                {
                    dictionaryWithWeightsOfIngredients[ingredientType]++;
                }
                else
                {
                    dictionaryWithWeightsOfIngredients.Add(ingredientType, 0);
                }
            }

        }
        return dictionaryWithWeightsOfIngredients;
    }
    private DishUsageInfo GetDishUsageInfo(Type dishType)
    {
        var dictionaryWithWeightsOfIngredients = new Dictionary<Type, double>();
        var totalEarned = 0.0;
        foreach (var order in _ordersContainerBase.GetAllOrders().Values)
        {
            if (order.Dish.GetType() == dishType)
            {
                _eateryMenu.TryGetRecipe(order.Dish.Name, out Recipe recipe);
                foreach (var ingredient in recipe.IngredientsTakesPartInPreparing)
                {
                    var ingredientType = ingredient.GetType();
                    if (dictionaryWithWeightsOfIngredients.TryGetValue(ingredientType, out double necessaryIngredientWeight))
                    {
                        dictionaryWithWeightsOfIngredients[ingredientType] += necessaryIngredientWeight;
                    }
                    else
                    {
                        dictionaryWithWeightsOfIngredients.Add(ingredientType, ingredient.Value);
                    }
                }
                totalEarned += order.Dish.Price.Amount;
            }
        }
        return new DishUsageInfo(new ReadOnlyDictionary<Type, double>(dictionaryWithWeightsOfIngredients),
                                 new Money(totalEarned, "USD"), dishType);
    }
}

public class IngredientUsageInfo
{
    public IngredientUsageInfo(Type ingredientType, double usedWeight)
    {
        IngredientType = ingredientType;
        UsedWeight = usedWeight;
    }

    public Type IngredientType { get; }
    public double UsedWeight { get; }
}

public class DishUsageInfo
{
    public DishUsageInfo(IReadOnlyDictionary<Type, double> allUsedIngredients, Money earnedMoney, Type dishType)
    {
        AllUsedIngredients = allUsedIngredients;
        EarnedMoney = earnedMoney;
        DishType = dishType;
    }
    public IReadOnlyDictionary<Type, double> AllUsedIngredients { get; }
    public Money EarnedMoney { get; }
    public Type DishType { get; }
}