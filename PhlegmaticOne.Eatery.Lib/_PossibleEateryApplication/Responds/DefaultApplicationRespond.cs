namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class DefaultApplicationRespond<T> : IApplicationRespond<T>
{
    public DefaultApplicationRespond() { }
    public DefaultApplicationRespond(T result, ApplicationRespondType requestResultType, string message)
    {
        RespondResult1 = result;
        RespondType = requestResultType;
        Message = message;
    }
    public T RespondResult1 { get; init; }
    public ApplicationRespondType RespondType { get; init; }
    public string Message { get; init; }

    public IApplicationRespond<T> Update(T newResult, ApplicationRespondType newRespondType, string message) =>
        new DefaultApplicationRespond<T>(newResult, newRespondType, message);
}

public class DefaultApplicationRespond<T1, T2> : IApplicationRespond<T1, T2>
{
    public DefaultApplicationRespond()
    {

    }
    public DefaultApplicationRespond(T1 respondResult1, T2 respondResult2,
                                    ApplicationRespondType respondType, string message)
    {
        RespondResult2 = respondResult2;
        RespondResult1 = respondResult1;
        RespondType = respondType;
        Message = message;
    }

    public T2 RespondResult2 { get; init; }
    public T1 RespondResult1 { get; init; }
    public ApplicationRespondType RespondType { get; init; }
    public string Message { get; init; }

    public IApplicationRespond<T1, T2> Update(T1 newResult1, T2 newResult2, ApplicationRespondType newRequestResultType, string message)
    {
        return new DefaultApplicationRespond<T1, T2>(newResult1, newResult2, newRequestResultType, message);
    }

    public IApplicationRespond<T1> Update(T1 newResult, ApplicationRespondType newRequestResultType, string message)
    {
        return Update(newResult, RespondResult2, newRequestResultType, message);
    }
}
