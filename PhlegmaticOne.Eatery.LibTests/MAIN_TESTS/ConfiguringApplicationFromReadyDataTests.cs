using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.JSONModelsSaving;
using PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Storages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.LibTests.MAIN_TESTS;
[TestClass]
public class ConfiguringApplicationFromReadyDataTests
{
    private static IEateryApplicationControllersContainer _controllersContainer;
    private const string DIRECTORY_WITH_CONFIGURING_FILES_PATH = @"C:\Users\lolol\source\repos\PhlegmaticOne.Eatery\PhlegmaticOne.Eatery.JSONModelsSavingTests\TestFiles\";
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
}
