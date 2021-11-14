using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class DefaultApplicationRequest<T> : IApplicationRequest<T>
{
    public DefaultApplicationRequest(Worker worker, T requestData)
    {
        Worker = worker;
        RequestData = requestData;
    }

    public Worker Worker { get; }
    public T RequestData { get; }
}
