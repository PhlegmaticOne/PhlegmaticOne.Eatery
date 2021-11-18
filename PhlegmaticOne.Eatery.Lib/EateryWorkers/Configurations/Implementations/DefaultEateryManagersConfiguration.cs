namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;
/// <summary>
/// Represents default eatery managers configuration
/// </summary>
public class DefaultEateryManagersConfiguration : IEateryManagersConfiguration
{
    private readonly IList<Worker> _eateryWorkers;

    internal DefaultEateryManagersConfiguration(IList<Worker> eateryWorkers) => _eateryWorkers = eateryWorkers;
    /// <summary>
    /// Adds main chief in configuring container
    /// </summary>
    public IEateryChiefConfiguration AddChief(Chief chief)
    {
        if (chief is not null)
        {
            _eateryWorkers.Add(chief);
        }
        return new DefaultEateryChiefConfiguration(_eateryWorkers);
    }
    /// <summary>
    /// Adds other manager in configuring container 
    /// </summary>
    public IEateryManagersConfiguration AddManager(Manager manager)
    {
        if (manager is not null)
        {
            _eateryWorkers.Add(manager);
        }
        return this;
    }
    public override string ToString() => GetType().Name;
}
