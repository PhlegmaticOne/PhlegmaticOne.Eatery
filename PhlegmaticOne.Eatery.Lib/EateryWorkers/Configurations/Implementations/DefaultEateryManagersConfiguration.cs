namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public class DefaultEateryManagersConfiguration : IEateryManagersConfiguration
{
    private readonly IList<Worker> _eateryWorkers;

    internal DefaultEateryManagersConfiguration(IList<Worker> eateryWorkers)
    {
        _eateryWorkers = eateryWorkers;
    }
    public IEateryChiefConfiguration AddChief(Chief chief)
    {
        if (chief is not null)
        {
            _eateryWorkers.Add(chief);
        }
        return new DefaultEateryChiefConfiguration(_eateryWorkers);
    }

    public IEateryManagersConfiguration AddManager(Manager manager)
    {
        if (manager is not null)
        {
            _eateryWorkers.Add(manager);
        }
        return this;
    }
}
