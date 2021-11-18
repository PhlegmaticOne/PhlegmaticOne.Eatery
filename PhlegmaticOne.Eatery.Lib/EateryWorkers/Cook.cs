using Newtonsoft.Json;

namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;
/// <summary>
/// Represents cook worker
/// </summary>
public class Cook : Worker
{
    /// <summary>
    /// Initializes new Cook instance 
    /// </summary>
    /// <param name="name">Specified cook name</param>
    [JsonConstructor]
    public Cook(string name) : base(name) { }
}
