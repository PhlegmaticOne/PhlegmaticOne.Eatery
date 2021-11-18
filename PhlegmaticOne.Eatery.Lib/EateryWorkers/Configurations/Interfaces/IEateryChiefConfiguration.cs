namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;
/// <summary>
/// Represents contract for configuring chief
/// </summary>
public interface IEateryChiefConfiguration
{
    /// <summary>
    /// Builds EateryWorkersContainerBase fron configuring workers
    /// </summary>
    EateryWorkersContainerBase Build();
}
