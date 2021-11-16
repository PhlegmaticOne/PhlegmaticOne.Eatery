namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public class DefaultEateryWorkersContainer : EateryWorkersContainerBase
{
    internal DefaultEateryWorkersContainer(IEnumerable<Worker> workers) : base(workers)
    {
    }
    public static IEateryCooksConfiguration GetDefaultWorkersBuilder() =>
        new DefaultEateryCooksConfiguration();
}
