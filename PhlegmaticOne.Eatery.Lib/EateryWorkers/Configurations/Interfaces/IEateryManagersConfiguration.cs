namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public interface IEateryManagersConfiguration
{
    IEateryManagersConfiguration AddManager(Manager manager);
    IEateryChiefConfiguration AddChief(Chief chief);
}
