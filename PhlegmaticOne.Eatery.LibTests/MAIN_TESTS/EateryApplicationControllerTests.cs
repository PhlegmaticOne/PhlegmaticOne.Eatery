using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Storages;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PhlegmaticOne.Eatery.LibTests.MAIN_TESTS;

[TestClass()]
public class EateryApplicationControllerTests
{
    private static EateryApplicationStorageController _storagesController;
    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        var ingredientProcessesContainer = DefaultProcessContainer.GetDefaultContainerBuilder()
            .ConfigureProcess<CuttingProcess, DefaultProcessBuilder<CuttingProcess>>(builder =>
            {
                builder.CanProcess<Cucumber>().WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                builder.CanProcess<Tomato>().WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                builder.CanProcess<Olive>().WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
            })
            .ConfigureProcess<AddingProcess, DefaultProcessBuilder<AddingProcess>>(builder =>
            {
                builder.CanProcess<Cucumber>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                builder.CanProcess<Tomato>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                builder.CanProcess<Olive>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                builder.CanProcess<OliveOil>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
            })
            .Build();

        var intermidiateStorageContainer =
            DefaultIntermediateProcessContainer.GetDefaultIntermediateProcessContainerBuilder()
            .ConfigureProcess<MixingProcess, DefaultIntermadiateProcessBuilder<MixingProcess>>(builder =>
            {
                builder.DishMayContain<Cucumber>().DishMayContain<Tomato>()
                       .WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                builder.SetDefaultProcess(new Money(20, "RUB"), TimeSpan.FromMinutes(1));
            })
            .Build();

        var productionCapacityContainer =
                    DefaultProductionCapacityContainer.GetDefaultProductionCapacityContainerBuilder()
                        .SetMaximalIngredientsToProcess<CuttingProcess>(30)
                        .SetMaximalIngredientsToProcess<MixingProcess>(30)
                    .Build();

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

        _storagesController = new EateryApplicationStorageController(strorageContainer);
    }
    [TestMethod]
    public void IsNotNullTest()
    {
        Assert.IsNotNull(_storagesController);
    }
    [TestMethod]
    public void GetAllStoragesTest()
    {
        var storagesRespond = _storagesController.GetAllStorages(new DefaultApplicationRequest<object>(new Cook(), null));
        Assert.IsTrue(storagesRespond.RespondType == ApplicationRespondType.Success);
        Assert.IsInstanceOfType(storagesRespond.Result, typeof(IReadOnlyCollection<Storage>));
        Assert.IsTrue(storagesRespond.Result.Count == 2);
    }
    [TestMethod]
    public void GetStorageBuilder_AccessDenied_Test()
    {
        var storageBuilderRespond = _storagesController.GetDefaultStorageBuilder<Cellar>(
            new DefaultApplicationRequest<object>(new Cook(), null));

        Assert.IsTrue(storageBuilderRespond.RespondType == ApplicationRespondType.AccessDenied);
        Assert.IsNull(storageBuilderRespond.Result);
    }
    [TestMethod]
    public void GetStorageBuilder_AccessSuccessfull_Test()
    {
        var storageBuilderRespond = _storagesController.GetDefaultStorageBuilder<Cellar>(
            new DefaultApplicationRequest<object>(new Manager(), null));
        Assert.IsTrue(storageBuilderRespond.RespondType == ApplicationRespondType.Success);
        Assert.IsInstanceOfType(storageBuilderRespond.Result, typeof(IStorageBuilder<Cellar>));
    }
    [TestMethod]
    public void AddNewStorageTest()
    {
        var manager = new Manager();
        var storageBuilderRequest = new DefaultApplicationRequest<object>(manager, null);
        var storageBuilderResponce = _storagesController.GetDefaultStorageBuilder<Cellar>(storageBuilderRequest);

        var storageBuilder = storageBuilderResponce.Result;

        storageBuilder.WithKeepingIngredientsTypes<DefaultStorageIngredientsConfiguration>(
            conf =>
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

        var addingRequest = new DefaultApplicationRequest<IEnumerable<Cellar>>(manager, newStorages);
        var addingResponce =  _storagesController.Add(addingRequest);

        var allStoragesRequest = new DefaultApplicationRequest<object>(manager, null);
        var allStoragesResponse = _storagesController.GetAllStorages(allStoragesRequest);

        Assert.IsTrue(addingResponce.Result);

        Assert.AreEqual(3, allStoragesResponse.Result.Count);
    }
}
