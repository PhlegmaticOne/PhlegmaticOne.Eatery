namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents contract for application controllers container
/// </summary>
public interface IEateryApplicationControllersContainer
{
    /// <summary>
    /// Gets controller of specified type
    /// </summary>
    TController GetApplicationController<TController>() where TController : EateryApplicationControllerBase, new();
}
