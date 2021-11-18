namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public class DefaultEateryApplicationControllersContainer : IEateryApplicationControllersContainer
{
    private readonly Dictionary<Type, EateryApplicationControllerBase> _controllers;
    public DefaultEateryApplicationControllersContainer(IEnumerable<EateryApplicationControllerBase> eateryApplicationControllers)
    {
        _controllers = eateryApplicationControllers.ToDictionary(x => x.GetType());
    }
    public TController GetApplicationController<TController>() where TController : EateryApplicationControllerBase, new()
    {
        if (_controllers.TryGetValue(typeof(TController), out var controller))
        {
            return controller as TController;
        }
        return null;
    }
}
