using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using System;
using System.Linq;

namespace PhlegmaticOne.Eatery.Lib.Recipies.Tests
{
    [TestClass()]
    public class DefaultRecipeBuilderTests
    {
        [TestMethod()]
        public void ConfigureTest()
        {
            var ingredientProcessesContainer = DefaultProcessContainer.GetDefaultContainerBuilder()
                    .ConfigureProcess<CuttingProcess, DefaultProcessBuilder<CuttingProcess>>(builder =>
                    {
                        builder.CanProcess<Cucumber>()
                                .WithCost(new Money(10, "RUB"))
                                .WithTimeToFinish(TimeSpan.FromMinutes(2));
                        builder.CanProcess<Tomato>()
                                .WithCost(new Money(10, "RUB"))
                                .WithTimeToFinish(TimeSpan.FromMinutes(2));
                        builder.CanProcess<Olive>()
                                .WithCost(new Money(10, "RUB"))
                                .WithTimeToFinish(TimeSpan.FromMinutes(2));
                    })
                    .ConfigureProcess<AddingProcess, DefaultProcessBuilder<AddingProcess>>(builder =>
                    {
                        builder.CanProcess<Cucumber>()
                                .WithCost(new Money(20, "RUB"))
                                .WithTimeToFinish(TimeSpan.FromMinutes(2));
                        builder.CanProcess<Tomato>()
                                .WithCost(new Money(20, "RUB"))
                                .WithTimeToFinish(TimeSpan.FromMinutes(2));
                        builder.CanProcess<Olive>()
                                .WithCost(new Money(20, "RUB"))
                                .WithTimeToFinish(TimeSpan.FromMinutes(2));
                        builder.CanProcess<OliveOil>()
                                .WithCost(new Money(20, "RUB"))
                                .WithTimeToFinish(TimeSpan.FromMinutes(2));
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
            Assert.IsNotNull(recipe);
            Assert.AreEqual("VegetableSalad", recipe.Name);
            Assert.AreEqual(8, recipe.GetProcessesQueueToPrepareDish().Count);
            Assert.AreEqual(4, recipe.GetIngredientsTakesPartInPreparing().Count);
            Assert.AreEqual(new Money(10, "RUB"), recipe.GetProcessesQueueToPrepareDish().Last().Price);
        }
    }
}