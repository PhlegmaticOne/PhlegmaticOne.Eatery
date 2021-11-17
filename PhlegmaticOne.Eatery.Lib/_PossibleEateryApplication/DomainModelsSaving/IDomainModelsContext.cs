namespace PhlegmaticOne.Eatery.Lib._PossibleEateryApplication;

public interface IDomainModelsContext<T> where T : class
{
    Task Save(T entity);
    Task<T> Load();
}