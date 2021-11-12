namespace PhlegmaticOne.Eatery.Lib.Helpers;

public interface IRetrievingCollection<T> : ICollection<T>
{
    public T RetrieveFirstOrDefault(Func<T, bool> predicate);
    public IEnumerable<T> Retrieve(Func<T, bool> predicate);
    public IEnumerable<T> RetrieveAll();
}
