namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;
/// <summary>
/// Represents contract for configuring cooks
/// </summary>
public interface IEateryCooksConfiguration
{
    /// <summary>
    /// Adds new cook in configuring container
    /// </summary>
    IEateryCooksConfiguration AddCook(Cook cook);
    /// <summary>
    /// Adds first manager in configuring container
    /// </summary>
    IEateryManagersConfiguration AddManager(Manager manager);
}
