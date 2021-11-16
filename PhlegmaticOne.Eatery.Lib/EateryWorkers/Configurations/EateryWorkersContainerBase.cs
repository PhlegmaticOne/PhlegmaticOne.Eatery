namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public abstract class EateryWorkersContainerBase
{
    private readonly Dictionary<string, Worker> _workers;
    internal EateryWorkersContainerBase(IEnumerable<Worker> workers)
    {
        _workers = workers.ToDictionary(k => k.Name);
    }
    internal Worker GetWorker(string name)
    {
        if(_workers.TryGetValue(name, out var worker))
        {
            return worker;
        }
        return null;
    }
}
