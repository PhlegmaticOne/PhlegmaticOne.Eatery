using Newtonsoft.Json;

namespace PhlegmaticOne.Eatery.JSONModelsSaving;

public abstract class CustomJsonWorkerBase<T> where T : class
{
    protected readonly string FilePath;
    protected CustomJsonWorkerBase(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
        {
            throw new ArgumentException($"\"{nameof(filePath)}\" cannot be empty or white space", nameof(filePath));
        }
        FilePath = filePath;
    }
    protected abstract JsonConverter[] HelpingConverters { get; }
    public virtual async Task<TConcrete> LoadAsync<TConcrete>() where TConcrete : T, new()
    {
        var json = await File.ReadAllTextAsync(FilePath);
        return JsonConvert.DeserializeObject<TConcrete>(json,
               new JsonSerializerSettings() { Converters = HelpingConverters });
    }

    public virtual async Task SaveAsync(T entity)
    {
        var json = JsonConvert.SerializeObject(entity, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        });
        await File.WriteAllTextAsync(FilePath, json);
    }

    public TConcrete Load<TConcrete>() where TConcrete : T, new()
    {
        var json = File.ReadAllText(FilePath);
        return JsonConvert.DeserializeObject<TConcrete>(json,
               new JsonSerializerSettings() { Converters = HelpingConverters });
    }

    public void Save(T entity)
    {
        var json = JsonConvert.SerializeObject(entity, new JsonSerializerSettings()
        {
            TypeNameHandling = TypeNameHandling.All,
            Formatting = Formatting.Indented,
        });
        File.WriteAllText(FilePath, json);
    }
}
