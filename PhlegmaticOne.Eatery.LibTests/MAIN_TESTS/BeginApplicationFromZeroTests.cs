using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
using PhlegmaticOne.Eatery.Lib.Dishes;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Orders;
using PhlegmaticOne.Eatery.Lib.Recipies;
using PhlegmaticOne.Eatery.Lib.Storages;
using System;
using System.Collections.Generic;

namespace PhlegmaticOne.Eatery.LibTests.MAIN_TESTS;

[TestClass()]
public class BeginApplicationFromZeroTests
{
    private static IEateryApplicationControllersContainer _controllersContainer;
    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        var ingredientProcessesContainer = DefaultIngredientProcessContainer.GetDefaultContainerBuilder()
            .ConfigureProcess<CuttingProcess, DefaultIngredientProcessBuilder<CuttingProcess>>(builder =>
            {
                builder.CanProcess<Cucumber>().WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                builder.CanProcess<Tomato>().WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                builder.CanProcess<Olive>().WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
            })
            .ConfigureProcess<AddingProcess, DefaultIngredientProcessBuilder<AddingProcess>>(builder =>
            {
                builder.CanProcess<Cucumber>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromSeconds(10));
                builder.CanProcess<Tomato>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromSeconds(20));
                builder.CanProcess<Olive>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromSeconds(30));
                builder.CanProcess<OliveOil>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromSeconds(40));
            })
            .Build();

        var intermediateProcessContainer =
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

        var eateryWorkersContainer = DefaultEateryWorkersContainer.GetDefaultWorkersBuilder()
                                     .AddCook(new Cook("Vasya"))
                                     .AddCook(new Cook("Vitya"))
                                     .AddManager(new Manager("Vitaliy"))
                                     .AddManager(new Manager("Vladimir"))
                                     .AddChief(new Chief("Vladilen"))
                                     .Build();

        _controllersContainer = EateryApplication.Create<DefaultEateryApplicationBuilder>(builder =>
        {
            builder.UseIngredientProcessesContainer(ingredientProcessesContainer);
            builder.UseIntermediateProcessContainer(intermediateProcessContainer);
            builder.UseProductionCapacityContainer(productionCapacityContainer);
            builder.UseStorageContainer(strorageContainer);
            builder.UseEateryMenu(new EateryMenu());
            builder.UseEateryWorkersContainer(eateryWorkersContainer);
            builder.UseOrdersContainer(new DefaultOrderContainer());
        }).Run();
    }

    [TestMethod]
    public void IsNotNullTest()
    {
        Assert.IsNotNull(_controllersContainer);
    }
    [TestMethod]
    public void ApplicationWorkTest()
    {
        var ingredientsController = _controllersContainer.GetApplicationController<IngredientsController>();
        var workerController = _controllersContainer.GetApplicationController<WorkersController>();
        var cook = workerController.LogIn(LogInApplicationRequest.Default("Vasya")).RespondResult1;

        var addIngredientsRequest = new DefaultApplicationRequest<Func<Storage, bool>, IEnumerable<Ingredient>>(
            cook, storage => storage.Lightning == StorageLightning.Darkness,
            new List<Ingredient>()
            {
                new Cucumber(500, 500),
                new Tomato(500, 500),
                new Olive(500, 500),
                new OliveOil(500, 500),
            }
        );
        var addIngredientsRespond = ingredientsController.AddIngredientsInStorage(addIngredientsRequest);

        var recipeController = _controllersContainer.GetApplicationController<RecipeController>();
        var chief = workerController.LogIn(LogInApplicationRequest.Default("Vladilen")).RespondResult1;
        var createNewRecipeRequest = new DefaultApplicationRequest<string>(chief, "VegetableSalad");
        var createNewRecipeRespond = recipeController.GetRecipeBuilder(createNewRecipeRequest);
        var recipe = GetRecipe(createNewRecipeRespond.RespondResult1);

        var eateryMenuController = _controllersContainer.GetApplicationController<EateryMenuController>();
        var addRecipeInMenuRequest = new DefaultApplicationRequest<Recipe>(chief, recipe);
        var addRecipeInMenuRespond = eateryMenuController.AddRecipeInMenu(addRecipeInMenuRequest);

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

        var preparedDish = endPreparingRespond.RespondResult1;
        Assert.IsNotNull(preparedDish);
        Assert.IsInstanceOfType(preparedDish, typeof(Dish));
        Assert.IsTrue(preparedDish.Price.Amount > 0);
        Assert.AreEqual("VegetableSalad", preparedDish.Name);
    }
    private Recipe GetRecipe(IRecipeBuilder recipeBuilder)
    {
        var recipe = recipeBuilder.Configure<DefaultRecipeIngredientTypesConfiguration, DefaultProcessSequenceBuilder>(
                        recipeBuilder =>
                        {
                            recipeBuilder.NeedIngredient<Cucumber>().InWeightOf(100);
                            recipeBuilder.NeedIngredient<Tomato>().InWeightOf(100);
                            recipeBuilder.NeedIngredient<Olive>().InWeightOf(30);
                            recipeBuilder.NeedIngredient<OliveOil>().InWeightOf(50);
                        },
                        sequenceBuilder =>
                        {
                            sequenceBuilder.InsertInSequence<AddingProcess, Cucumber>();
                            sequenceBuilder.InsertInSequence<CuttingProcess, Cucumber>();
                            sequenceBuilder.InsertInSequence<AddingProcess, Tomato>();
                            sequenceBuilder.InsertInSequence<CuttingProcess, Tomato>();
                            sequenceBuilder.InsertInSequence<AddingProcess, Olive>();
                            sequenceBuilder.InsertInSequence<CuttingProcess, Olive>();
                            sequenceBuilder.InsertInSequence<AddingProcess, OliveOil>();
                            sequenceBuilder.InsertInSequence<MixingProcess>();
                        }
                    )
                    .Create();
        return recipe;
    }
}
