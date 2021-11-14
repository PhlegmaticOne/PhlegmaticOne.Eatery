namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public interface IApplicationRespond<T>
{
    T Result { get; init; }
    ApplicationRespondType RespondType { get; init; }
    string Message { get; init; }
    IApplicationRespond<T> Update(T newResult, ApplicationRespondType newRequestResultType, string message);
}
