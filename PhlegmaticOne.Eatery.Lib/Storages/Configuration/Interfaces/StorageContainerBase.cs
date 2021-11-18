using System.Collections.ObjectModel;

namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents contract for storage containers
/// </summary>
public abstract class StoragesContainerBase
{
    /// <summary>
    /// Collection of all storages
    /// </summary>
    [Newtonsoft.Json.JsonProperty]
    protected readonly List<Storage> Storages;
    /// <summary>
    /// Initializes new StoragesContainerBase instance
    /// </summary>
    protected StoragesContainerBase() { Storages = new(); }
    /// <summary>
    /// Initializes new StoragesContainerBase instance
    /// </summary>
    /// <param name="storages">Specified storage collection</param>
    /// <exception cref="ArgumentNullException"></exception>
    [Newtonsoft.Json.JsonConstructor]
    internal StoragesContainerBase(IEnumerable<Storage> storages) => Storages = storages?.ToList() ??
                                                                     throw new ArgumentNullException(nameof(storages));
    /// <summary>
    /// Amount of storages in eatery
    /// </summary>
    public int Count => Storages.Count;
    /// <summary>
    /// Gets read-only collection of all storages in eatery
    /// </summary>
    /// <returns></returns>
    public virtual IReadOnlyCollection<Storage> AllStorages() => new ReadOnlyCollection<Storage>(Storages.ToList());
    /// <summary>
    /// Returns first fitted to a specified predicate storage or null
    /// </summary>
    public virtual Storage? FirstOrDefaultStorage(Func<Storage, bool> predicate) => Storages.FirstOrDefault(predicate);
    /// <summary>
    /// Adds new storage in current container instance
    /// </summary>
    /// <param name="storage"></param>
    internal virtual void Add(Storage storage)
    {
        if (storage is not null)
        {
            Storages.Add(storage);
        }
    }
    /// <summary>
    /// Tries to remove storage from current container instance
    /// </summary>
    /// <returns>True - storage was returned, false - it is not</returns>
    internal virtual bool TryRemove(Storage storage)
    {
        var preRemoveCount = Storages.Count;
        Storages.Remove(storage);
        return preRemoveCount > Storages.Count;
    }
    /// <summary>
    /// Gets all existing ingredients from all storages in current container
    /// </summary>
    /// <returns>Read-only dictionary in which keys are ingredient types, values - their total weight from all storages</returns>
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
    /// <summary>
    /// Gets collection of storages where specified ingredient type is contained
    /// </summary>
    internal virtual IEnumerable<Storage> GetStoragesContainingIngredientType(Type ingredientType) =>
        Storages.Where(storage => storage.ContainsIngredient(ingredientType));
    /// <summary>
    /// Gets string representation of default storage container
    /// </summary>
    public override string ToString() => string.Format("{0}. Count: {1}", GetType().Name, Storages.Count);
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
