using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents controller which is responsible for operating with workers
/// </summary>
public class WorkersController : EateryApplicationControllerBase
{
    private readonly EateryWorkersContainerBase _eateryWorkersContainer;
    /// <summary>
    /// Initializes new WorkersController instance
    /// </summary>
    public WorkersController() { }
    /// <summary>
    /// Initializes new WorkersController instance
    /// </summary>
    /// <param name="eateryWorkersContainer">Specified eatery workers containers</param>
    internal WorkersController(EateryWorkersContainerBase eateryWorkersContainer) => 
        _eateryWorkersContainer = eateryWorkersContainer;
    /// <summary>
    /// Logs in worker by its name
    /// </summary>
    /// <param name="logInRequest">Login request with worker</param>
    /// <returns>Respond with worker and login type</returns>
    public IApplicationRespond<Worker, LogInRespondType> LogIn(LogInApplicationRequest logInRequest)
    {
        var worker = _eateryWorkersContainer.GetWorker(logInRequest.RequestData1);
        var isLoggedIn = worker is not null;
        return isLoggedIn ? new DefaultApplicationRespond<Worker, LogInRespondType>
                            (worker, LogInRespondType.LoggedIn, ApplicationRespondType.Success, $"{worker?.Name} logged in") :
                            new DefaultApplicationRespond<Worker, LogInRespondType>
                            (worker, LogInRespondType.UnknownWorker, ApplicationRespondType.AccessDenied, $"{logInRequest} is unknown");
    }
}
public enum LogInRespondType
{
    LoggedIn,
    UnknownWorker
}
