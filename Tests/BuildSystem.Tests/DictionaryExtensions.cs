public static class DictionaryExtensions
{
    public static void AddRange<TKey, TValue>(
        this Dictionary<TKey, TValue> dic,
        IEnumerable<KeyValuePair<TKey, TValue>> collection
    )
    {
        foreach (var item in collection)
        {
            if (!dic.ContainsKey(item.Key))
            {
                dic.Add(item.Key, item.Value);
            }
        }
    }
}
