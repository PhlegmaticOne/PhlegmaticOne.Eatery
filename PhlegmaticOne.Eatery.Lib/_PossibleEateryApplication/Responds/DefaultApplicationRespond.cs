namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents default one parametrized application respond
/// </summary>
public class DefaultApplicationRespond<T> : IApplicationRespond<T>, IEquatable<DefaultApplicationRespond<T>>
{
    /// <summary>
    /// Initializes new DefaultApplicationRespond instance
    /// </summary>
    public DefaultApplicationRespond() { }
    /// <summary>
    /// Initializes new DefaultApplicationRespond instance
    /// </summary>
    /// <param name="result">Specified respond result</param>
    /// <param name="requestResultType">Specified request result type</param>
    /// <param name="message">Specified respond message</param>
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
    public bool Equals(DefaultApplicationRespond<T>? other) => other is not null &&
                                                           other.RespondResult1.Equals(RespondResult1) &&
                                                           other.Message.Equals(Message) &&
                                                           other.RespondType == RespondType;
    public override bool Equals(object obj) => Equals(obj as DefaultApplicationRespond<T>);

    public override int GetHashCode() => RespondType.GetHashCode() ^ Message.GetHashCode() ^ (int)RespondType;

    public override string? ToString() => string.Format("{0}. {1}", RespondType, RespondResult1);
}
/// <summary>
/// Represents default two parametrized application respond
/// </summary>
public class DefaultApplicationRespond<T1, T2> : IApplicationRespond<T1, T2>, IEquatable<DefaultApplicationRespond<T1,T2>>
{
    /// <summary>
    /// Initializes new DefaultApplicationRespond instance
    /// </summary>
    public DefaultApplicationRespond() { }
    /// <summary>
    /// Initializes new DefaultApplicationRespond instance
    /// </summary>
    /// <param name="respondResult1">First specified respond result</param>
    /// <param name="respondResult2">Second specified respond result</param>
    /// <param name="respondType">Specified request result type</param>
    /// <param name="message">Specified respond message</param>
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
    public IApplicationRespond<T1, T2> Update(T1 newResult1, T2 newResult2,
                                             ApplicationRespondType newRequestResultType, string message) =>
        new DefaultApplicationRespond<T1, T2>(newResult1, newResult2, newRequestResultType, message);

    public IApplicationRespond<T1> Update(T1 newResult, ApplicationRespondType newRequestResultType, string message) =>
        Update(newResult, RespondResult2, newRequestResultType, message);
    public bool Equals(DefaultApplicationRespond<T1, T2>? other) => other is not null &&
                                                           other.RespondResult1.Equals(RespondResult1) &&
                                                           other.RespondResult2.Equals(RespondResult2) &&
                                                           other.Message.Equals(Message) &&
                                                           other.RespondType == RespondType;
    public override bool Equals(object obj) => Equals(obj as DefaultApplicationRespond<T1, T2>);

    public override int GetHashCode() => RespondType.GetHashCode() ^ Message.GetHashCode() ^ (int)RespondType;

    public override string? ToString() => string.Format("{0}. {1}. {2}", RespondType, RespondResult1, RespondResult2);
}
