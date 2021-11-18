using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents contract for storage containers
/// </summary>
public abstract class StoragesContainerBase
{
    protected StoragesContainerBase() { }
    [Newtonsoft.Json.JsonProperty]
    protected readonly List<Storage> Storages;
    public int Count => Storages.Count;
    [Newtonsoft.Json.JsonConstructor]
    internal StoragesContainerBase(IEnumerable<Storage> storages) =>
        Storages = storages.ToList() ?? throw new ArgumentNullException(nameof(storages));
    public virtual IReadOnlyCollection<Storage> AllStorages() => new ReadOnlyCollection<Storage>(Storages.ToList());
    public virtual Storage FirstOrDefaultStorage(Func<Storage, bool> predicate) => Storages.FirstOrDefault(predicate);
    internal virtual void Add(Storage storage)
    {
        if (storage is not null)
        {
            Storages.Add(storage);
        }
    }
    internal virtual bool TryRemove(Storage storage)
    {
        var preRemoveCount = Storages.Count;
        Storages.Remove(storage);
        return preRemoveCount > Storages.Count;
    }
    internal virtual IReadOnlyDictionary<Type, double> GetAllExistingIngredients()
    {
        var result = new Dictionary<Type, double>();
        foreach (var storage in AllStorages())
        {
            foreach (var ingredient in storage.KeepingIngredients)
            {
                if (result.TryGetValue(ingredient.Key, out double totalWeight))
                {
                    result[ingredient.Key] = totalWeight + ingredient.Value;
                }
                else
                {
                    result.Add(ingredient.Key, ingredient.Value);
                }
            }
        }
        return new ReadOnlyDictionary<Type, double>(result);
    }
    internal virtual IEnumerable<Storage> GetStoragesContainingIngredientType(Type ingredientType) =>
        Storages.Where(storage => storage.ContainsIngredient(ingredientType));
    /// <summary>
    /// Gets string representation of default storage container
    /// </summary>
    public override string ToString() => string.Format("Count: {0}", Storages.Count);
    /// <summary>
    /// Gets hash code of default storage container
    /// </summary>
    public override int GetHashCode() => Storages.GetHashCode();
    /// <summary>
    /// Checks equality of default storage container with other specified object 
    /// </summary>
    public override bool Equals(object? obj) => obj is DefaultStorageContainer defaultStorageContainer &&
                                                Storages.Except(defaultStorageContainer.Storages).Any() == false;
}
