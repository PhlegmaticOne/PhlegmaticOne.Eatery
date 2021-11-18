namespace PhlegmaticOne.Eatery.Lib.Helpers.Attributes;
/// <summary>
/// Attribute for eatery workers roles
/// </summary>
public class EateryWorkerAttribute : Attribute
{
    /// <summary>
    /// Initializes new EateryWorkerAttribute instance
    /// </summary>
    /// <param name="typesWhoCanCallMethod">Types who can call specified method in controllers</param>
    public EateryWorkerAttribute(params Type[] typesWhoCanCallMethod) => 
        TypesWhoCanCallMethod = typesWhoCanCallMethod;
    /// <summary>
    /// Types who can call method
    /// </summary>
    public Type[] TypesWhoCanCallMethod { get; }
    public override string ToString() => string.Format("[{0}]", string.Join(',', TypesWhoCanCallMethod.Select(x => x.Name)));
}
