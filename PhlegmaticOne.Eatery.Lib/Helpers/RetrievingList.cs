using System.Collections;

namespace PhlegmaticOne.Eatery.Lib.Helpers;

public class RetrievingList<T> : IRetrievingCollection<T>
{
    private readonly List<T> _list;
    public RetrievingList() => _list = new List<T>();
    public RetrievingList(IEnumerable<T> entities)
    {
        if (entities is null)
        {
            throw new ArgumentNullException(nameof(entities));
        }
        _list = entities.ToList();
    }
    public int Count => _list.Count;
    public bool IsReadOnly => false;
    public void Add(T entity)
    {
        if (entity is not null)
        {
            _list.Add(entity);
        }
    }
    public T RetrieveFirstOrDefault(Func<T, bool> predicate)
    {
        var finded = _list.FirstOrDefault(predicate);
        if (finded is not null)
        {
            _list.Remove(finded);
            return finded;
        }
        return default;
    }
    public IEnumerable<T> Retrieve(Func<T, bool> predicate)
    {
        var result = new List<T>(_list.Where(predicate));
        for (int i = 0; i < result.Count(); i++)
        {
            _list.Remove(result.ElementAt(i));
        }
        return result;
    }

    public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

    public void Clear() => _list.Clear();

    public bool Contains(T item) => _list.Contains(item);
    public void CopyTo(T[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

    public bool Remove(T item) => _list.Remove(item);

    public IEnumerable<T> RetrieveAll()
    {
        var result = new List<T>(_list);
        _list.Clear();
        return result;
    }
}
