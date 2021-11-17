using Newtonsoft.Json;

namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public class Cook : Worker
{
    [JsonConstructor]
    public Cook(string name) : base(name)
    {
    }
}
