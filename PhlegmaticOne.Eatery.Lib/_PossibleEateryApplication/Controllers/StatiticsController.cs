using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Recipies;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents controller which is responsible for operating with statistics of orders
/// </summary>
public class StatiticsController : EateryApplicationControllerBase
{
    private readonly EateryMenuBase _eateryMenu;
    private readonly OrdersContainerBase _ordersContainerBase;
    private readonly IngredientProcessContainerBase _ingredientProcessContainer;
    private readonly IntermediateProcessContainerBase _intermediateProcessContainer;
    /// <summary>
    /// Initializes new StatiticsController instance
    /// </summary>
    public StatiticsController() { }
    /// <summary>
    /// Initializes new StatiticsController instance
    /// </summary>
    /// <param name="eateryMenu">Specified eatery menu</param>
    /// <param name="ordersContainerBase">Specified orders container</param>
    /// <param name="ingredientProcessContainer">Specified ingredient process container</param>
    /// <param name="intermediateProcessContainer">Specified intermediate process container</param>
    internal StatiticsController(EateryMenuBase eateryMenu, OrdersContainerBase ordersContainerBase,
                                 IngredientProcessContainerBase ingredientProcessContainer, IntermediateProcessContainerBase intermediateProcessContainer)
    {
        _eateryMenu = eateryMenu;
        _ordersContainerBase = ordersContainerBase;
        _ingredientProcessContainer = ingredientProcessContainer;
        _intermediateProcessContainer = intermediateProcessContainer;
    }
    /// <summary>
    /// Get all order that was made in data range
    /// </summary>
    /// <param name="getOrdersInDataRangeRequest">Requaset with begin data and end data of searching orders</param>
    /// <returns>Read-only collection with finded orders</returns>
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<IReadOnlyCollection<Order>> GetOrdersInDataRange
        (IApplicationRequest<DateTime, DateTime> getOrdersInDataRangeRequest)
    {
        if (IsInRole(getOrdersInDataRangeRequest.Worker, nameof(GetOrdersInDataRange)) == false)
        {
            return GetDefaultAccessDeniedRespond<IReadOnlyCollection<Order>>(getOrdersInDataRangeRequest.Worker);
        }
        var beginDate = getOrdersInDataRangeRequest.RequestData1;
        var endDate = getOrdersInDataRangeRequest.RequestData2;
        if(endDate > beginDate)
        {
            (beginDate, endDate) = (endDate, beginDate);
        }
        var respond = new DefaultApplicationRespond<IReadOnlyCollection<Order>>();
        var result = new List<Order>();
        foreach (var order in _ordersContainerBase.GetAllOrders().Values)
        {
            if (order.OrderDate >= beginDate && order.OrderDate <= endDate)
            {
                result.Add(order);
            }
        }
        return respond.Update(new ReadOnlyCollection<Order>(result), ApplicationRespondType.Success, "Orders were returned");
    }
    /// <summary>
    /// Gets most used ingredient in weight
    /// </summary>
    /// <param name="getMostUsedIngredientInWeightRequest">Empty request</param>
    /// <returns>Respond with ingredient weight usage info of most used ingredient</returns>
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<IngredientWeightUsageInfo> GetMostUsedIngredientInWeight
        (EmptyApplicationRequest getMostUsedIngredientInWeightRequest)
    {
        if (IsInRole(getMostUsedIngredientInWeightRequest.Worker, nameof(GetMostUsedIngredientInWeight)) == false)
        {
            return GetDefaultAccessDeniedRespond<IngredientWeightUsageInfo>(getMostUsedIngredientInWeightRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<IngredientWeightUsageInfo>();
        var dictionaryWithWeightsOfIngredients = GetAllEverUsedIngredientInfo();
        var resultIngredient = dictionaryWithWeightsOfIngredients.MaxBy(x => x.Value);
        return respond.Update(new IngredientWeightUsageInfo(resultIngredient.Key, resultIngredient.Value),
                              ApplicationRespondType.Success, "Maximal usage ingredient was returned");
    }
    /// <summary>
    /// Gets least used ingredient in weight
    /// </summary>
    /// <param name="getLeastUsedIngredientInWeightRequest">Empty request</param>
    /// <returns>Respond with ingredient weight usage info of least used ingredient</returns>
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<IngredientWeightUsageInfo> GetLeastUsedIngredientInWeight
        (EmptyApplicationRequest getLeastUsedIngredientInWeightRequest)
    {
        if (IsInRole(getLeastUsedIngredientInWeightRequest.Worker, nameof(GetLeastUsedIngredientInWeight)) == false)
        {
            return GetDefaultAccessDeniedRespond<IngredientWeightUsageInfo>(getLeastUsedIngredientInWeightRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<IngredientWeightUsageInfo>();
        var dictionaryWithWeightsOfIngredients = GetAllEverUsedIngredientInfo();
        var resultIngredient = dictionaryWithWeightsOfIngredients.MinBy(x => x.Value);
        return respond.Update(new IngredientWeightUsageInfo(resultIngredient.Key, resultIngredient.Value),
                              ApplicationRespondType.Success, "Maximal usage ingredient was returned");
    }
    /// <summary>
    /// Gets most used ingredient in count
    /// </summary>
    /// <param name="getMostUsedIngredientInCountRequest">Empty request</param>
    /// <returns>Respond with ingredient count usage info of most used ingredient</returns>
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<IngredientTimesUsageInfo> GetMostUsedIngredientInCount
        (EmptyApplicationRequest getMostUsedIngredientInCountRequest)
    {
        if (IsInRole(getMostUsedIngredientInCountRequest.Worker, nameof(GetMostUsedIngredientInCount)) == false)
        {
            return GetDefaultAccessDeniedRespond<IngredientTimesUsageInfo>(getMostUsedIngredientInCountRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<IngredientTimesUsageInfo>();
        var dictionaryWithWeightsOfIngredients = GetUsageInCountIngredientInfo();
        var resultIngredient = dictionaryWithWeightsOfIngredients.MaxBy(x => x.Value);
        return respond.Update(new IngredientTimesUsageInfo(resultIngredient.Key, resultIngredient.Value),
                              ApplicationRespondType.Success, "Maximal usage ingredient was returned");
    }
    /// <summary>
    /// Gets least used ingredient in count
    /// </summary>
    /// <param name="getLeastUsedIngredientInCountRequest">Empty request</param>
    /// <returns>Respond with ingredient count usage info of least used ingredient</returns>
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<IngredientTimesUsageInfo> GetLeastUsedIngredientInCount
        (EmptyApplicationRequest getLeastUsedIngredientInCountRequest)
    {
        if (IsInRole(getLeastUsedIngredientInCountRequest.Worker, nameof(GetLeastUsedIngredientInCount)) == false)
        {
            return GetDefaultAccessDeniedRespond<IngredientTimesUsageInfo>(getLeastUsedIngredientInCountRequest.Worker);
        }
        var respond = new DefaultApplicationRespond<IngredientTimesUsageInfo>();
        var dictionaryWithWeightsOfIngredients = GetUsageInCountIngredientInfo();
        var resultIngredient = dictionaryWithWeightsOfIngredients.MinBy(x => x.Value);
        return respond.Update(new IngredientTimesUsageInfo(resultIngredient.Key, resultIngredient.Value),
                              ApplicationRespondType.Success, "Maximal usage ingredient was returned");
    }
    /// <summary>
    /// Gets total info of dishes prepares
    /// </summary>
    /// <param name="getUsageOfDrinksRequest">Empty request</param>
    /// <returns>Respond with dish usage info of dish type</returns>
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DishUsageInfo> GetUsageOfDrinks(EmptyApplicationRequest getUsageOfDrinksRequest)
    {
        if (IsInRole(getUsageOfDrinksRequest.Worker, nameof(GetLeastUsedIngredientInCount)) == false)
        {
            return GetDefaultAccessDeniedRespond<DishUsageInfo>(getUsageOfDrinksRequest.Worker);
        }
        return new DefaultApplicationRespond<DishUsageInfo>(GetDishUsageInfo(typeof(Drink)), ApplicationRespondType.Success,
                                                           "Info about usage of drinks was returned");
    }
    /// <summary>
    /// Gets total info of drinks prepares
    /// </summary>
    /// <param name="getUsageOfDishesRequest">Empty request</param>
    /// <returns>Respond with dish usage info of drink type</returns>
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DishUsageInfo> GetUsageOfDishes(EmptyApplicationRequest getUsageOfDishesRequest)
    {
        if (IsInRole(getUsageOfDishesRequest.Worker, nameof(GetLeastUsedIngredientInCount)) == false)
        {
            return GetDefaultAccessDeniedRespond<DishUsageInfo>(getUsageOfDishesRequest.Worker);
        }
        return new DefaultApplicationRespond<DishUsageInfo>(GetDishUsageInfo(typeof(Dish)), ApplicationRespondType.Success,
                                                           "Info about usage of drinks was returned");
    }
    /// <summary>
    /// Gets most expensive process
    /// </summary>
    /// <param name="getOrdersInDataRangeRequest">Empty request</param>
    /// <returns>Respond with most expensive process</returns>
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DomainProductProcess> GetMostExpensiveProcessOverDish
        (EmptyApplicationRequest getMostExpensiveProcessRequest)
    {
        if (IsInRole(getMostExpensiveProcessRequest.Worker, nameof(GetMostExpensiveProcessOverDish)) == false)
        {
            return GetDefaultAccessDeniedRespond<DomainProductProcess>(getMostExpensiveProcessRequest.Worker);
        }
        var maxPriceIngredientProcess = _ingredientProcessContainer.MaxPriceProcess();
        var maxPriceIntemediateProcess = _intermediateProcessContainer.MaxPriceProcess();
        return new DefaultApplicationRespond<DomainProductProcess>
            (maxPriceIngredientProcess.Price.Amount > maxPriceIntemediateProcess.Price.Amount ? maxPriceIngredientProcess : maxPriceIntemediateProcess,
            ApplicationRespondType.Success, "Most expensive process returned");
    }
    /// <summary>
    /// Gets least expensive process
    /// </summary>
    /// <param name="getOrdersInDataRangeRequest">Empty request</param>
    /// <returns>Respond with least expensive process</returns>
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DomainProductProcess> GetLeastExpensiveProcessOverDish
        (EmptyApplicationRequest getLeastExpensiveProcessRequest)
    {
        if (IsInRole(getLeastExpensiveProcessRequest.Worker, nameof(GetMostExpensiveProcessOverDish)) == false)
        {
            return GetDefaultAccessDeniedRespond<DomainProductProcess>(getLeastExpensiveProcessRequest.Worker);
        }
        var maxPriceIngredientProcess = _ingredientProcessContainer.MinPriceProcess();
        var maxPriceIntemediateProcess = _intermediateProcessContainer.MinPriceProcess();
        return new DefaultApplicationRespond<DomainProductProcess>
            (maxPriceIngredientProcess.Price.Amount < maxPriceIntemediateProcess.Price.Amount ? maxPriceIngredientProcess : maxPriceIntemediateProcess,
            ApplicationRespondType.Success, "Least expensive process returned");
    }
    /// <summary>
    /// Gets longest process
    /// </summary>
    /// <param name="getOrdersInDataRangeRequest">Empty request</param>
    /// <returns>Respond with longest process</returns>
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DomainProductProcess> GetLongestProcessOverDish
        (EmptyApplicationRequest getLongestProcessRequest)
    {
        if (IsInRole(getLongestProcessRequest.Worker, nameof(GetMostExpensiveProcessOverDish)) == false)
        {
            return GetDefaultAccessDeniedRespond<DomainProductProcess>(getLongestProcessRequest.Worker);
        }
        var maxPriceIngredientProcess = _ingredientProcessContainer.MaxTimeProcess();
        var maxPriceIntemediateProcess = _intermediateProcessContainer.MaxTimeProcess();
        return new DefaultApplicationRespond<DomainProductProcess>
            (maxPriceIngredientProcess.Price.Amount > maxPriceIntemediateProcess.Price.Amount ? maxPriceIngredientProcess : maxPriceIntemediateProcess,
            ApplicationRespondType.Success, "Longest process returned");
    }
    /// <summary>
    /// Gets shortest process
    /// </summary>
    /// <param name="getOrdersInDataRangeRequest">Empty request</param>
    /// <returns>Respond with shortest process</returns>
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DomainProductProcess> GetShortestProcessOverDish
        (EmptyApplicationRequest getSortestProcessRequest)
    {
        if (IsInRole(getSortestProcessRequest.Worker, nameof(GetMostExpensiveProcessOverDish)) == false)
        {
            return GetDefaultAccessDeniedRespond<DomainProductProcess>(getSortestProcessRequest.Worker);
        }
        var maxPriceIngredientProcess = _ingredientProcessContainer.MinTimeProcess();
        var maxPriceIntemediateProcess = _intermediateProcessContainer.MinTimeProcess();
        return new DefaultApplicationRespond<DomainProductProcess>
            (maxPriceIngredientProcess.Price.Amount < maxPriceIntemediateProcess.Price.Amount ? maxPriceIngredientProcess : maxPriceIntemediateProcess,
            ApplicationRespondType.Success, "Shortest process returned");
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
