using Newtonsoft.Json;
using PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;
/// <summary>
/// Represents base custom json serializer
/// </summary>
public abstract class CustomJsonWorkerBase<T> : IEateryApplicationSerializer<T> where T : class
{
    /// <summary>
    /// Path to file with data
    /// </summary>
    protected string FilePath;
    /// <summary>
    /// Initializes new CustomJsonWorkerBase instance
    /// </summary>
    /// <param name="filePath">Path to file with data</param>
    /// <exception cref="ArgumentException">File path is null or white space</exception>
    protected CustomJsonWorkerBase(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"\"{nameof(filePath)}\" cannot be empty or white space", nameof(filePath));
        }
        FilePath = filePath;
    }
    /// <summary>
    /// Initializes new CustomJsonWorkerBase instance
    /// </summary>
    protected CustomJsonWorkerBase() { FilePath = string.Empty; }
    /// <summary>
    /// Path to file with data
    /// </summary>
    public string SavingPlacePath
    {
        get => FilePath;
        init => FilePath = value;
    }
    /// <summary>
    /// Helping converters to serialize abstract instances
    /// </summary>
    protected abstract JsonConverter[] HelpingConverters { get; }
    /// <summary>
    /// Deserializes instance from json file asynchronously
    /// </summary>
    /// <typeparam name="TConcrete">Concrete type of deserializing</typeparam>
    /// <returns>Instance of TConcrete or null</returns>
    public virtual async Task<TConcrete?> LoadAsync<TConcrete>() where TConcrete : T, new() =>
        JsonConvert.DeserializeObject<TConcrete>(await File.ReadAllTextAsync(FilePath),
                                                 new JsonSerializerSettings() { Converters = HelpingConverters });
    /// <summary>
    /// Saves instance into the json file asynchronously
    /// </summary>
    /// <param name="entity">Instance to save</param>
    public virtual async Task SaveAsync(T entity) =>
        await File.WriteAllTextAsync(FilePath, JsonConvert.SerializeObject(entity, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        }));
    /// <summary>
    /// Deserializes instance from json file synchronously
    /// </summary>
    /// <typeparam name="TConcrete">Concrete type of deserializing</typeparam>
    /// <returns>Instance of TConcrete or null</returns>
    public virtual TConcrete? Load<TConcrete>() where TConcrete : T, new() =>
        JsonConvert.DeserializeObject<TConcrete>(File.ReadAllText(FilePath),
                                                 new JsonSerializerSettings() { Converters = HelpingConverters });
    /// <summary>
    /// Saves instance into the json file synchronously
    /// </summary>
    /// <param name="entity">Instance to save</param>
    public virtual void Save(T entity) => 
        File.WriteAllText(FilePath, JsonConvert.SerializeObject(entity, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        }));
    /// <summary>
    /// Gets string representation of CustomJsonWorkerBase
    /// </summary>
    public override string ToString() => string.Format("{0} for type {1}", GetType().Name, typeof(T).Name);
}
