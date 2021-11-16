using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public interface IApplicationRequest<T>
{
    Worker Worker { get; }
    T RequestData1 { get; }
}

public interface IApplicationRequest<T1, T2> : IApplicationRequest<T1>
{
    T2 RequestData2 { get; }
}
