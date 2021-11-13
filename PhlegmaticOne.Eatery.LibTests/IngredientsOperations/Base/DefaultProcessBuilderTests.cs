using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using System;

namespace PhlegmaticOne.Eatery.Lib.Models.Ingredients.Tests
{
    [TestClass()]
    public class DefaultProcessBuilderTests
    {
        [TestMethod()]
        public void RegisterAsPossibleToProcessTest()
        {
            var ingredientProcessesContainer = DefaultProcessContainer.GetDefaultContainerBuilder()
                .ConfigureProcess<CuttingProcess, DefaultProcessBuilder<CuttingProcess>>(builder =>
                {
                    builder.CanProcess<Cucumber>().WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                    builder.CanProcess<Tomato>().WithCost(new Money(20, "USD")).WithTimeToFinish(TimeSpan.FromMinutes(1));
                    builder.CanProcess<Olive>().WithCost(new Money(30, "EUR")).WithTimeToFinish(TimeSpan.FromMinutes(4));
                })
                .ConfigureProcess<AddingProcess, DefaultProcessBuilder<AddingProcess>>(builder =>
                {
                    builder.CanProcess<Cucumber>().WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(1));
                    builder.CanProcess<Tomato>().WithCost(new Money(20, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                    builder.CanProcess<Olive>().WithCost(new Money(30, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(3));
                    builder.CanProcess<OliveOil>().WithCost(new Money(40, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(4));
                })
                .Build();
            Assert.IsNotNull(ingredientProcessesContainer);
        }
    }
}