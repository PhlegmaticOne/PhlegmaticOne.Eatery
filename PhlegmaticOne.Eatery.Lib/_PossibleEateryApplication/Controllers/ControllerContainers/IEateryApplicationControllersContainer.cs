namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public interface IEateryApplicationControllersContainer
{
    TController GetApplicationController<TController>() where TController : EateryApplicationControllerBase, new();
}
