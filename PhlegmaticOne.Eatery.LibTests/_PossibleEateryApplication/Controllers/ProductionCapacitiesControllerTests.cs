using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using System.Reflection;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication.Tests
{
    [TestClass()]
    public class ProductionCapacitiesControllerTests
    {
        private static ProductionCapacitiesController _productionCapacitiesController;
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
            var productionCapacityContainer =
                    DefaultProductionCapacityContainer.GetDefaultProductionCapacityContainerBuilder()
                        .SetMaximalIngredientsToProcess<CuttingProcess>(30)
                        .SetMaximalIngredientsToProcess<MixingProcess>(30)
                    .Build();

            ///Конструктор internal, поэтому создать объект простым вызовом конструктора нельзя
            var type = typeof(ProductionCapacitiesController);
            _productionCapacitiesController = type.Assembly.CreateInstance(
                                 type.FullName, true, BindingFlags.Instance | BindingFlags.NonPublic,
                                 null, new object[] { productionCapacityContainer }, null, null) as ProductionCapacitiesController;
        }

        [TestMethod()]
        public void GetProductionMaximalCapacityContainerTest()
        {
            var cook = new Cook("Vasya");
            var getProductionCapacityRequest = EmptyApplicationRequest.Empty(cook);
            var getProductionCapacityRespond = _productionCapacitiesController
                                               .GetProductionCapacityContainer(getProductionCapacityRequest);
            foreach (var processCapacity in getProductionCapacityRespond.RespondResult1.GetPossibleCapacities())
            {
                Assert.IsTrue(processCapacity.Key.IsAssignableTo(typeof(DomainProductProcess)));
                Assert.AreEqual(30, processCapacity.Value);
            }
        }
        [TestMethod()]
        public void GetProductionCurrentCapacityContainerTest()
        {
            var cook = new Cook("Vasya");
            var getProductionCapacityRequest = EmptyApplicationRequest.Empty(cook);
            var getProductionCapacityRespond = _productionCapacitiesController
                                               .GetProductionCapacityContainer(getProductionCapacityRequest);
            foreach (var processCapacity in getProductionCapacityRespond.RespondResult1.GetCurrentCapacities())
            {
                Assert.IsTrue(processCapacity.Key.IsAssignableTo(typeof(DomainProductProcess)));
                Assert.AreEqual(30, processCapacity.Value);
            }
        }
    }
}