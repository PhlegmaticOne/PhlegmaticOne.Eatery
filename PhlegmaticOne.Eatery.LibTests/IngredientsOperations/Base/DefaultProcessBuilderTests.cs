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
            var cuttingProcessContainer =
                            DefaultProcessContainer.GetDefaultContainerBuilder<CuttingProcess>()
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
            Assert.IsNotNull(cuttingProcessContainer);

            var first = cuttingProcessContainer.GetProcessOf<Cucumber>();
            Assert.IsTrue(first.Price.Amount == 10);
            Assert.IsTrue(first.TimeToFinish.Minutes == 10);

            var second = cuttingProcessContainer.GetProcessOf<Tomato>();
            Assert.IsTrue(second.Price.Amount == 20);
            Assert.IsTrue(second.TimeToFinish.Minutes == 2);

            var third = cuttingProcessContainer.GetProcessOf<Olive>();
            Assert.IsTrue(third.Price.Amount == 15);
            Assert.IsTrue(third.TimeToFinish.Minutes == 4);
        }
    }
}