using Microsoft.VisualStudio.TestTools.UnitTesting;
using PhlegmaticOne.Eatery.Lib.EateryWorkers;
using System.Reflection;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication.Tests;

[TestClass()]
public class WorkersControllerTests
{
    private static WorkersController _workersController;
    [ClassInitialize]
    public static void ClassInitialize(TestContext testContext)
    {
        var eateryWorkersContainer = DefaultEateryWorkersContainer.GetDefaultWorkersBuilder()
                                 .AddCook(new Cook("Vasya"))
                                 .AddCook(new Cook("Vitya"))
                                 .AddManager(new Manager("Vitaliy"))
                                 .AddManager(new Manager("Vladimir"))
                                 .AddChief(new Chief("Vladilen"))
                                 .Build();

        ///Конструктор internal, поэтому создать объект простым вызовом конструктора нельзя
        var type = typeof(WorkersController);
        _workersController = type.Assembly.CreateInstance(
                             type.FullName, true, BindingFlags.Instance | BindingFlags.NonPublic,
                             null, new object[] { eateryWorkersContainer }, null, null) as WorkersController;
    }

    [TestMethod()]
    public void LogInSuccessfulCookTest()
    {
        var logInRequest = LogInApplicationRequest.Default("Vasya");
        var logInRespond = _workersController.LogIn(logInRequest);
        var worker = logInRespond.RespondResult1;
        Assert.AreEqual("Vasya", worker.Name);
        Assert.AreEqual(typeof(Cook), worker.GetType());
        Assert.AreEqual(LogInRespondType.LoggedIn, logInRespond.RespondResult2);
    }
    [TestMethod()]
    public void LogInSuccessfulManagerTest()
    {
        var logInRequest = LogInApplicationRequest.Default("Vitaliy");
        var logInRespond = _workersController.LogIn(logInRequest);
        var worker = logInRespond.RespondResult1;
        Assert.AreEqual("Vitaliy", worker.Name);
        Assert.AreEqual(typeof(Manager), worker.GetType());
        Assert.AreEqual(LogInRespondType.LoggedIn, logInRespond.RespondResult2);
    }
    [TestMethod()]
    public void LogInSuccessfulChiefTest()
    {
        var logInRequest = LogInApplicationRequest.Default("Vladilen");
        var logInRespond = _workersController.LogIn(logInRequest);
        var worker = logInRespond.RespondResult1;
        Assert.AreEqual("Vladilen", worker.Name);
        Assert.AreEqual(typeof(Chief), worker.GetType());
        Assert.AreEqual(LogInRespondType.LoggedIn, logInRespond.RespondResult2);
    }
    [TestMethod()]
    public void LogInUnsuccesfullTest()
    {
        var logInRequest = LogInApplicationRequest.Default("Anatoliy");
        var logInRespond = _workersController.LogIn(logInRequest);
        var worker = logInRespond.RespondResult1;
        Assert.IsNull(worker);
        Assert.AreEqual(LogInRespondType.UnknownWorker, logInRespond.RespondResult2);
    }
}
