namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;
/// <summary>
/// Represents contract for configuring managers
/// </summary>
public interface IEateryManagersConfiguration
{
    /// <summary>
    /// Adds other manager in configuring container 
    /// </summary>
    IEateryManagersConfiguration AddManager(Manager manager);
    /// <summary>
    /// Adds main chief in configuring container
    /// </summary>
    IEateryChiefConfiguration AddChief(Chief chief);
}
