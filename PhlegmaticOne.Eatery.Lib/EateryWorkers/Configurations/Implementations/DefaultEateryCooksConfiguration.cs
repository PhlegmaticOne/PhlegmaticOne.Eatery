namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;
/// <summary>
/// Represents default eatery cooks configuration
/// </summary>
public class DefaultEateryCooksConfiguration : IEateryCooksConfiguration
{
    private readonly List<Worker> _eateryWorkers = new();
    /// <summary>
    /// Adds new cook in configuring container
    /// </summary>
    public IEateryCooksConfiguration AddCook(Cook cook)
    {
        if (cook is not null)
        {
            _eateryWorkers.Add(cook);
        }
        return this;
    }
    /// <summary>
    /// Adds first manager in configuring container
    /// </summary>

    public IEateryManagersConfiguration AddManager(Manager manager)
    {
        if (manager is not null)
        {
            _eateryWorkers.Add(manager);
        }
        return new DefaultEateryManagersConfiguration(_eateryWorkers);
    }
    public override string ToString() => GetType().Name;
}
