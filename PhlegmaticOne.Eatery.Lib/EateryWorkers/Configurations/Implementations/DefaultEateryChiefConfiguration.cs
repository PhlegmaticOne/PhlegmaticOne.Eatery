namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public class DefaultEateryChiefConfiguration : IEateryChiefConfiguration
{
    private readonly IList<Worker> _eateryWorkers;
    internal DefaultEateryChiefConfiguration(IList<Worker> eateryWorkers) => _eateryWorkers = eateryWorkers;
    public EateryWorkersContainerBase Build() => new DefaultEateryWorkersContainer(_eateryWorkers);
}
