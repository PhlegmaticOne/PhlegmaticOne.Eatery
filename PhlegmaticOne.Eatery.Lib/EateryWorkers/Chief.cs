using Newtonsoft.Json;

namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;
/// <summary>
/// Represents chief worker
/// </summary>
public class Chief : Worker
{
    /// <summary>
    /// Initializes new Chief instance 
    /// </summary>
    /// <param name="name">Specified chief name</param>
    [JsonConstructor]
    public Chief(string name) : base(name) { }

}
