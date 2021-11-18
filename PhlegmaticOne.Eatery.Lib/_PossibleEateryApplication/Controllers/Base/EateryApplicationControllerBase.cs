using PhlegmaticOne.Eatery.Lib.EateryWorkers;

namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents base controller for other application controllers
/// </summary>
public abstract class EateryApplicationControllerBase
{
    /// <summary>
    /// Initializes new EateryApplicationControllerBase instance
    /// </summary>
    protected EateryApplicationControllerBase() { }
    /// <summary>
    /// Check if worker that calls controller method can use this method
    /// </summary>
    /// <param name="worker">Specified worker calls mehtod of controller</param>
    /// <param name="methodName">Controller method name</param>
    /// <returns></returns>
    protected virtual bool IsInRole(Worker worker, string methodName)
    {
        dynamic methodAttribute = GetType().GetMethod(methodName).GetCustomAttributes(false).First();
        IEnumerable<Type> types = methodAttribute.TypesWhoCanCallMethod;
        return types.Any(t => t == worker.GetType());
    }
    /// <summary>
    /// Gets default one parametrized application respond for situation when access to method denied
    /// </summary>
    /// <param name="worker">Worker who cannot call controller method</param>
    protected virtual IApplicationRespond<T> GetDefaultAccessDeniedRespond<T>(Worker worker)
    {
        return new DefaultApplicationRespond<T>(default(T), ApplicationRespondType.AccessDenied,
                                                $"{worker.GetType().Name} has no rights to do this action");
    }
    /// <summary>
    /// Gets default two parametrized application respond for situation when access to method denied
    /// </summary>
    /// <param name="worker">Worker who cannot call controller method</param>
    protected virtual IApplicationRespond<T1, T2> GetDefaultAccessDeniedRespond<T1, T2>(Worker worker)
    {
        return new DefaultApplicationRespond<T1, T2>(default(T1), default(T2), ApplicationRespondType.AccessDenied,
                                                $"{worker.GetType().Name} has no rights to do this action");
    }
    public override string ToString() => GetType().Name;
}
