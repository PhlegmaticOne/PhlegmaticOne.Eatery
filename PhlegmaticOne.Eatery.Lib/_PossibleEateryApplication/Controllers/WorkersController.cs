using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class WorkersController : EateryApplicationControllerBase
{
    private readonly EateryWorkersContainerBase _eateryWorkersContainer;
    internal WorkersController(EateryWorkersContainerBase eateryWorkersContainer)
    {
        _eateryWorkersContainer = eateryWorkersContainer;
    }
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
