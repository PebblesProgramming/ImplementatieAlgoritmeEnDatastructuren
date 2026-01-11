using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAlgoritmen.HashTable
{
    public class HashEntry<K, V>
    {
        public K Key { get; set; }
        public V Value { get; set; }

        public HashEntry(K key, V value)
        {
            Key = key;
            Value = value;
        }
    }

    public class HashTable<K, V>
    {
        private readonly int _size;
        private readonly LinkedList<HashEntry<K, V>>[] _buckets;

        public HashTable(int size = 100)
        {
            _size = size;
            _buckets = new LinkedList<HashEntry<K, V>>[size];
        }

        // De hashing-functie: zet een Key om naar een index in de array
        private int GetHash(K key)
        {
            int hash = key.GetHashCode();
            return Math.Abs(hash % _size);
        }

        public void Put(K key, V value) // O(1) gemiddeld
        {
            int index = GetHash(key);
            if (_buckets[index] == null)
            {
                _buckets[index] = new LinkedList<HashEntry<K, V>>();
            }

            foreach (var entry in _buckets[index])
            {
                if (entry.Key.Equals(key))
                {
                    entry.Value = value; // Update als key al bestaat
                    return;
                }
            }

            _buckets[index].AddLast(new HashEntry<K, V>(key, value));
        }

        public V Get(K key) // O(1) gemiddeld
        {
            int index = GetHash(key);
            if (_buckets[index] != null)
            {
                foreach (var entry in _buckets[index])
                {
                    if (entry.Key.Equals(key)) return entry.Value;
                }
            }
            throw new KeyNotFoundException();
        }
    }
}
