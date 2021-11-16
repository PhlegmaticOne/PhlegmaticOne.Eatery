using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class EmptyApplicationRequest : DefaultApplicationRequest<object>
{
    public EmptyApplicationRequest(Worker worker, object requestData) : base(worker, requestData)
    {
    }
    public static EmptyApplicationRequest Empty(Worker worker) =>
        new EmptyApplicationRequest(worker, null);
}
