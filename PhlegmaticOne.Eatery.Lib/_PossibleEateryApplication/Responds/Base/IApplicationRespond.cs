namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public interface IApplicationRespond<T>
{
    T RespondResult1 { get; init; }
    ApplicationRespondType RespondType { get; init; }
    string Message { get; init; }
    IApplicationRespond<T> Update(T newResult, ApplicationRespondType newRequestResultType, string message);
}

public interface IApplicationRespond<T1, T2> : IApplicationRespond<T1>
{
    T2 RespondResult2 { get; init; }
    IApplicationRespond<T1, T2> Update(T1 newResult1, T2 newResult2,
                                       ApplicationRespondType newRequestResultType,
                                       string message);
}