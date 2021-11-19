using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public abstract class EateryWorkersContainerBase
{
    [JsonProperty]
    private readonly Dictionary<string, Worker> _workers;
    protected EateryWorkersContainerBase() { _workers = new(); }
    internal EateryWorkersContainerBase(IEnumerable<Worker> workers) => _workers = workers.ToDictionary(k => k.Name);
    [JsonConstructor]
    internal EateryWorkersContainerBase(Dictionary<string, Worker> workers) => _workers = workers;
    internal Worker GetWorker(string name)
    {
        if (_workers.TryGetValue(name, out var worker))
        {
            return worker;
        }
        return null;
    }
    public IReadOnlyDictionary<string, Worker> GetWorkers() => new ReadOnlyDictionary<string, Worker>(_workers);
    public override string ToString() => GetType().Name;
}
