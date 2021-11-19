using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.JSONModelsSaving;
using PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Recipies;
using PhlegmaticOne.Eatery.Lib.Storages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.LibTests.MAIN_TESTS;
[TestClass]
public class ConfiguringApplicationFromReadyDataTests
{
    private static IEateryApplicationControllersContainer _controllersContainer;
    private const string DIRECTORY_WITH_CONFIGURING_FILES_PATH =
        @"C:\Users\lolol\source\repos\PhlegmaticOne.Eatery\PhlegmaticOne.Eatery.JSONModelsSavingTests\TestFiles\";
    [ClassInitialize]
    public static async Task ClassInitialize(TestContext testContext)
    {
        var asyncApplicationInitializer = new JsonEateryApplicationAsyncInitializer(
            new Dictionary<Type, string>()
            {
                { typeof(EateryMenuBase), DIRECTORY_WITH_CONFIGURING_FILES_PATH + "menu.json" },
                { typeof(StoragesContainerBase), DIRECTORY_WITH_CONFIGURING_FILES_PATH + "storages.json" },
                { typeof(IntermediateProcessContainerBase), DIRECTORY_WITH_CONFIGURING_FILES_PATH + "intermediate_processes.json" },
                { typeof(IngredientProcessContainerBase), DIRECTORY_WITH_CONFIGURING_FILES_PATH + "ingredient_processes.json" },
                { typeof(ProductionCapacitiesContainerBase), DIRECTORY_WITH_CONFIGURING_FILES_PATH + "production_capacities.json" },
                { typeof(EateryWorkersContainerBase), DIRECTORY_WITH_CONFIGURING_FILES_PATH + "workers.json" },
                { typeof(OrdersContainerBase), DIRECTORY_WITH_CONFIGURING_FILES_PATH + "orders.json" }
            });
        var applicationInstance = await EateryApplication.CreateAsync(asyncApplicationInitializer);
        _controllersContainer = applicationInstance.Run();
    }
    [TestMethod]
    public void IsNotNullTest()
    {
        Assert.IsNotNull(_controllersContainer);
    }
    [TestMethod]
    public void GetAllCapacities_NotBeginnedPreparing_Test()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var cook = workerController.LogIn(LogInApplicationRequest.Default("Vasya")).RespondResult1;
        var productionCapacitiesControoler = _controllersContainer.GetApplicationController<ProductionCapacitiesController>();
        var getAllCapacitiesRequest = EmptyApplicationRequest.Empty(cook);
        var getAllCapacitiesRespond = productionCapacitiesControoler.GetProductionCapacityContainer(getAllCapacitiesRequest);
        foreach (var capacity in getAllCapacitiesRespond.RespondResult1.GetCurrentCapacities())
        {
            Assert.AreEqual(30, capacity.Value);
        }
    }
    [TestMethod]
    public void GetAllCapacities_BeginnedPreparing_Test()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var cook = workerController.LogIn(LogInApplicationRequest.Default("Vasya")).RespondResult1;

        var recipeController = _controllersContainer.GetApplicationController<RecipeController>();
        var ordersController = _controllersContainer.GetApplicationController<OrderController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var createNewOrderRequest = new DefaultApplicationRequest<string, Type>(manager, "VegetableSalad", typeof(Dish));
        var createNewOrderRespond = ordersController.CreateNewOrder(createNewOrderRequest);

        var ordersQueueController = _controllersContainer.GetApplicationController<OrderQueueController>();
        var enqueueOrderRequest = new DefaultApplicationRequest<Order>(manager, createNewOrderRespond.RespondResult1);
        var enqueueOrderRespond = ordersQueueController.EnqueueNewOrder(enqueueOrderRequest);

        var dequeueOrderRespond = ordersQueueController.DequeueLatestOrder(EmptyApplicationRequest.Empty(cook));

        var getRecipeForDishOnOrderRequest = new DefaultApplicationRequest<Order>(cook, dequeueOrderRespond.RespondResult1);
        var getRecipeForDishOrderRespond = recipeController.GetRecipeForDishInOrder(getRecipeForDishOnOrderRequest);

        var preparingController = _controllersContainer.GetApplicationController<PreparingDishController>();
        var beginPreparingRequest = new DefaultApplicationRequest<Order, Recipe>(cook, getRecipeForDishOrderRespond.RespondResult1, getRecipeForDishOrderRespond.RespondResult2);
        var beginPrepareRespond = preparingController.BeginPreparing(beginPreparingRequest);
        var productionCapacitiesControoler = _controllersContainer.GetApplicationController<ProductionCapacitiesController>();


        var getAllCapacitiesRequest = EmptyApplicationRequest.Empty(cook);
        var getAllCapacitiesRespond = productionCapacitiesControoler.GetProductionCapacityContainer(getAllCapacitiesRequest);
        var currentCapacities = getAllCapacitiesRespond.RespondResult1.GetCurrentCapacities();
        Assert.AreEqual(27, currentCapacities[typeof(CuttingProcess)]);
        Assert.AreEqual(29, currentCapacities[typeof(MixingProcess)]);
    }

    [TestMethod]
    public void GetAllExistingIngredients_NotBeginnedPreparing_Test()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var cook = workerController.LogIn(LogInApplicationRequest.Default("Vasya")).RespondResult1;
        var ingredientController = _controllersContainer.GetApplicationController<IngredientsController>();
        var getAllIngredientsRequest = EmptyApplicationRequest.Empty(cook);
        var getAllCapacitiesRespond = ingredientController.GetAllExistingIngredients(getAllIngredientsRequest);
        foreach (var capacity in getAllCapacitiesRespond.RespondResult1)
        {
            Assert.AreEqual(300, capacity.Value);
        }
    }
    [TestMethod]
    public void GetAllIngredients_OnePreparedDish_Test()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var cook = workerController.LogIn(LogInApplicationRequest.Default("Vasya")).RespondResult1;
        var recipeController = _controllersContainer.GetApplicationController<RecipeController>();
        var ordersController = _controllersContainer.GetApplicationController<OrderController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var createNewOrderRequest = new DefaultApplicationRequest<string, Type>(manager, "VegetableSalad", typeof(Dish));
        var createNewOrderRespond = ordersController.CreateNewOrder(createNewOrderRequest);

        var ordersQueueController = _controllersContainer.GetApplicationController<OrderQueueController>();
        var enqueueOrderRequest = new DefaultApplicationRequest<Order>(manager, createNewOrderRespond.RespondResult1);
        var enqueueOrderRespond = ordersQueueController.EnqueueNewOrder(enqueueOrderRequest);

        var dequeueOrderRespond = ordersQueueController.DequeueLatestOrder(EmptyApplicationRequest.Empty(cook));

        var getRecipeForDishOnOrderRequest = new DefaultApplicationRequest<Order>(cook, dequeueOrderRespond.RespondResult1);
        var getRecipeForDishOrderRespond = recipeController.GetRecipeForDishInOrder(getRecipeForDishOnOrderRequest);

        var preparingController = _controllersContainer.GetApplicationController<PreparingDishController>();
        var beginPreparingRequest = new DefaultApplicationRequest<Order, Recipe>(cook, getRecipeForDishOrderRespond.RespondResult1, getRecipeForDishOrderRespond.RespondResult2);
        var beginPrepareRespond = preparingController.BeginPreparing(beginPreparingRequest);
        var endPreparingRequest = new DefaultApplicationRequest<string>(cook, beginPrepareRespond.RespondResult2);
        var endPreparingRespond = preparingController.EndPreparing(endPreparingRequest);

        var ingredientController = _controllersContainer.GetApplicationController<IngredientsController>();
        var getAllIngredientsRequest = EmptyApplicationRequest.Empty(cook);
        var getAllCapacitiesRespond = ingredientController.GetAllExistingIngredients(getAllIngredientsRequest);
        Assert.AreEqual(200, getAllCapacitiesRespond.RespondResult1[typeof(Cucumber)]);
        Assert.AreEqual(200, getAllCapacitiesRespond.RespondResult1[typeof(Tomato)]);
        Assert.AreEqual(270, getAllCapacitiesRespond.RespondResult1[typeof(Olive)]);
        Assert.AreEqual(250, getAllCapacitiesRespond.RespondResult1[typeof(OliveOil)]);
    }

    [TestMethod]
    public void GetOrdersInDataRange_Empty_Test()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var statisticsController = _controllersContainer.GetApplicationController<StatiticsController>();
        var getOrdersInDataRange = new DefaultApplicationRequest<DateTime, DateTime>
            (manager, DateTime.Today - TimeSpan.FromDays(20), DateTime.Today - TimeSpan.FromDays(10));
        var ordersInDataRengeRespond = statisticsController.GetOrdersInDataRange(getOrdersInDataRange);
        Assert.AreEqual(0, ordersInDataRengeRespond.RespondResult1.Count);
    }
    [TestMethod]
    public void GetOrdersInDataRange_NotEmpty_Test()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var statisticsController = _controllersContainer.GetApplicationController<StatiticsController>();
        var getOrdersInDataRange = new DefaultApplicationRequest<DateTime, DateTime>
            (manager, DateTime.Today - TimeSpan.FromDays(2) - TimeSpan.FromHours(2), DateTime.Today + TimeSpan.FromDays(10));
        var ordersInDataRengeRespond = statisticsController.GetOrdersInDataRange(getOrdersInDataRange);
        Assert.AreEqual(6, ordersInDataRengeRespond.RespondResult1.Count);
    }
    [TestMethod]
    public void GetMostUsedIngredientInWeightTest()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var statisticsController = _controllersContainer.GetApplicationController<StatiticsController>();
        var getMostUsedIngredientInWeight = statisticsController.GetMostUsedIngredientInWeight(EmptyApplicationRequest.Empty(manager));
        Assert.AreEqual(800, getMostUsedIngredientInWeight.RespondResult1.UsedWeight);
        Assert.AreEqual("Cucumber", getMostUsedIngredientInWeight.RespondResult1.IngredientType.Name);
    }
    [TestMethod]
    public void GetLeastUsedIngredientInWeightTest()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var statisticsController = _controllersContainer.GetApplicationController<StatiticsController>();
        var getMostUsedIngredientInWeight = statisticsController.GetLeastUsedIngredientInWeight(EmptyApplicationRequest.Empty(manager));
        Assert.AreEqual(240, getMostUsedIngredientInWeight.RespondResult1.UsedWeight);
        Assert.AreEqual("Olive", getMostUsedIngredientInWeight.RespondResult1.IngredientType.Name);
    }
    [TestMethod]
    public void GetMostUsedIngredientInCountTest()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var statisticsController = _controllersContainer.GetApplicationController<StatiticsController>();
        var getMostUsedIngredientInWeight = statisticsController.GetMostUsedIngredientInCount(EmptyApplicationRequest.Empty(manager));
        Assert.AreEqual(8, getMostUsedIngredientInWeight.RespondResult1.TimesUsed);
        Assert.AreEqual("Cucumber", getMostUsedIngredientInWeight.RespondResult1.IngredientType.Name);
    }
    [TestMethod]
    public void GetLeastUsedIngredientInCountTest()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var statisticsController = _controllersContainer.GetApplicationController<StatiticsController>();
        var getMostUsedIngredientInWeight = statisticsController.GetLeastUsedIngredientInCount(EmptyApplicationRequest.Empty(manager));
        Assert.AreEqual(8, getMostUsedIngredientInWeight.RespondResult1.TimesUsed);
        Assert.AreEqual("Cucumber", getMostUsedIngredientInWeight.RespondResult1.IngredientType.Name);
    }
    [TestMethod]
    public void GetIngredientsByConditionsTest()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var storagesController = _controllersContainer.GetApplicationController<StoragesController>();
        var cook = workerController.LogIn(LogInApplicationRequest.Default("Vasya")).RespondResult1;
        var getAllStoragesRequest = EmptyApplicationRequest.Empty(cook);
        var getAllStoragesRespond = storagesController.GetAllStorages(getAllStoragesRequest);
        var fiitedStorage = getAllStoragesRespond.RespondResult1.FirstOrDefaultStorage(s => s.Lightning == StorageLightning.Darkness);
        foreach (var ingredient in fiitedStorage.GetIngredientsKeepingTypes())
        {
            Assert.IsTrue(ingredient.Key.IsAssignableTo(typeof(Ingredient)));
        }
    }
    [TestMethod]
    public void GetMostExpensiveProcessTest()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var statisticsController = _controllersContainer.GetApplicationController<StatiticsController>();
        var getMostUsedIngredientInWeight = statisticsController.GetMostExpensiveProcessOverDish(EmptyApplicationRequest.Empty(manager));
        Assert.IsInstanceOfType(getMostUsedIngredientInWeight.RespondResult1, typeof(AddingProcess));
        Assert.AreEqual(0.26, getMostUsedIngredientInWeight.RespondResult1.Price.Amount);
    }
    [TestMethod]
    public void GetLeastExpensiveProcesstTest()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var statisticsController = _controllersContainer.GetApplicationController<StatiticsController>();
        var getMostUsedIngredientInWeight = statisticsController.GetLeastExpensiveProcessOverDish(EmptyApplicationRequest.Empty(manager));
        Assert.IsInstanceOfType(getMostUsedIngredientInWeight.RespondResult1, typeof(CuttingProcess));
        Assert.AreEqual(0.13, getMostUsedIngredientInWeight.RespondResult1.Price.Amount);
    }
    [TestMethod]
    public void GetLongestProcesstTest()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var statisticsController = _controllersContainer.GetApplicationController<StatiticsController>();
        var getMostUsedIngredientInWeight = statisticsController.GetLongestProcessOverDish(EmptyApplicationRequest.Empty(manager));
        Assert.IsInstanceOfType(getMostUsedIngredientInWeight.RespondResult1, typeof(MixingProcess));
        Assert.AreEqual(TimeSpan.FromMinutes(2), getMostUsedIngredientInWeight.RespondResult1.TimeToFinish);
    }
    [TestMethod]
    public void GetShortestProcessTest()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var statisticsController = _controllersContainer.GetApplicationController<StatiticsController>();
        var getMostUsedIngredientInWeight = statisticsController.GetShortestProcessOverDish(EmptyApplicationRequest.Empty(manager));
        Assert.IsInstanceOfType(getMostUsedIngredientInWeight.RespondResult1, typeof(AddingProcess));
        Assert.AreEqual(TimeSpan.FromSeconds(10), getMostUsedIngredientInWeight.RespondResult1.TimeToFinish);
    }
    [TestMethod]
    public void GetDishesUsageTest()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var statisticsController = _controllersContainer.GetApplicationController<StatiticsController>();
        var getMostUsedIngredientInWeight = statisticsController.GetUsageOfDishes(EmptyApplicationRequest.Empty(manager));
        var usageInfo = getMostUsedIngredientInWeight.RespondResult1;
        Assert.AreEqual(80, usageInfo.EarnedMoney.Amount);
    }
    [TestMethod]
    public void GetDrinksUsageTest()
    {
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var manager = workerController.LogIn(LogInApplicationRequest.Default("Vladimir")).RespondResult1;
        var statisticsController = _controllersContainer.GetApplicationController<StatiticsController>();
        var getMostUsedIngredientInWeight = statisticsController.GetUsageOfDrinks(EmptyApplicationRequest.Empty(manager));
        var usageInfo = getMostUsedIngredientInWeight.RespondResult1;
        Assert.AreEqual(0, usageInfo.EarnedMoney.Amount);
    }
}
