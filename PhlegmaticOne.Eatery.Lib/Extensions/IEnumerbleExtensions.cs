namespace PhlegmaticOne.Eatery.Lib.Extensions;

public static class IEnumerbleExtensions
{
    private readonly static Random _random = new Random();
    public static void Shuffle<T>(this IList<T> entities)
    {
        int count = entities.Count();
        while (count > 1)
        {
            int rand = _random.Next(count--);
            T temp = entities[count];
            entities[count] = entities[rand];
            entities[rand] = temp;
        }
    }
}
