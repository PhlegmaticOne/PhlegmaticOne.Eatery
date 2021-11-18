namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;
/// <summary>
/// Represents default eatery chief configuration
/// </summary>
public class DefaultEateryChiefConfiguration : IEateryChiefConfiguration
{
    private readonly IList<Worker> _eateryWorkers;
    /// <summary>
    /// Initializes new DefaultEateryChiefConfiguration instance
    /// </summary>
    /// <param name="eateryWorkers"></param>
    internal DefaultEateryChiefConfiguration(IList<Worker> eateryWorkers) => _eateryWorkers = eateryWorkers;
    /// <summary>
    /// Builds EateryWorkersContainerBase fron configuring workers
    /// </summary>
    public EateryWorkersContainerBase Build() => new DefaultEateryWorkersContainer(_eateryWorkers);
    public override string ToString() => GetType().Name;
}
