using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents contract for one parametrized application request
/// </summary>
public interface IApplicationRequest<T>
{
    /// <summary>
    /// Worker which make request
    /// </summary>
    Worker Worker { get; }
    /// <summary>
    /// Data of request
    /// </summary>
    T RequestData1 { get; }
}
/// <summary>
/// Represents contract for two parametrized application request
/// </summary>
public interface IApplicationRequest<T1, T2> : IApplicationRequest<T1>
{
    /// <summary>
    /// Second data of request
    /// </summary>
    T2 RequestData2 { get; }
}
