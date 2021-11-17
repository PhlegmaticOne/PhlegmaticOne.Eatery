using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.Storages;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.JSONModelsSaving.Tests;

[TestClass()]
public class DefaultStoragesJsonWorkerTests
{
    private const string DIRECTORY_WITH_TESTED_FILES_PATH = @"C:\Users\lolol\source\repos\PhlegmaticOne.Eatery\PhlegmaticOne.Eatery.JSONModelsSavingTests\TestFiles\";
    private const string WORKERS_FILE_NAME = @"storages.json";
    private static DefaultStoragesJsonWorker _storagesJsonWorker;
    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
        _storagesJsonWorker = new(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME);
    }
    [TestMethod]
    public async Task ASaveStoragesTest()
    {
        //Было сделано для тестов. В приложении public конструктор не доступен
        //var storages = new List<Storage>()
        //{
        //    new Cellar(StorageLightning.Darkness, new StorageTemperature(-10, 20, 10), 
        //    new Dictionary<Type, double>()
        //    {
        //        { typeof(Cucumber), 1000 },
        //        { typeof(Tomato), 1000 },
        //        { typeof(Olive), 1000 },
        //        { typeof(OliveOil), 1000 },
        //    },
        //    new Dictionary<Type, double>()
        //    {
        //        { typeof(Cucumber), 300 },
        //        { typeof(Tomato), 300 },
        //        { typeof(Olive), 300 },
        //        { typeof(OliveOil), 300 },
        //    })
        //};

        //var storagesContainer = new DefaultStorageContainer(storages);
        //await _storagesJsonWorker.SaveAsync(storagesContainer);
        //Assert.IsTrue(File.Exists(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME));
    }
    [TestMethod()]
    public async Task BLoadStoragesTest()
    {
        var storages = await _storagesJsonWorker.LoadAsync<DefaultStorageContainer>();
        Assert.IsInstanceOfType(storages, typeof(DefaultStorageContainer));
    }
}
