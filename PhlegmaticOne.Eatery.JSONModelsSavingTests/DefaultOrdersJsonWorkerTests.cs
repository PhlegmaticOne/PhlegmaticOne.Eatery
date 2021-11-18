using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Orders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.JSONModelsSaving.Tests;

[TestClass()]
public class DefaultOrdersJsonWorkerTests
{
    private const string DIRECTORY_WITH_TESTED_FILES_PATH = @"C:\Users\lolol\source\repos\PhlegmaticOne.Eatery\PhlegmaticOne.Eatery.JSONModelsSavingTests\TestFiles\";
    private const string WORKERS_FILE_NAME = @"orders.json";
    private static DefaultOrdersJsonWorker _ordersJsonWorker;

    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
        _ordersJsonWorker = new(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME);
    }
    [TestMethod]
    public async Task ASaveOrdersTest()
    {
        //Было сделано для тестов. В приложении public конструктор не доступен
        //var orders = new List<Order>()
        //{
        //    new Order(1, new Dish(new Money(10, "USD"), 100, "VegetableSalad"), DateTime.Now - TimeSpan.FromMinutes(2), "VegetableSalad"),
        //    new Order(2, new Dish(new Money(10, "USD"), 100, "VegetableSalad"), DateTime.Now - TimeSpan.FromMinutes(1), "VegetableSalad"),
        //    new Order(3, new Dish(new Money(10, "USD"), 100, "VegetableSalad"), DateTime.Now, "VegetableSalad"),
        //    new Order(4, new Dish(new Money(10, "USD"), 100, "VegetableSalad"), DateTime.Now + TimeSpan.FromMinutes(2), "VegetableSalad")
        //};
        //var ordersContainer = new DefaultOrderContainer(orders.ToDictionary(k => k.Id));
        //await _ordersJsonWorker.SaveAsync(ordersContainer);
        //Assert.IsTrue(File.Exists(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME));
    }
    [TestMethod()]
    public async Task BLoadOrdersTest()
    {
        var orders = await _ordersJsonWorker.LoadAsync<DefaultOrderContainer>();
        Assert.IsInstanceOfType(orders, typeof(DefaultOrderContainer));
    }
}
