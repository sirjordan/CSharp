    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
     
    public class MultiValueDictionary<TKey, TVal>
        where TKey : IComparable
    {
        private IList<TVal> values;
        private Dictionary<TKey, IList<TVal>> dictionary;
     
        public MultiValueDictionary()
        {
            this.values = new List<TVal>();
            this.dictionary = new Dictionary<TKey, IList<TVal>>();
        }
     
        /// <summary>
        /// Add a multiple value to givven key.
        /// </summary>
        /// <param name="key">Key of the record.</param>
        /// <param name="singleValue">Value to be added at the list behind the key.</param>
        public void Add(TKey key, TVal singleValue)
        {
            if (this.dictionary.ContainsKey(key))
            {
                this.dictionary[key].Add(singleValue);
            }
            else
            {
                this.dictionary[key] = new List<TVal>();
                this.dictionary[key].Add(singleValue);
            }
        }
     
        /// <summary>
        /// List of values.
        /// </summary>
        public IList<TVal> GetValues(TKey key)
        {
            if (this.dictionary.ContainsKey(key))
            {
                return this.dictionary[key];
            }
            else
            {
                throw new ArgumentException(string.Format("Unexistence of key: {0}"), key.ToString());
            }
        }
    }

