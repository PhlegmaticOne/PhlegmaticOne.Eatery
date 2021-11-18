namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;
/// <summary>
/// Represents contract for application data saver
/// </summary>
public interface IEateryApplicationSerializer<T> where T : class
{
    /// <summary>
    /// Path to file where data will be saved
    /// </summary>
    string SavingPlacePath { get; init; }
    /// <summary>
    /// Save data synchronously
    /// </summary>
    void Save(T entity);
    /// <summary>
    /// Save data asynchronously
    /// </summary>
    Task SaveAsync(T entity);
}
