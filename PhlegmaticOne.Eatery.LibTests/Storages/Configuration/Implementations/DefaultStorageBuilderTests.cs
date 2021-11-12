using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.Lib.Storages.Tests
{
    [TestClass()]
    public class DefaultStorageBuilderTests
    {
        [TestMethod()]
        public void BuildTest()
        {
            var strorageBuilder = DefaultStorageContainer.GetDefaultStorageContainerBuilder()
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
                                            conf.With<Cucumber>().WithMaximalValueOfIngredient(100);
                                            conf.With<Tomato>().WithMaximalValueOfIngredient(100);
                                            conf.With<Olive>().WithMaximalValueOfIngredient(100);
                                        });
                                        builder.InAmountOf(2);
                                    });
            var container = strorageBuilder.Build();


            Assert.IsNotNull(container);

            var cellar = container.OfStorageType<Cellar>().First();
            var possibleTypesToKeep = cellar.GetIngredientsKeepingTypes();
            Assert.IsTrue(possibleTypesToKeep.Count == 3);
            Assert.AreEqual(100, possibleTypesToKeep[typeof(Cucumber)]);
            Assert.AreEqual(100, possibleTypesToKeep[typeof(Tomato)]);
            Assert.AreEqual(100, possibleTypesToKeep[typeof(Olive)]);
        }
    }
}