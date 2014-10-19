	

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
     
    class PriorityQueue<T>
    {
        private SortedDictionary<int, T> items;
        private int highestPriorityIndex;
        private int lowestPriorityIndex;
     
        public PriorityQueue()
        {
            this.items = new SortedDictionary<int, T>();
            highestPriorityIndex = 0;
            lowestPriorityIndex = int.MaxValue;
        }
     
        public void InsertWithHighestPriority(T item)
        {
            if (this.items.Count > 0)
            {
                //int highestPriorityIndex = this.items.Keys.Last();
                this.highestPriorityIndex++;
                this.items.Add(highestPriorityIndex, item);
            }
            else
            {
                this.items.Add(0, item);
            }
        }
     
        public void Insert(T item, int priority)
        {
            if (this.items.ContainsKey(priority))
            {
                throw new InvalidOperationException(string.Format("There's already item with {0} priority.", priority));
            }
     
            if (priority > this.highestPriorityIndex)
            {
                this.highestPriorityIndex = priority;
            }
     
            if (priority < this.lowestPriorityIndex)
            {
                this.lowestPriorityIndex = priority;
            }
     
            this.items.Add(priority, item);
        }
     
        public T PeekHighestPriorityItem()
        {
            if (this.items.Count > 0)
            {
                //int highestPriorityIndex = this.items.Keys.Last();
                return this.items[highestPriorityIndex];
            }
            else
            {
                throw new InvalidOperationException("Priority queue is empty.");
            }
        }
     
        public T PopHighestPriorityItem()
        {
            if (this.items.Count > 0)
            {
                T highPriorityItem = this.items[highestPriorityIndex];
                this.items.Remove(this.highestPriorityIndex);
                this.highestPriorityIndex--;
     
                return highPriorityItem;
               
            }
            else
            {
                throw new InvalidOperationException("Priority queue is empty.");
            }
        }
     
        public T PopLowestPriorityItem()
        {
            if (this.items.Count > 0)
            {
                T lowestPriorityItem = this.items[lowestPriorityIndex];
                this.items.Remove(lowestPriorityIndex);
                this.lowestPriorityIndex++;
     
                return lowestPriorityItem;
            }
            else
            {
                throw new InvalidOperationException("Priority queue is empty.");
            }
        }
     
    }

