using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using PhlegmaticOne.Eatery.Lib.Recipies;
using PhlegmaticOne.Eatery.Lib.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.Lib.Recipies.Tests
{
    [TestClass()]
    public class DefaultRecipeBuilderTests
    {
        [TestMethod()]
        public void ConfigureTest()
        {
            var cuttingProcessContainer = DefaultProcessContainer.GetDefaultContainerBuilder<CuttingProcess>()
                          .RegisterAsPossibleToProcess<Cucumber>(init =>
                               init.WithCost(new Money(10, "USD"))
                                   .WithTimeToFinish(TimeSpan.FromMinutes(10)))
                          .RegisterAsPossibleToProcess<Tomato>(init =>
                               init.WithCost(new Money(20, "EUR"))
                                   .WithTimeToFinish(TimeSpan.FromMinutes(2)))
                          .RegisterAsPossibleToProcess<Olive>(init =>
                               init.WithCost(new Money(15, "RUB"))
                                   .WithTimeToFinish(TimeSpan.FromMinutes(4)))
                          .Build();

            var recipe = Recipe.GetDefaultRecipeBuilder()
                        .Configure<DefaultRecipeIngredientTypesConfiguration, DefaultProcessSequenceBuilder>(
                            recipeBuilder =>
                            {
                                recipeBuilder.NeedIngredient<Cucumber>()
                                             .InWeightOf(100);
                                recipeBuilder.NeedIngredient<Tomato>()
                                             .InWeightOf(100);
                            },
                            sequenceBuilder =>
                            {
                                sequenceBuilder.SetSource(cuttingProcessContainer).InsertInSequence<Cucumber>();
                                sequenceBuilder.SetSource(cuttingProcessContainer).InsertInSequence<Tomato>();
                            }
                        );
            Assert.IsNotNull(recipe);
        }
    }
}