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
                                            conf.With<Cucumber>();
                                            conf.With<Tomato>();
                                            conf.With<Olive>();
                                        });
                                        builder.InAmountOf(2);
                                    });
            var container = strorageBuilder.Build();

            Assert.IsNotNull(container);

            var allCellars = container.OfStorageType<Cellar>();
            Assert.IsTrue(container.Count == 2);
            Assert.IsTrue(allCellars.Count() == 2);
            Assert.IsTrue(allCellars.All(s => s is not null));
        }
    }
}