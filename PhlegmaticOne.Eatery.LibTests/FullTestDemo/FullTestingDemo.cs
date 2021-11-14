using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Recipies;
using PhlegmaticOne.Eatery.Lib.Storages;
using System;
using System.Linq;

namespace PhlegmaticOne.Eatery.LibTests.FullTestDemo;
[TestClass]
public class FullTestingDemo
{
    [TestMethod]
    public void FullTest()
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
                        .SetMaximalIngredientsToProcess<CuttingProcess>(3)
                        .SetMaximalIngredientsToProcess<MixingProcess>(5)
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

        var storage = strorageContainer.OfStorageType<Cellar>().First();
        storage.TryAdd(typeof(Cucumber), 100);
        storage.TryAdd(typeof(Tomato), 100);
        storage.TryAdd(typeof(Olive), 100);
        storage.TryAdd(typeof(OliveOil), 100);

        var recipe = Recipe.GetDefaultRecipeBuilder("VegetableSalad")
            .Configure<DefaultRecipeIngredientTypesConfiguration, DefaultProcessSequenceBuilder>(
                recipeBuilder =>
                {
                    recipeBuilder.NeedIngredient<Cucumber>().InWeightOf(100);
                    recipeBuilder.NeedIngredient<Tomato>().InWeightOf(100);
                    recipeBuilder.NeedIngredient<Olive>().InWeightOf(30);
                    recipeBuilder.NeedIngredient<OliveOil>().InWeightOf(50);
                },
                sequenceBuilder =>
                {
                    sequenceBuilder.SetSources(ingredientProcessesContainer, intermidiateStorageContainer);
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

        var recipeController = new RecipeController(recipe, strorageContainer, productionCapacityContainer);
        var prepareResult = recipeController.Prepare();
        Assert.IsNotNull(prepareResult);
        Assert.IsTrue(prepareResult.PrepareResultType == PrepareResultType.NotEnoughProductionCapacity);
        Assert.IsNull(prepareResult.Dish);
    }
}
