using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class DefaultApplicationRequest<T> : IApplicationRequest<T>
{
    public DefaultApplicationRequest(Worker worker, T requestData)
    {
        Worker = worker;
        RequestData1 = requestData;
    }

    public Worker Worker { get; }
    public T RequestData1 { get; }
}

public class DefaultApplicationRequest<T1, T2> : IApplicationRequest<T1, T2>
{
    public DefaultApplicationRequest(Worker worker, T1 requestData1, T2 requestData2)
    {
        RequestData2 = requestData2;
        Worker = worker;
        RequestData1 = requestData1;
    }

    public T2 RequestData2 { get; }
    public Worker Worker { get; }
    public T1 RequestData1 { get; }
}
