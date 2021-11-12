using System.Collections;

namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents prepared default storage container
/// </summary>
public class DefaultStorageContainer : IStorageContainer, IEnumerable<Storage>
{
    private readonly IEnumerable<Storage> _storages;
    /// <summary>
    /// Initializes new default storage container
    /// </summary>
    /// <param name="storages">Enumarable of storages</param>
    /// <exception cref="ArgumentNullException">Storages is null</exception>
    public DefaultStorageContainer(IEnumerable<Storage> storages) => _storages = storages ?? throw new ArgumentNullException(nameof(storages));
    /// <summary>
    /// Gets amount of storages in default storage container
    /// </summary>
    public int Count => _storages.Count();
    /// <summary>
    /// Gets default storage container builder for default container
    /// </summary>
    public static IStorageContainerBuilder GetDefaultStorageContainerBuilder() =>
                  new DefaultStorageContainerBuilder();
    /// <summary>
    /// Returns all storages from default storage container fitted to specified predicate
    /// </summary>
    /// <param name="predicate">Condition of searching</param>
    public IEnumerable<Storage> AllStorages(Func<Storage, bool> predicate) => _storages.Where(predicate);
    /// <summary>
    /// Returns all registered storages in default storage container
    /// </summary>
    public IEnumerable<Storage> AllStorages() => _storages;
    /// <summary>
    /// Returns first fitted to specified predicate storage instance from default storage container
    /// </summary>
    /// <typeparam name="TStorage">Storage type</typeparam>
    /// <param name="predicate">Conditions of searching</param>
    public TStorage FirstOrDefaultStorage<TStorage>(Func<TStorage, bool> predicate) where TStorage : Storage, new() =>
        _storages.OfType<TStorage>().FirstOrDefault(predicate);

    /// <summary>
    /// Returns all storages of specified type from from default storage container
    /// </summary>
    /// <typeparam name="TStorage">Storage type</typeparam>
    public IEnumerable<TStorage> OfStorageType<TStorage>() where TStorage : Storage, new() =>
        _storages.OfType<TStorage>();
    /// <summary>
    /// Gets enumerator of from default storage container
    /// </summary>
    public IEnumerator<Storage> GetEnumerator() => _storages.GetEnumerator();
    /// <summary>
    /// Gets enumerator of from default storage container
    /// </summary>
    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)_storages).GetEnumerator();
    /// <summary>
    /// Gets string representation of default storage container
    /// </summary>
    public override string ToString() => string.Format("Count: {0}", _storages.Count());
    /// <summary>
    /// Gets hash code of default storage container
    /// </summary>
    public override int GetHashCode() => _storages.GetHashCode();
    /// <summary>
    /// Checks equality of default storage container with other specified object 
    /// </summary>
    public override bool Equals(object? obj) => obj is DefaultStorageContainer defaultStorageContainer &&
                                                _storages.Except(defaultStorageContainer._storages).Any() == false;
}

////////////////TO       DO /////////////////// ////////ADD//REMOVE//Update