using System.Collections.Generic;

namespace Perficient.Infrastructure.Extensions
{
    public static class DictionaryExtensions
    {
        public static void AddIfNotNull<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key,
            TValue value)
        {
            if (key == null || value == null || EqualityComparer<TValue>.Default.Equals(value, default))
            {
                return;
            }

            dictionary.Add(key, value);
        }

        public static TValue GetValueOrDefault<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key)
        {
            if (dictionary == null || !dictionary.ContainsKey(key))
            {
                return default;
            }

            return dictionary.TryGetValue(key, out TValue value) 
                ? value 
                : default;
        }


        public static bool IsNotNullAndContainsKey<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key)
        {
            if (dictionary == null)
            {
                return false;
            }

            return dictionary.ContainsKey(key);
        }
    }
}
