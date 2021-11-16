namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public interface IEateryCooksConfiguration
{
    IEateryCooksConfiguration AddCook(Cook cook);
    IEateryManagersConfiguration AddManager(Manager manager);
}
