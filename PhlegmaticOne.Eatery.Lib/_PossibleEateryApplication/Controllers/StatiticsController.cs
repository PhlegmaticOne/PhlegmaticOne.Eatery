using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Recipies;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class StatiticsController : EateryApplicationControllerBase
{
    private readonly EateryMenuBase _eateryMenu;
    private readonly OrdersContainerBase _ordersContainerBase;
    private readonly IngredientProcessContainerBase _ingredientProcessContainer;
    private readonly IntermediateProcessContainerBase _intermediateProcessContainer;

    internal StatiticsController(EateryMenuBase eateryMenu, OrdersContainerBase ordersContainerBase,
                                 IngredientProcessContainerBase ingredientProcessContainer, IntermediateProcessContainerBase intermediateProcessContainer)
    {
        _eateryMenu = eateryMenu;
        _ordersContainerBase = ordersContainerBase;
        _ingredientProcessContainer = ingredientProcessContainer;
        _intermediateProcessContainer = intermediateProcessContainer;
    }

    public StatiticsController()
    {
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
            if (order.OrderDate >= getOrdersInDataRangeRequest.RequestData1 && order.OrderDate <= getOrdersInDataRangeRequest.RequestData2)
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

    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DomainProductProcess> GetMostExpensiveProcessOverDish(EmptyApplicationRequest getRecipeByNameRequest)
    {
        if (IsInRole(getRecipeByNameRequest.Worker, nameof(GetMostExpensiveProcessOverDish)) == false)
        {
            return GetDefaultAccessDeniedRespond<DomainProductProcess>(getRecipeByNameRequest.Worker);
        }
        var maxPriceIngredientProcess = _ingredientProcessContainer.MaxPriceProcess();
        var maxPriceIntemediateProcess = _intermediateProcessContainer.MaxPriceProcess();
        return new DefaultApplicationRespond<DomainProductProcess>
            (maxPriceIngredientProcess.Price.Amount > maxPriceIntemediateProcess.Price.Amount ? maxPriceIngredientProcess : maxPriceIntemediateProcess,
            ApplicationRespondType.Success, "Most expensive process returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DomainProductProcess> GetLeastExpensiveProcessOverDish(EmptyApplicationRequest getRecipeByNameRequest)
    {
        if (IsInRole(getRecipeByNameRequest.Worker, nameof(GetMostExpensiveProcessOverDish)) == false)
        {
            return GetDefaultAccessDeniedRespond<DomainProductProcess>(getRecipeByNameRequest.Worker);
        }
        var maxPriceIngredientProcess = _ingredientProcessContainer.MinPriceProcess();
        var maxPriceIntemediateProcess = _intermediateProcessContainer.MinPriceProcess();
        return new DefaultApplicationRespond<DomainProductProcess>
            (maxPriceIngredientProcess.Price.Amount < maxPriceIntemediateProcess.Price.Amount ? maxPriceIngredientProcess : maxPriceIntemediateProcess,
            ApplicationRespondType.Success, "Least expensive process returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DomainProductProcess> GetLongestProcessOverDish(EmptyApplicationRequest getRecipeByNameRequest)
    {
        if (IsInRole(getRecipeByNameRequest.Worker, nameof(GetMostExpensiveProcessOverDish)) == false)
        {
            return GetDefaultAccessDeniedRespond<DomainProductProcess>(getRecipeByNameRequest.Worker);
        }
        var maxPriceIngredientProcess = _ingredientProcessContainer.MaxTimeProcess();
        var maxPriceIntemediateProcess = _intermediateProcessContainer.MaxTimeProcess();
        return new DefaultApplicationRespond<DomainProductProcess>
            (maxPriceIngredientProcess.Price.Amount > maxPriceIntemediateProcess.Price.Amount ? maxPriceIngredientProcess : maxPriceIntemediateProcess,
            ApplicationRespondType.Success, "Longest process returned");
    }
    [EateryWorker(typeof(Manager))]
    public IApplicationRespond<DomainProductProcess> GetShortestProcessOverDish(EmptyApplicationRequest getRecipeByNameRequest)
    {
        if (IsInRole(getRecipeByNameRequest.Worker, nameof(GetMostExpensiveProcessOverDish)) == false)
        {
            return GetDefaultAccessDeniedRespond<DomainProductProcess>(getRecipeByNameRequest.Worker);
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
