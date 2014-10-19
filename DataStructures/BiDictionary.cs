    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
     
    class BiDictionary<Key1, Key2, T>
    {
        private Dictionary<Key1, T> FirstKeyDict { get;  set; }
     
        private Dictionary<Key2, T> SecondKeyDict { get;  set; }
     
        public BiDictionary()
        {
            this.FirstKeyDict = new Dictionary<Key1, T>();
            this.SecondKeyDict = new Dictionary<Key2, T>();
        }
     
        // By first key
        public T this[Key1 key]
        {
            get
            {
                if (this.FirstKeyDict.ContainsKey(key))
                {
                    return this.FirstKeyDict[key];
                }
                else
                {
                    throw new KeyNotFoundException("Invalid key indexer.");
                }
            }
     
            // If not contains key, the Dictionary<Key, Value> would throw an exeption
            set
            {
                // TODO: Verification
                this.FirstKeyDict[key] = value;
            }
        }
     
        // By second key
        public T this[Key2 key]
        {
            get
            {
                if (this.SecondKeyDict.ContainsKey(key))
                {
                    return this.SecondKeyDict[key];
                }
                else
                {
                    throw new KeyNotFoundException("Invalid key indexer.");
                }
            }
     
            // If not contains key, the Dictionary<Key, Value> would throw an exeption
            set
            {
                // TODO: Verification
                this.SecondKeyDict[key] = value;
            }
        }
     
        // By both first and second keys
        public T this[Key1 key1, Key2 key2]
        {
            get
            {
                if (this.FirstKeyDict.ContainsKey(key1))
                {
                    return this.FirstKeyDict[key1];
                }
     
                if (this.SecondKeyDict.ContainsKey(key2))
                {
                    return this.SecondKeyDict[key2];
                }
     
                else
                {
                    throw new KeyNotFoundException("Invalid key indexer.");
                }
            }
     
            set
            {
                // TODO: Verification
                this.FirstKeyDict[key1] = value;
                this.SecondKeyDict[key2] = value;
            }
        }
     
        // TODO:
        // Add(key1)
        // Add(key2)
        // Remove(key1)
        // Remove(key2)
    }

