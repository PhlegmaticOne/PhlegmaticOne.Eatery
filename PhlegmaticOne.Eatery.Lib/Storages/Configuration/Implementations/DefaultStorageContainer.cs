using System.Collections;

namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents prepared default storage container
/// </summary>
public class DefaultStorageContainer : IStorageContainer
{
    private readonly ICollection<Storage> _storages;

    public int Count => _storages.Count;
    public bool IsReadOnly => false;

    /// <summary>
    /// Initializes new default storage container
    /// </summary>
    /// <param name="storages">Enumarable of storages</param>
    /// <exception cref="ArgumentNullException">Storages is null</exception>
    public DefaultStorageContainer(IEnumerable<Storage> storages) => _storages = storages.ToList() ?? throw new ArgumentNullException(nameof(storages));
    /// <summary>
    /// Gets default storage container builder for default container
    /// </summary>
    public static IStorageContainerBuilder GetDefaultStorageContainerBuilder() =>
                  new DefaultStorageContainerBuilder();

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

    public TStorage FirstOrDefaultStorage<TStorage>(Func<TStorage, bool> predicate) where TStorage : Storage, new() =>
        OfStorageType<TStorage>().FirstOrDefault(predicate);

    public ICollection<TStorage> OfStorageType<TStorage>() where TStorage : Storage, new() =>
        _storages.OfType<TStorage>().ToList();

    public ICollection<Storage> AllStorages(Func<Storage, bool> predicate) => _storages.Where(predicate).ToList();

    public ICollection<Storage> AllStorages() => _storages.ToList();

    public void Add(Storage storage)
    {
        if(storage is not null)
        {
            _storages.Add(storage);
        }
    }

    public void Clear() => _storages.Clear();

    public bool Contains(Storage item) => _storages.Contains(item);

    public void CopyTo(Storage[] array, int arrayIndex) => _storages.CopyTo(array, arrayIndex);

    public bool Remove(Storage item) => _storages.Remove(item);

    public IEnumerator<Storage> GetEnumerator() => _storages.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}