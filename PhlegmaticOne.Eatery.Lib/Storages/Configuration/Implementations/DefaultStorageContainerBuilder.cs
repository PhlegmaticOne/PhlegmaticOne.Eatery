namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents prepared default storage container builder
/// </summary>
public class DefaultStorageContainerBuilder : IStorageContainerBuilder
{
    /// <summary>
    /// Configuring storages
    /// </summary>
    private readonly List<Storage> _storages;
    /// <summary>
    /// Initializes new default storage container builder
    /// </summary>
    public DefaultStorageContainerBuilder() => _storages = new();
    /// <summary>
    /// Registers new storages in default storage container
    /// </summary>
    /// <typeparam name="TStorage">Storage type</typeparam>
    /// <typeparam name="TStorageBuilder">Storage builder type</typeparam>
    /// <param name="storageConfigurationAction">Storage builder configurating action</param>
    /// <returns>Instance of current container builder/returns>
    public IStorageContainerBuilder RegisterStorage<TStorage, TStorageBuilder>(
                                                              Action<TStorageBuilder> storageConfigurationAction)
                                                              where TStorage : Storage, new()
                                                              where TStorageBuilder : IStorageBuilder<TStorage>, new()
    {
        var storageBuilder = new TStorageBuilder();
        storageConfigurationAction.Invoke(storageBuilder);
        _storages.AddRange(storageBuilder.Build());
        return this;
    }
    /// <summary>
    /// Builds new storage container from configuring data
    /// </summary>
    public StoragesContainerBase Build() => new DefaultStorageContainer(_storages);
    /// <summary>
    /// Gets string representation of default storage container builder 
    /// </summary>
    /// <returns></returns>
    public override string ToString() => string.Format("Default storage container builder");
}
