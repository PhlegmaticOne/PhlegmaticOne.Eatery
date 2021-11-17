using System.Text.Json.Serialization;

namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;

public class Manager : Worker
{
    [JsonConstructor]
    public Manager(string name) : base(name)
    {
    }
}
