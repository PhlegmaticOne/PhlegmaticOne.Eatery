namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public class DefaultEateryCooksConfiguration : IEateryCooksConfiguration
{
    private readonly List<Worker> _eateryWorkers = new();
    public IEateryCooksConfiguration AddCook(Cook cook)
    {
        if (cook is not null)
        {
            _eateryWorkers.Add(cook);
        }
        return this;
    }

    public IEateryManagersConfiguration AddManager(Manager manager)
    {
        if (manager is not null)
        {
            _eateryWorkers.Add(manager);
        }
        return new DefaultEateryManagersConfiguration(_eateryWorkers);
    }
}
