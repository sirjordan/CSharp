    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
     
    public class DynamicStack<T>
    {
        private class Node<T>
        {
            //private Node<T> prevNode;
     
            public Node<T> PrevousNode { get; set; }
     
            public T Element { get; private set; }
     
            public Node(Node<T> prevNode, T element)
            {
                this.PrevousNode = prevNode;
                this.Element = element;
            }
     
            public Node()
            {
                this.PrevousNode = null;
            }
        }
     
        private Node<T> current;
     
        public DynamicStack()
        {
            this.current = new Node<T>();
        }
     
        /// <summary>
        /// Add element in the top of the stack.
        /// </summary>
        public void Push(T element)
        {
            current = new Node<T>(current, element);
        }
     
        /// <summary>
        /// Returns the top of the stack without remove it.
        /// </summary>
        public T Peek()
        {
            return this.current.Element;
        }
     
        /// <summary>
        /// Returns the top element in the stack and remove it.
        /// </summary>
        public T Pop()
        {
            var itemToReturn = this.current.Element;
            this.current = current.PrevousNode;
            return itemToReturn;
        }
    }

