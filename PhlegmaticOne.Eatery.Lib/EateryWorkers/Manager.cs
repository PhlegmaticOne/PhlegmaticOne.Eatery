using System.Text.Json.Serialization;

namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;
/// <summary>
/// Represents manager worker
/// </summary>
public class Manager : Worker
{
    /// <summary>
    /// Initializes new Manager instance 
    /// </summary>
    /// <param name="name">Specified manager name</param>
    [JsonConstructor]
    public Manager(string name) : base(name) { }
}
