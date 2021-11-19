using Newtonsoft.Json;

namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;
/// <summary>
/// Represents base class for other workers
/// </summary>
public abstract class Worker
{
    /// <summary>
    /// Initializes new Worker instance
    /// </summary>
    /// <param name="name">Name of worker</param>
    [JsonConstructor]
    protected Worker(string name) => Name = name ?? throw new ArgumentNullException(nameof(name));
    /// <summary>
    /// Name of worker
    /// </summary>
    [JsonProperty]
    public string Name { get; }
    override public string ToString() => string.Format("{0}: {1}", GetType().Name, Name);
}
