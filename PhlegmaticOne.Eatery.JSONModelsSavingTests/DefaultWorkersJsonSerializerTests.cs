using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using System.IO;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.JSONModelsSaving.Tests;

[TestClass()]
public class DefaultWorkersJsonSerializerTests
{
    private const string DIRECTORY_WITH_TESTED_FILES_PATH = @"C:\Users\lolol\source\repos\PhlegmaticOne.Eatery\PhlegmaticOne.Eatery.JSONModelsSavingTests\TestFiles\";
    private const string WORKERS_FILE_NAME = @"workers.json";
    private static DefaultWorkersJsonWorker _workersJsonWorker;
    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
        _workersJsonWorker = new(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME);
    }
    [TestMethod]
    public async Task ASaveWorkersTest()
    {
        var eateryWorkersContainer = DefaultEateryWorkersContainer.GetDefaultWorkersBuilder()
                                     .AddCook(new Cook("Vasya"))
                                     .AddCook(new Cook("Vitya"))
                                     .AddManager(new Manager("Vitaliy"))
                                     .AddManager(new Manager("Vladimir"))
                                     .AddChief(new Chief("Vladilen"))
                                     .Build();

        await _workersJsonWorker.SaveAsync(eateryWorkersContainer);
        Assert.IsTrue(File.Exists(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME));
    }
    [TestMethod()]
    public async Task BLoadWorkersTest()
    {
        var workers = await _workersJsonWorker.LoadAsync<DefaultEateryWorkersContainer>();
        Assert.IsInstanceOfType(workers, typeof(DefaultEateryWorkersContainer));
    }
}
