using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Reprsents request which does not need any data
/// </summary>
public class EmptyApplicationRequest : DefaultApplicationRequest<object>
{
    private EmptyApplicationRequest(Worker worker, object requestData) : base(worker, requestData) { }
    /// <summary>
    /// Initializes new EmptyApplicationRequest with specified worker
    /// </summary>
    public static EmptyApplicationRequest Empty(Worker worker) =>
        new EmptyApplicationRequest(worker, null);
}
