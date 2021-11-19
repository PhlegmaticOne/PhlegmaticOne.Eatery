using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.JSONModelsSaving;
using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Orders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
    public void ASaveOrdersTest()
    {
        //Было сделано для тестов. В приложении public конструктор не доступен
        //var orders = new List<Order>()
        //{
        //    new Order(1, new Dish(new Money(10, "USD"), 100, "VegetableSalad"), DateTime.Now - TimeSpan.FromDays(5), "VegetableSalad"),
        //    new Order(2, new Dish(new Money(10, "USD"), 100, "VegetableSalad"), DateTime.Now - TimeSpan.FromDays(3), "VegetableSalad"),
        //    new Order(3, new Dish(new Money(10, "USD"), 100, "VegetableSalad"), DateTime.Now - TimeSpan.FromDays(2) - TimeSpan.FromHours(3), "VegetableSalad"),
        //    new Order(4, new Dish(new Money(10, "USD"), 100, "VegetableSalad"), DateTime.Now - TimeSpan.FromDays(1) - TimeSpan.FromHours(2), "VegetableSalad"),
        //    new Order(5, new Dish(new Money(10, "USD"), 100, "VegetableSalad"), DateTime.Now, "VegetableSalad"),
        //    new Order(6, new Dish(new Money(10, "USD"), 100, "VegetableSalad"), DateTime.Now + TimeSpan.FromHours(2), "VegetableSalad"),
        //    new Order(7, new Dish(new Money(10, "USD"), 100, "VegetableSalad"), DateTime.Now + TimeSpan.FromDays(2), "VegetableSalad"),
        //    new Order(8, new Dish(new Money(10, "USD"), 100, "VegetableSalad"), DateTime.Now + TimeSpan.FromDays(3) + TimeSpan.FromHours(2), "VegetableSalad"),
        //};
        //var ordersContainer = new DefaultOrderContainer(orders.ToDictionary(k => k.Id));
        //_ordersJsonWorker.Save(ordersContainer);
        //Assert.IsTrue(File.Exists(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME));
    }
    [TestMethod()]
    public async Task BLoadOrdersTest()
    {
        var orders = await _ordersJsonWorker.LoadAsync<DefaultOrderContainer>();
        Assert.IsInstanceOfType(orders, typeof(DefaultOrderContainer));
    }
}
