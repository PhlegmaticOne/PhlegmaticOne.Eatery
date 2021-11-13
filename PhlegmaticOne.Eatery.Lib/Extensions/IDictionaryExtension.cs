namespace PhlegmaticOne.Eatery.Lib.Extensions
{
    public static class IDictionaryExtension
    {
        public static bool AllEquals<TKey, TValue>(this IDictionary<TKey, TValue> current, IDictionary<TKey, TValue> other)
        {
            if (other is null || current is null) return false;
            if (other.Count != current.Count) return false;
            foreach (var pair in current)
            {
                if (other.TryGetValue(pair.Key, out var value))
                {
                    if (value.Equals(pair.Value) == false)
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
    }
}
