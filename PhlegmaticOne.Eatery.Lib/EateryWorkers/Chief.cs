using Newtonsoft.Json;

namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public class Chief : Worker
{
    [JsonConstructor]
    public Chief(string name) : base(name)
    {
    }
}
