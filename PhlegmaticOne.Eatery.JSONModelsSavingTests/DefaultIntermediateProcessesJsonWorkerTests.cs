using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.Helpers;
using PhlegmaticOne.Eatery.Lib.Ingredients;
using PhlegmaticOne.Eatery.Lib.IngredientsOperations;
using System;
using System.IO;
using System.Threading.Tasks;

namespace PhlegmaticOne.Eatery.JSONModelsSaving.Tests;

[TestClass()]
public class DefaultIntermediateProcessesJsonWorkerTests
{
    private const string DIRECTORY_WITH_TESTED_FILES_PATH = @"C:\Users\lolol\source\repos\PhlegmaticOne.Eatery\PhlegmaticOne.Eatery.JSONModelsSavingTests\TestFiles\";
    private const string WORKERS_FILE_NAME = @"intermediate_processes.json";
    private static DefaultIntermediateProcessesJsonWorker _intermediateProcessesJsonWorker;
    [ClassInitialize]
    public static void Initialize(TestContext testContext)
    {
        _intermediateProcessesJsonWorker = new(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME);
    }
    [TestMethod]
    public async Task ASaveIntermediateProcessesTest()
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

        await _intermediateProcessesJsonWorker.SaveAsync(intermediateProcessContainer);
        Assert.IsTrue(File.Exists(DIRECTORY_WITH_TESTED_FILES_PATH + WORKERS_FILE_NAME));
    }
    [TestMethod()]
    public async Task BLoadIntermadiateProcessesTest()
    {
        var intermediateProcesses = await _intermediateProcessesJsonWorker.LoadAsync<DefaultIntermediateProcessContainer>();
        Assert.IsInstanceOfType(intermediateProcesses, typeof(DefaultIntermediateProcessContainer));
    }
}
