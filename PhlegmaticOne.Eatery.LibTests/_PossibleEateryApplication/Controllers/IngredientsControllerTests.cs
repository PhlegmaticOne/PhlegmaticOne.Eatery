using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.Storages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication.Tests
{
    [TestClass()]
    public class IngredientsControllerTests
    {
        private static IngredientsController _ingredientsController;
        [ClassInitialize]
        public static void ClassInitialize(TestContext testContext)
        {
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
                });
                builder.InAmountOf(2);
            })
            .Build();

            ///Конструктор internal, поэтому создать объект простым вызовом конструктора нельзя
            var type = typeof(IngredientsController);
            _ingredientsController = type.Assembly.CreateInstance(
                                 type.FullName, true, BindingFlags.Instance | BindingFlags.NonPublic,
                                 null, new object[] { strorageContainer }, null, null) as IngredientsController;
        }
        [TestMethod()]
        public void GetAllExistingIngredientsTest()
        {
            var worker = new Chief("s");
            var getAllIngredientsRequest = EmptyApplicationRequest.Empty(worker);
            var getAllIngredientsRespond = _ingredientsController.GetAllExistingIngredients(getAllIngredientsRequest);
            var ingredients = getAllIngredientsRespond.RespondResult1;

            Assert.IsTrue(getAllIngredientsRespond.RespondType == ApplicationRespondType.Success);
            Assert.IsNotNull(ingredients);
            Assert.AreEqual(3, ingredients.Count);
            Assert.IsTrue(ingredients.All(i => i.Value == 0));
        }

        [TestMethod()]
        public void AddIngredientsInStorage_Successful_Test()
        {
            var worker = new Chief("s");
            var addIngredientsRequest = new DefaultApplicationRequest<Func<Storage, bool>, IEnumerable<Ingredient>>
                (worker, _testedStoragePredicate, _successfulIngredients);
            var addIngredientsRespond = _ingredientsController.AddIngredientsInStorage(addIngredientsRequest);
            Assert.IsTrue(addIngredientsRespond.RespondResult1);
            Assert.AreEqual(ApplicationRespondType.Success, addIngredientsRespond.RespondType);
            Assert.AreEqual("3 of 3 ingredients was added", addIngredientsRespond.Message);
        }
        [TestMethod()]
        public void AddIngredientsInStorage_Wrong_StorageNotContainingIngredientTypeTest()
        {
            var worker = new Chief("s");
            var addIngredientsRequest = new DefaultApplicationRequest<Func<Storage, bool>, IEnumerable<Ingredient>>
                (worker, _testedStoragePredicate, _notPossibleToContain);
            var addIngredientsRespond = _ingredientsController.AddIngredientsInStorage(addIngredientsRequest);
            Assert.IsFalse(addIngredientsRespond.RespondResult1);
            Assert.AreEqual(ApplicationRespondType.InternalError, addIngredientsRespond.RespondType);
            Assert.AreEqual("This container cannot keep OliveOil", addIngredientsRespond.Message);
        }
        [TestMethod()]
        public void AddIngredientsInStorage_Wrong_AddingNotAllIngredients_Test()
        {
            var worker = new Chief("s");
            var addIngredientsRequest = new DefaultApplicationRequest<Func<Storage, bool>, IEnumerable<Ingredient>>
                (worker, _testedStoragePredicate, _addingNotAllIngredients);
            var addIngredientsRespond = _ingredientsController.AddIngredientsInStorage(addIngredientsRequest);
            Assert.IsTrue(addIngredientsRespond.RespondResult1);
            Assert.AreEqual(ApplicationRespondType.Success, addIngredientsRespond.RespondType);
            Assert.AreEqual("4 of 6 ingredients was added", addIngredientsRespond.Message);
        }
        [TestMethod()]
        public void AddIngredientsInStorage_Wrong_NotContainsStorage_Test()
        {
            var worker = new Chief("s");
            var addIngredientsRequest = new DefaultApplicationRequest<Func<Storage, bool>, IEnumerable<Ingredient>>
                (worker, _testedWrongStoragePredicate, _addingNotAllIngredients);
            var addIngredientsRespond = _ingredientsController.AddIngredientsInStorage(addIngredientsRequest);
            Assert.IsFalse(addIngredientsRespond.RespondResult1);
            Assert.AreEqual(ApplicationRespondType.InternalError, addIngredientsRespond.RespondType);
            Assert.AreEqual("There are no such storages in eatery", addIngredientsRespond.Message);
        }
        private Func<Storage, bool> _testedStoragePredicate => storage => storage.Lightning == StorageLightning.Darkness;
        private Func<Storage, bool> _testedWrongStoragePredicate => storage => storage.Lightning == StorageLightning.Lamplight;
        private IEnumerable<Ingredient> _successfulIngredients => new List<Ingredient>()
        {
            new Cucumber(100, 100),
            new Tomato(100, 100),
            new Olive(500, 500)
        };
        private IEnumerable<Ingredient> _notPossibleToContain => new List<Ingredient>()
        {
            new OliveOil(100, 111),
        };
        private IEnumerable<Ingredient> _addingNotAllIngredients => new List<Ingredient>()
        {
            new Cucumber(220, 200),
            new Cucumber(220, 200),
            new Cucumber(220, 200),
            new Cucumber(220, 200),
            new Cucumber(220, 200),
            new Cucumber(220, 200),
        };
    }
}