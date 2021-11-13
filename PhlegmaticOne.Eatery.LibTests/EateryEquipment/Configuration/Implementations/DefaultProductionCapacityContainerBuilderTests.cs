using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.Lib.EateryEquipment.Tests
{
    [TestClass()]
    public class DefaultProductionCapacityContainerBuilderTests
    {
        [TestMethod()]
        public void SetMaximalIngredientsToProcessTest()
        {
            var productionCapacityContainer =
                    DefaultProductionCapacityContainer.GetDefaultProductionCapacityContainerBuilder()
                        .SetMaximalIngredientsToProcess<CuttingProcess>(30)
                        .SetMaximalIngredientsToProcess<MixingProcess>(5)
                    .Build();
            Assert.IsNotNull(productionCapacityContainer);
            Assert.AreEqual(30, productionCapacityContainer.GetMaximalProductsToProcess(typeof(CuttingProcess)));
            Assert.AreEqual(5, productionCapacityContainer.GetMaximalProductsToProcess(typeof(MixingProcess)));

        }
    }
}