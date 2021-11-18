using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public abstract class EateryApplicationControllerBase
{
    protected EateryApplicationControllerBase()
    {

    }
    protected virtual bool IsInRole(Worker worker, string methodName)
    {
        dynamic methodAttribute = GetType().GetMethod(methodName).GetCustomAttributes(false).First();
        IEnumerable<Type> types = methodAttribute.TypesWhoCanCallMethod;
        return types.Any(t => t == worker.GetType());
    }
    protected virtual IApplicationRespond<T> GetDefaultAccessDeniedRespond<T>(Worker worker)
    {
        return new DefaultApplicationRespond<T>(default(T), ApplicationRespondType.AccessDenied,
                                                $"{worker.GetType().Name} has no rights to do this action");
    }
    protected virtual IApplicationRespond<T1, T2> GetDefaultAccessDeniedRespond<T1, T2>(Worker worker)
    {
        return new DefaultApplicationRespond<T1, T2>(default(T1), default(T2), ApplicationRespondType.AccessDenied,
                                                $"{worker.GetType().Name} has no rights to do this action");
    }
}
