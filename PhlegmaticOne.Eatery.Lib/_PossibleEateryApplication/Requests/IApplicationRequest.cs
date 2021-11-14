using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public interface IApplicationRequest<T>
{
    Worker Worker { get; }
    T RequestData { get; }
}
