using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.Storages;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication.Tests;

[TestClass()]
public class StorageControllerTests
{
    private static StoragesController _storagesController;
    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        var strorageContainer = DefaultStorageContainer.GetDefaultStorageContainerBuilder()
            .RegisterStorage<Cellar, DefaultStorageBuilder<Cellar>>(builder =>
            {
                builder.WithLightning(StorageLightning.Darkness);
                builder.WithTemperarure<DefaultStorageTemperatureConfiguration<StorageTemperature>>(conf =>
                {
                    conf.WithMinimalTemperature(-20);
                    conf.WithMaximalTemperature(40);
                    conf.WithAverageTemperature(30);
                });
                builder.WithKeepingIngredientsTypes<DefaultStorageIngredientsConfiguration>(conf =>
                {
                    conf.With<Cucumber>().WithMaximalWeightOfIngredient(1000);
                    conf.With<Tomato>().WithMaximalWeightOfIngredient(1000);
                    conf.With<Olive>().WithMaximalWeightOfIngredient(1000);
                    conf.With<OliveOil>().WithMaximalWeightOfIngredient(1000);
                });
                builder.InAmountOf(2);
            })
            .Build();

        ///Конструктор internal, поэтому создать объект простым вызовом конструктора нельзя
        var type = typeof(StoragesController);
        _storagesController = type.Assembly.CreateInstance(
                             type.FullName, true, BindingFlags.Instance | BindingFlags.NonPublic,
                             null, new object[] { strorageContainer }, null, null) as StoragesController;
    }

    [TestMethod()]
    public void GetAllStoragesTest()
    {
        var cook = new Cook(Guid.NewGuid().ToString());
        var getAllStoragesRequest = EmptyApplicationRequest.Empty(cook);
        var getAllStoragesRespond = _storagesController.GetAllStorages(getAllStoragesRequest);
        foreach (var storage in getAllStoragesRespond.RespondResult1.AllStorages())
        {
            Assert.IsInstanceOfType(storage, typeof(Cellar));
        }
    }

    [TestMethod()]
    public void GetDefaultStorageBuilderTest()
    {
        var worker = new Manager(Guid.NewGuid().ToString());
        var storageBuilderRequest = EmptyApplicationRequest.Empty(worker);
        var storageBuilderResponce = _storagesController.GetDefaultStorageBuilder<Cellar>(storageBuilderRequest);
        Assert.IsNotNull(storageBuilderResponce.RespondResult1);
    }

    [TestMethod()]
    public void AddNewStoragesTest()
    {
        var worker = new Manager(Guid.NewGuid().ToString());
        var storageBuilderRequest = EmptyApplicationRequest.Empty(worker);
        var storageBuilderResponce = _storagesController.GetDefaultStorageBuilder<Cellar>(storageBuilderRequest);
        var storageBuilder = storageBuilderResponce.RespondResult1;
        storageBuilder.WithKeepingIngredientsTypes<DefaultStorageIngredientsConfiguration>(conf =>
        {
            conf.With<Tomato>().WithMaximalWeightOfIngredient(2000);
        });
        storageBuilder.WithLightning(StorageLightning.Daylight);
        storageBuilder.WithTemperarure<DefaultStorageTemperatureConfiguration<StorageTemperature>>(conf =>
        {
            conf.WithAverageTemperature(10);
            conf.WithMaximalTemperature(50);
            conf.WithMinimalTemperature(-2);
        });
        storageBuilder.InAmountOf(1);
        var newStorages = storageBuilder.Build();

        var addingRequest = new DefaultApplicationRequest<IEnumerable<Cellar>>(worker, newStorages);
        var addingResponce = _storagesController.AddNewStorages(addingRequest);

        var allStoragesRequest = EmptyApplicationRequest.Empty(new Cook("S"));
        var allStoragesResponse = _storagesController.GetAllStorages(allStoragesRequest);

        Assert.IsTrue(addingResponce.RespondResult1);
        Assert.AreEqual(3, allStoragesResponse.RespondResult1.Count);
    }

    [TestMethod()]
    public void RemoveStorageTest()
    {
        var worker = new Cook(Guid.NewGuid().ToString());
        var getAllStoragesRequest = EmptyApplicationRequest.Empty(worker);
        var getAllStoragesRespond = _storagesController.GetAllStorages(getAllStoragesRequest);
        var removedStorage = getAllStoragesRespond.RespondResult1.FirstOrDefaultStorage(s => s.Lightning == StorageLightning.Darkness);
        var manager = new Manager("s");
        var removeStorageRequest = new DefaultApplicationRequest<Storage>(manager, removedStorage);
        var removeStorageRespond = _storagesController.RemoveStorage(removeStorageRequest);
        Assert.AreEqual(1, getAllStoragesRespond.RespondResult1.Count);
    }
}
