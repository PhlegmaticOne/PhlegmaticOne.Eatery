using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public abstract class EateryApplicationControllerBase
{
    protected virtual bool IsInRole(Worker worker, string methodName)
    {
        dynamic methodAttribute = GetType().GetMethod(methodName).GetCustomAttributes(false).First();
        IEnumerable<Type> types = methodAttribute.TypesWhoCanCallMethod;
        return types.Any(t => t == worker.GetType());
    }
}
