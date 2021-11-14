namespace PhlegmaticOne.Eatery.Lib.Helpers.Attributes;

public class EateryWorkerAttribute : Attribute
{
    public EateryWorkerAttribute(params Type[] typesWhoCanCallMethod)
    {
        TypesWhoCanCallMethod = typesWhoCanCallMethod;
    }

    public Type[] TypesWhoCanCallMethod { get; }
}
