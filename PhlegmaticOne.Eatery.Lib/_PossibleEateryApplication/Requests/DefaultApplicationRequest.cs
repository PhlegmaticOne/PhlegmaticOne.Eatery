using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents default one parametrized request
/// </summary>
public class DefaultApplicationRequest<T> : IApplicationRequest<T>, IEquatable<DefaultApplicationRequest<T>>
{
    /// <summary>
    /// Initializes new DefaultApplicationRequest instance
    /// </summary>
    /// <param name="worker">Specified worker</param>
    /// <param name="requestData">Request data</param>
    public DefaultApplicationRequest(Worker worker, T requestData)
    {
        Worker = worker;
        RequestData1 = requestData;
    }
    public Worker Worker { get; }
    public T RequestData1 { get; }

    public bool Equals(DefaultApplicationRequest<T>? other) => other is not null &&
                                                               other.RequestData1.Equals(RequestData1) &&
                                                               other.Worker.Equals(Worker);

    public override bool Equals(object obj) => Equals(obj as DefaultApplicationRequest<T>);

    public override int GetHashCode() => RequestData1.GetHashCode() ^ Worker.GetHashCode();

    public override string? ToString() => string.Format("{0}. {1}", Worker, RequestData1);
}
/// <summary>
/// Represents default one parametrized request
/// </summary>
public class DefaultApplicationRequest<T1, T2> : IApplicationRequest<T1, T2>
{
    /// <summary>
    /// Initializes new DefaultApplicationRequest instance
    /// </summary>
    /// <param name="worker">Specified worker</param>
    /// <param name="requestData1">First request data</param>
    /// <param name="requestData2">Second request data</param>
    public DefaultApplicationRequest(Worker worker, T1 requestData1, T2 requestData2)
    {
        RequestData2 = requestData2;
        Worker = worker;
        RequestData1 = requestData1;
    }
    public T2 RequestData2 { get; }
    public Worker Worker { get; }
    public T1 RequestData1 { get; }
    public bool Equals(DefaultApplicationRequest<T1, T2>? other) => other is not null &&
                                                           other.RequestData1.Equals(RequestData1) &&
                                                           other.RequestData2.Equals(RequestData2) &&
                                                           other.Worker.Equals(Worker);
    public override bool Equals(object obj) => Equals(obj as DefaultApplicationRequest<T1, T2>);
    public override int GetHashCode() => RequestData1.GetHashCode() ^ Worker.GetHashCode();
    public override string? ToString() => string.Format("{0}. {1}. {2}", Worker, RequestData1, RequestData2);
}
