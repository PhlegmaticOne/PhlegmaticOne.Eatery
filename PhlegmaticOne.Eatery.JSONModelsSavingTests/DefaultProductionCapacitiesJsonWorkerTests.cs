using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.EateryEquipment;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.JSONModelsSaving.Tests;

[TestClass()]
public class DefaultProductionCapacitiesJsonWorkerTests
{
    private const string DIRECTORY_WITH_TESTED_FILES_PATH = @"C:\Users\lolol\source\repos\PhlegmaticOne.Eatery\PhlegmaticOne.Eatery.JSONModelsSavingTests\TestFiles\";
    private const string WORKERS_FILE_NAME = @"production_capacities.json";
    private static DefaultProductionCapacitiesJsonWorker _ordersJsonWorker;

    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
        _ordersJsonWorker = new(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME);
    }
    [TestMethod]
    public async Task ASaveOrdersTest()
    {
        //Было сделано для тестов. В приложении public конструктор не доступен
        //var dictionaryOfTypes = new Dictionary<Type, int>()
        //{
        //    {typeof(CuttingProcess), 30 },
        //    {typeof(MixingProcess), 30 },
        //};
        //var capacitiesContainer = new DefaultProductionCapacityContainer(dictionaryOfTypes);
        //await _ordersJsonWorker.SaveAsync(capacitiesContainer);
        //Assert.IsTrue(File.Exists(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME));
    }
    [TestMethod()]
    public async Task BLoadOrdersTest()
    {
        var capacities = await _ordersJsonWorker.LoadAsync<DefaultProductionCapacityContainer>();
        Assert.IsInstanceOfType(capacities, typeof(DefaultProductionCapacityContainer));
    }
}
