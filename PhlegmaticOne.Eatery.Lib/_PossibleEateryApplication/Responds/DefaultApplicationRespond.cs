namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class DefaultApplicationRespond<T> : IApplicationRespond<T>
{
    public DefaultApplicationRespond() { }
    public DefaultApplicationRespond(T result, ApplicationRespondType requestResultType, string message)
    {
        Result = result;
        RespondType = requestResultType;
        Message = message;
    }
    public T Result { get; init; }
    public ApplicationRespondType RespondType { get; init; }
    public string Message { get; init; }

    public IApplicationRespond<T> Update(T newResult, ApplicationRespondType newRespondType, string message) =>
        new DefaultApplicationRespond<T>(newResult, newRespondType, message);
}
