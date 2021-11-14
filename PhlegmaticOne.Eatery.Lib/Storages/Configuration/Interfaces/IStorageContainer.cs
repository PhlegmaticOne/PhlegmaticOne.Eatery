namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents contract for storage containers
/// </summary>
public interface IStorageContainer : ICollection<Storage>
{
    /// <summary>
    /// Returns first fitted to specified predicate storage instance
    /// </summary>
    /// <typeparam name="TStorage">Storage type</typeparam>
    /// <param name="predicate">Conditions of searching</param>
    TStorage FirstOrDefaultStorage<TStorage>(Func<TStorage, bool> predicate) where TStorage : Storage, new();
    /// <summary>
    /// Returns all storages of specified type
    /// </summary>
    /// <typeparam name="TStorage">Storage type</typeparam>
    ICollection<TStorage> OfStorageType<TStorage>() where TStorage : Storage, new();
    /// <summary>
    /// Returns all storages fitted to specified predicate
    /// </summary>
    /// <param name="predicate">Condition of searching</param>
    ICollection<Storage> AllStorages(Func<Storage, bool> predicate);
    /// <summary>
    /// Returns all registered storages
    /// </summary>
    ICollection<Storage> AllStorages();
}
