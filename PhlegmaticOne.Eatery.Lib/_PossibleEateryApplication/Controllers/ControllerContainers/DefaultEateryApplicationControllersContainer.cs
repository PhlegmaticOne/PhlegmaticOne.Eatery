namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents default eatery application controllers container
/// </summary>
public class DefaultEateryApplicationControllersContainer : IEateryApplicationControllersContainer
{
    private readonly Dictionary<Type, EateryApplicationControllerBase> _controllers;
    /// <summary>
    /// Initialzies new DefaultEateryApplicationControllersContainer instance
    /// </summary>
    /// <param name="eateryApplicationControllers">Collection of application controllers</param>
    public DefaultEateryApplicationControllersContainer
        (IEnumerable<EateryApplicationControllerBase> eateryApplicationControllers) =>
        _controllers = eateryApplicationControllers.ToDictionary(x => x.GetType());
    public TController GetApplicationController<TController>() where TController : EateryApplicationControllerBase, new()
    {
        if (_controllers.TryGetValue(typeof(TController), out var controller))
        {
            return controller as TController;
        }
        return null;
    }
    public override string ToString() => GetType().Name;
}
