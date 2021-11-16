namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents contract for storage container builders
/// </summary>
public interface IStorageContainerBuilder
{
    /// <summary>
    /// Registers new storages in container
    /// </summary>
    /// <typeparam name="TStorage">Storage type</typeparam>
    /// <typeparam name="TStorageBuilder">Storage builder type</typeparam>
    /// <param name="storageConfigurationAction">Storage builder configurating action</param>
    /// <returns>Instance of current container builder/returns>
    public IStorageContainerBuilder RegisterStorage<TStorage, TStorageBuilder>(
                                                                  Action<TStorageBuilder> storageConfigurationAction)
                                                                  where TStorage : Storage, new()
                                                                  where TStorageBuilder : IStorageBuilder<TStorage>, new();
    /// <summary>
    /// Builds new storage container from configuring data
    /// </summary>
    StoragesContainerBase Build();
}
