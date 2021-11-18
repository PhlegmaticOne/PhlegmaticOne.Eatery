using Newtonsoft.Json;

namespace PhlegmaticOne.Eatery.Lib.EateryWorkers;
/// <summary>
/// Represents base class for other workers
/// </summary>
public abstract class Worker : IEquatable<Worker>
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
    public override bool Equals(object? obj) => Equals(obj as Worker);
    public bool Equals(Worker? other) => other is not null && other.GetType() == GetType() && other.Name == Name;
    override public int GetHashCode() => Name.GetHashCode() ^ GetType().GetHashCode();
    override public string ToString() => string.Format("{0}: {1}", GetType().Name, Name);
}
