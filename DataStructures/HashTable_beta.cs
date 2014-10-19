    using System;
    using System.Collections.Generic;
    using System.Collections;
     
    /// <summary>
    /// Contains a collection of KeyValuePairs
    /// </summary>
    /// <typeparam name="K">Key for add/search</typeparam>
    /// <typeparam name="T">Value for add</typeparam>
    /// <remarks>Make better hash function.</remarks>
    public class HashTable<K, T> : IEnumerable<KeyValuePair<K, T>>
        where K : IComparable
    {
        const int INITIAL_CAPACITY = 16;
        const double LOAD_FACTOR = 0.75d;
        private List<KeyValuePair<K, T>>[] elements;
        private int elementsCount;
     
        public int Count
        {
            get { return this.elementsCount; }
        }
     
        public HashTable()
            : this(INITIAL_CAPACITY)
        {
        }
     
        private HashTable(int capacity)
        {
            this.elementsCount = 0;
            this.elements = new List<KeyValuePair<K, T>>[capacity];
        }
     
        public List<KeyValuePair<K, T>> Find(K key)
        {
            // index = hash(key) % capacity
            int index = GetIndex(key, this.elements.Length);
            if (this.elements[index] != null)
            {
                return this.elements[index];
            }
     
            throw new KeyNotFoundException(string.Format("Key <{0}> not found", key.ToString()));
        }
     
        public void Add(K key, T value)
        {
            // If not reches the load factor
            if (this.elementsCount + 1 < this.elements.Length * LOAD_FACTOR)
            {
                int index = GetIndex(key, this.elements.Length);
                if (this.elements[index] == null)
                {
                    this.elements[index] = new List<KeyValuePair<K, T>>();
                    // The elementCounter increases only if add a new List()
                    elementsCount++;
                }
     
                KeyValuePair<K, T> newValue = new KeyValuePair<K, T>(key, value);
                this.elements[index].Add(newValue);
            }
            else
            {
                // Make dual lengh the current capacity, copy all elements in the new, add the new element.
                Expand();
                Add(key, value);
            }
        }
     
        private void Expand()
        {
            int newCapacity = this.elements.Length * 2;
            List<KeyValuePair<K, T>>[] newContainer = new List<KeyValuePair<K, T>>[newCapacity];
            foreach (List<KeyValuePair<K, T>> element in this.elements)
            {
                if (element != null)
                {
                    // Get new hash key, based on the new array capacity
                    int newIndex = GetIndex(element[0].Key, newCapacity);
                    newContainer[newIndex] = element;
                }
            }
     
            this.elements = newContainer;
        }
     
        private int GetIndex(K key, int arrayCapacity)
        {
            // TODO:
            int hash = Math.Abs(key.GetHashCode());
            int index = hash % arrayCapacity;
            return index;
        }
     
        public IEnumerator<KeyValuePair<K, T>> GetEnumerator()
        {
            foreach (List<KeyValuePair<K, T>> listOfElements in this.elements)
            {
                if (listOfElements != null)
                {
                    foreach (KeyValuePair<K, T> entry in listOfElements)
                    {
                        yield return entry;
                    }
                }
            }
        }
     
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<K, T>>)this).GetEnumerator();
        }
    }

