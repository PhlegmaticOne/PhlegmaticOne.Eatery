using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Recipies;
using PhlegmaticOne.Eatery.Lib.Storages;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class PreparingDishController : EateryApplicationControllerBase
{
    private readonly PriorityQueue<Recipe, int> _recipesInProcessOfPreparing;
    private readonly StoragesContainerBase _storagesContainer;
    private readonly ProductionCapacitiesContainerBase _capacitiesContainer;
    private readonly OrdersContainerBase _ordersContainerBase;

    internal PreparingDishController(StoragesContainerBase storagesContainer,
                                     ProductionCapacitiesContainerBase capacitiesContainer,
                                     OrdersContainerBase ordersContainerBase)
    {
        _recipesInProcessOfPreparing = new();
        _storagesContainer = storagesContainer;
        _capacitiesContainer = capacitiesContainer;
        _ordersContainerBase = ordersContainerBase;
    }
    [EateryWorker(typeof(Chief), typeof(Cook))]
    public IApplicationRespond<TryToPrepareDishRespondType, string> BeginPreparing(IApplicationRequest<Order, Recipe> beginPrepareDishRequest)
    {
        if (IsInRole(beginPrepareDishRequest.Worker, nameof(BeginPreparing)) == false)
        {
            return GetDefaultAccessDeniedRespond<TryToPrepareDishRespondType, string>(beginPrepareDishRequest.Worker);
        }
        var recipe = beginPrepareDishRequest.RequestData2;
        var neededIngredients = recipe.IngredientsTakesPartInPreparing;
        var neededCapacities = recipe.ProcessesQueueToPrepareDish.GroupBy(k => k.GetType());
        (var preIngredientCheckingType, var errorMessage1) = CheckForIngredients(neededIngredients);
        if (preIngredientCheckingType != TryToPrepareDishRespondType.PreparingBegan)
        {
            return new DefaultApplicationRespond<TryToPrepareDishRespondType, string>
                (preIngredientCheckingType, "", ApplicationRespondType.InternalError, errorMessage1);
        }
        (var preCapacitiesCheckingType, var errorMessage2) = CheckForIngredients(neededIngredients);
        if (preCapacitiesCheckingType != TryToPrepareDishRespondType.PreparingBegan)
        {
            return new DefaultApplicationRespond<TryToPrepareDishRespondType, string>
                (preCapacitiesCheckingType, "", ApplicationRespondType.InternalError, errorMessage2);
        }
        RetrieveRecipeIngredientsFromStorages(neededIngredients);
        DecreaseProductionCapacitiesNeededForRecipe(neededCapacities);
        _recipesInProcessOfPreparing.Enqueue(recipe, recipe.ProcessesQueueToPrepareDish.Sum(p => p.TimeToFinish.Seconds));
        _ordersContainerBase.Add(beginPrepareDishRequest.RequestData1);
        return new DefaultApplicationRespond<TryToPrepareDishRespondType, string>(
            TryToPrepareDishRespondType.PreparingBegan, recipe.Name, ApplicationRespondType.Success, $"You can prepare a dish");
    }

    [EateryWorker(typeof(Chief), typeof(Cook))]
    public IApplicationRespond<DishBase> EndPreparing(IApplicationRequest<string, Type> endPreparingDishRequest)
    {
        if (IsInRole(endPreparingDishRequest.Worker, nameof(BeginPreparing)) == false)
        {
            return GetDefaultAccessDeniedRespond<DishBase>(endPreparingDishRequest.Worker);
        }
        var recipe = _recipesInProcessOfPreparing.Peek();
        var respond = new DefaultApplicationRespond<DishBase>();
        if (recipe.Name != endPreparingDishRequest.RequestData1)
        {
            respond.Update(null, ApplicationRespondType.InternalError, "Dish is not already prepared or there are no such diches in preparing");
        }
        var dish = PrepareDish(endPreparingDishRequest.RequestData2, recipe);
        ReleaseCapacities(recipe);
        _ordersContainerBase.UpdateLastWith(dish);
        _recipesInProcessOfPreparing.Dequeue();
        return respond.Update(dish, ApplicationRespondType.Success, "Dish was prepared");
    }

    private DishBase PrepareDish(Type dishType, Recipe recipe)
    {
        DishBase dish = default;
        if (dishType == typeof(Dish))
        {
            dish = new Dish(new Helpers.Money(0, "USD"), 0, recipe.Name);
        }
        else if (dishType == typeof(Drink))
        {
            dish = new Drink(new Helpers.Money(0, "USD"), 0, recipe.Name);
        }
        foreach (var process in recipe.ProcessesQueueToPrepareDish)
        {
            if (process is IngredientProcess ingredientProcess)
            {
                ingredientProcess.Update(dish, recipe.IngredientsTakesPartInPreparing[ingredientProcess.CurrentIngredientType]);
            }
            if (process is IntermediateProcess intermediateProcess)
            {
                intermediateProcess.Update(dish);
            }
        }
        return dish;
    }

    private void ReleaseCapacities(Recipe recipe)
    {
        var releasingCapacities = recipe.ProcessesQueueToPrepareDish.GroupBy(k => k.GetType());
        foreach (var neededCapacity in releasingCapacities)
        {
            _capacitiesContainer.IncreaseCapacity(neededCapacity.Key, neededCapacity.Count());
        }
    }

    private void DecreaseProductionCapacitiesNeededForRecipe(IEnumerable<IGrouping<Type, DomainProductProcess>> neededCapacities)
    {
        foreach (var neededCapacity in neededCapacities)
        {
            _capacitiesContainer.DecreaseCapacity(neededCapacity.Key, neededCapacity.Count());
        }
    }

    private void RetrieveRecipeIngredientsFromStorages(IDictionary<Type, double> neededIngredients)
    {
        foreach (var neededIngredient in neededIngredients)
        {
            var neededIngredientWeight = neededIngredient.Value;
            var neededIngredientType = neededIngredient.Key;
            var storagesContainingIngredient = _storagesContainer.GetStoragesContainingIngredientType(neededIngredientType);
            for (int i = 0; i < storagesContainingIngredient.Count() && neededIngredientWeight > 0; ++i)
            {
                var storage = storagesContainingIngredient.ElementAt(i);
                var existingWeight = storage.GetExistingWeightOfIngredient(neededIngredientType);
                if (existingWeight <= neededIngredientWeight)
                {
                    storage.TryRetrieveAllIngredient(neededIngredientType);
                }
                else
                {
                    storage.TryRetrieveIngredientInWeight(neededIngredientType, neededIngredientWeight);
                }
                neededIngredientWeight -= existingWeight;
            }
        }
    }

    private (TryToPrepareDishRespondType, string) CheckForIngredients(IDictionary<Type, double> neededIngredients)
    {
        var existingIngredients = _storagesContainer.GetAllExistingIngredients();
        foreach (var neededIngredient in neededIngredients)
        {
            var neededWeight = neededIngredient.Value;
            if (existingIngredients.TryGetValue(neededIngredient.Key, out double existingProductWeight) == false)
            {
                return (TryToPrepareDishRespondType.NotEnoughIngredients,
                        $"Storages does not contain {neededIngredient.Key.Name} at all");
            }
            if (existingProductWeight < neededWeight)
            {
                return (TryToPrepareDishRespondType.NotEnoughIngredients,
                $"Storages contain {neededIngredient.Key.Name} in weight of {existingProductWeight} and you ask {neededWeight}");
            }
        }
        return (TryToPrepareDishRespondType.PreparingBegan, "");
    }
    private (TryToPrepareDishRespondType, string) CheckForCapacities
            (IEnumerable<IGrouping<Type, DomainProductProcess>> neededCapacities)
    {
        var existingCapacities = _capacitiesContainer.GetCurrentCapacities();
        var possibleCapacities = _capacitiesContainer.GetPossibleCapacities();
        foreach (var neededCapacity in neededCapacities)
        {
            if (existingCapacities.TryGetValue(neededCapacity.Key, out int existingCapacity) == false)
            {
                return (TryToPrepareDishRespondType.NotEnoughCapacitiesAtAll, $"Eatery does not have capacities for {neededCapacity.Key}");
            }
            var maximalCapacity = possibleCapacities[neededCapacity.Key];
            var neededCapacityCount = neededCapacity.Count();
            if (neededCapacityCount > maximalCapacity)
            {
                return (TryToPrepareDishRespondType.NotEnoughCapacitiesAtAll,
                        $"Eatery has {maximalCapacity} capacities for {neededCapacity.Key} and you ask {neededCapacityCount}");
            }
            if (neededCapacityCount <= maximalCapacity && neededCapacityCount > existingCapacity)
            {
                return (TryToPrepareDishRespondType.NotEnoghtCapacitiesAtThatMoment,
                       $"Eatery has {existingCapacities} out of {maximalCapacity} capacities" +
                       $" at that moment for {neededCapacity.Key} and you ask {neededCapacity}");
            }
        }
        return (TryToPrepareDishRespondType.PreparingBegan, "");
    }
}
