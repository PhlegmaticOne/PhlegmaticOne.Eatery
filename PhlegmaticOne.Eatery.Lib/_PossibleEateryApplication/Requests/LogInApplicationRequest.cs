using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class LogInApplicationRequest : IApplicationRequest<string>
{
    private LogInApplicationRequest(string workerNameToLogIn)
    {
        if (string.IsNullOrWhiteSpace(workerNameToLogIn))
        {
            throw new ArgumentException($"\"{nameof(workerNameToLogIn)}\" cannot be empty or white space", nameof(workerNameToLogIn));
        }
        RequestData1 = workerNameToLogIn;
    }
    public Worker Worker { get; }
    public string RequestData1 { get; }
    public static LogInApplicationRequest Default(string workerNameToLogIn) =>
        new LogInApplicationRequest(workerNameToLogIn);
}
