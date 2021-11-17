using Newtonsoft.Json;

namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public abstract class Worker
{
    [JsonConstructor]
    protected Worker(string name)
    {
        Name = name;
    }
    [JsonProperty]
    public string Name { get; }
}
