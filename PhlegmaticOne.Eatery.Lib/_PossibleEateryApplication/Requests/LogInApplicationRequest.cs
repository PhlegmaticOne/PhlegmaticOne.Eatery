using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class LogInApplicationRequest : IApplicationRequest<string>, IEquatable<LogInApplicationRequest>
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
    /// <summary>
    /// Initializes new LogInApplicationRequest with specified worker name
    /// </summary>
    public static LogInApplicationRequest Default(string workerNameToLogIn) =>
        new LogInApplicationRequest(workerNameToLogIn);

    public bool Equals(LogInApplicationRequest? other) => other is not null &&
                                                          other.RequestData1.Equals(RequestData1) &&
                                                          other.Worker.Equals(Worker);

    public override bool Equals(object obj) => Equals(obj as LogInApplicationRequest);

    public override int GetHashCode() => RequestData1.GetHashCode() ^ Worker.GetHashCode();

    public override string? ToString() => string.Format("{0}: {1}", GetType().Name, RequestData1);
}
