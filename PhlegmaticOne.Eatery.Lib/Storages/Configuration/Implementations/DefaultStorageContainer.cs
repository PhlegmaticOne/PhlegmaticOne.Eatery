namespace PhlegmaticOne.Eatery.Lib.Storages;
/// <summary>
/// Represents prepared default storage container
/// </summary>
public class DefaultStorageContainer : StoragesContainerBase
{
    public DefaultStorageContainer(IEnumerable<Storage> storages) : base(storages) { }
    /// <summary>
    /// Initializes new default storage container
    /// </summary>
    /// <param name="storages">Enumarable of storages</param>
    /// <exception cref="ArgumentNullException">Storages is null</exception>
    /// <summary>
    /// Gets default storage container builder for default container
    /// </summary>
    public static IStorageContainerBuilder GetDefaultStorageContainerBuilder() =>
                  new DefaultStorageContainerBuilder();
}