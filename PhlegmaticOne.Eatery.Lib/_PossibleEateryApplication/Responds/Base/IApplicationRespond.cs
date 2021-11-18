namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents contract for application respond
/// </summary>
public interface IApplicationRespond<T>
{
    /// <summary>
    /// Result of respond
    /// </summary>
    T RespondResult1 { get; init; }
    /// <summary>
    /// Type of respond
    /// </summary>
    ApplicationRespondType RespondType { get; init; }
    /// <summary>
    /// Additional message of respond
    /// </summary>
    string Message { get; init; }
    /// <summary>
    /// Updates istance of respond with new properties values
    /// </summary>
    IApplicationRespond<T> Update(T newResult, ApplicationRespondType newRequestResultType, string message);
}
/// <summary>
/// Represents contract for application respond
/// </summary>
public interface IApplicationRespond<T1, T2> : IApplicationRespond<T1>
{
    /// <summary>
    /// Second result of respond 
    /// </summary>
    T2 RespondResult2 { get; init; }
    /// <summary>
    /// Updates istance of respond with new properties values
    /// </summary>
    IApplicationRespond<T1, T2> Update(T1 newResult1, T2 newResult2,
                                       ApplicationRespondType newRequestResultType,
                                       string message);
}