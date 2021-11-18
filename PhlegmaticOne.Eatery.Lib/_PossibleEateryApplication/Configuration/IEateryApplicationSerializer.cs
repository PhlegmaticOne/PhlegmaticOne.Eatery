namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public interface IEateryApplicationSerializer<T> where T : class
{
    string SavingPlacePath { get; set; }
    void Save(T entity);
    Task SaveAsync(T entity);
}
