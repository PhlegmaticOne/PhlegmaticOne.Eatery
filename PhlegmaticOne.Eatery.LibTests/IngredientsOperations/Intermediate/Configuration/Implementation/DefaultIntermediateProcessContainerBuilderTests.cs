using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.Lib.IngredientsOperations.Tests
{
    [TestClass()]
    public class DefaultIntermediateProcessContainerBuilderTests
    {
        [TestMethod()]
        public void ConfigureProcessTest()
        {
            var intermediateProcessContainer =
                DefaultIntermediateProcessContainer.GetDefaultIntermediateProcessContainerBuilder()
                .ConfigureProcess<MixingProcess, DefaultIntermadiateProcessBuilder<MixingProcess>>(builder =>
                {
                    builder.DishMayContain<Cucumber>().DishMayContain<Tomato>()
                           .WithCost(new Money(10, "RUB")).WithTimeToFinish(TimeSpan.FromMinutes(2));
                    builder.SetDefaultProcess(new Money(20, "RUB"), TimeSpan.FromMinutes(1));
                })
                .Build();
            Assert.IsNotNull(intermediateProcessContainer);
            Assert.IsTrue(intermediateProcessContainer.GetProcess<MixingProcess>
                         (new List<Type>() { typeof(Tomato) }).Price.Amount == 10);
        }
    }
}