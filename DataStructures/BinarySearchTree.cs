    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
     
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        /// <summary>
        /// Represents a binary tree node
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private class BinaryTreeNode<T> : IComparable<BinaryTreeNode<T>> where T : IComparable<T>
        {
            // Contains the value of the node
            internal T value;
            // Contains the parent of the node
            internal BinaryTreeNode<T> parent;
            // Contains the left child of the node
            internal BinaryTreeNode<T> leftChild;
            // Contains the right child of the node
            internal BinaryTreeNode<T> rightChild;
     
            /// <summary>
            /// Constructs the tree node
            /// </summary>
            /// <param name="value">The value of the tree node</param>
            public BinaryTreeNode(T value)
            {
                this.value = value;
                this.parent = null;
                this.leftChild = null;
                this.rightChild = null;
            }
     
            public BinaryTreeNode<T>[] GetChilds()
            {
                BinaryTreeNode<T>[] childs = { leftChild, rightChild };
     
                return childs;
            }
     
            public override string ToString()
            {
                return this.value.ToString();
            }
     
            public override int GetHashCode()
            {
                return this.value.GetHashCode();
            }
     
            public override bool Equals(object obj)
            {
                BinaryTreeNode<T> other = (BinaryTreeNode<T>)obj;
     
                return this.CompareTo(other) == 0;
            }
     
            public int CompareTo(BinaryTreeNode<T> other)
            {
                return this.value.CompareTo(other.value);
            }
        }
     
        /// <summary>
        /// The root of the tree
        /// </summary>
        private BinaryTreeNode<T> root;
     
        /// <summary>
        /// Constructs the tree
        /// </summary>
        public BinarySearchTree()
        {
            this.root = null;
        }
     
        /// <summary>
        /// Inserts new value in the binary search tree
        /// </summary>
        /// <param name="value">the value to be inserted</param>
        public void Insert(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Cannot insert null value!");
            }
     
            this.root = Insert(value, null, root);
        }
     
        /// <summary>
        /// Inserts node in the binary search tree by given value
        /// </summary>
        /// <param name="value">the new value</param>
        /// <param name="parentNode">the parent of the new node</param>
        /// <param name="node">current node</param>
        /// <returns>the inserted node</returns>
        private BinaryTreeNode<T> Insert(T value, BinaryTreeNode<T> parentNode, BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                node = new BinaryTreeNode<T>(value);
                node.parent = parentNode;
            }
            else
            {
                int compareTo = value.CompareTo(node.value);
     
                if (compareTo < 0)
                {
                    node.leftChild = Insert(value, node, node.leftChild);
                }
                else if (compareTo > 0)
                {
                    node.rightChild = Insert(value, node, node.rightChild);
                }
            }
     
            return node;
        }
     
        /// <summary>
        /// Finds a given value in the tree and returns the node
        /// which contains it if such exsists
        /// </summary>
        /// <param name="value">the value to be found</param>
        /// <returns>the found node or null if not found</returns>
        private BinaryTreeNode<T> Find(T value)
        {
            BinaryTreeNode<T> node = this.root;
     
            while (node != null)
            {
                int compareTo = value.CompareTo(node.value);
     
                if (compareTo < 0)
                {
                    node = node.leftChild;
                }
                else if (compareTo > 0)
                {
                    node = node.rightChild;
                }
                else
                {
                    break;
                }
            }
     
            return node;
        }
     
        /// <summary>
        /// Counts the sum of all tree member if the are integeers.
        /// </summary>
        /// <param name="root"></param>
        /// <returns>The sum of Root child's value. If no integers in the tree returns -1</returns>
        public int SumOfIntegers()
        {
            try
            {
                return DFSCountIntegers(this.root);
            }
            catch (InvalidCastException)
            {
                return -1;
            }
        }
     
        /// <summary>
        /// Throw Exception If Tree not contains integers.
        /// </summary>
        private int DFSCountIntegers(BinaryTreeNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }
     
            int rootValue;
            bool isInt = int.TryParse(root.value.ToString(), out rootValue);
     
            if (isInt)
            {
                int counter = rootValue;
                BinaryTreeNode<T>[] childs = root.GetChilds();
     
                foreach (var child in childs)
                {
                    counter += DFSCountIntegers(child);
                }
     
                return counter;
            }
            else
            {
                throw new InvalidCastException("Can't sum objects if they are not integers");
            }
        }
     
        /// <summary>
        /// Count all edges of the tree.
        /// </summary>
        /// <returns>Returns the number of all edges in the tree</returns>
        public int CountEdges()
        {
            int result = CountEdgesDFS(this.root);
     
            return result;
        }
     
        private int CountEdgesDFS(BinaryTreeNode<T> root)
        {
            if (root == null)
            {
                return 0;
            }
     
            int counter = 1;
            BinaryTreeNode<T>[] childs = root.GetChilds();
     
            foreach (var child in childs)
            {
                counter += CountEdgesDFS(child);
            }
     
            return counter;
     
        }
     
        /// <summary>
        /// Leafs are members of the tree that has no childs.
        /// </summary>
        /// <returns>An Array of string representation of the members</returns>
        public string[] Leafs()
        {
            var res = GetLeafs(this.root).Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
     
            return res;
        }
     
        private string GetLeafs(BinaryTreeNode<T> root)
        {
            if (root == null)
            {
                return null;
            }
     
            StringBuilder result = new StringBuilder();
     
            BinaryTreeNode<T>[] children = root.GetChilds();
     
            foreach (var child in children)
            {
                if (child != null)
                {
                    result.Append(GetLeafs(child));
                    if (child.leftChild == null && child.rightChild == null)
                    {
                        result.Append(child.ToString() + ";");
                    }
                }
            }
     
            return result.ToString();
        }
     
        /// <summary>
        /// Return is givven item exists.
        /// </summary>
        public bool Contains(T item)
        {
            return Find(item) == null ? false : true;
        }
     
        /// <summary>
        /// Removes an element from the tree if exists
        /// </summary>
        /// <param name="value">the value to be deleted</param>
        public void Remove(T value)
        {
            BinaryTreeNode<T> nodeToDelete = Find(value);
            if (nodeToDelete == null)
            {
                return;
            }
     
            Remove(nodeToDelete);
        }
     
        private void Remove(BinaryTreeNode<T> node)
        {
            // Case 3: If the node has two children.
            // Note that if we get here at the end
            // the node will be with at most one child
            if (node.leftChild != null && node.rightChild != null)
            {
                BinaryTreeNode<T> replacement = node.rightChild;
                while (replacement.leftChild != null)
                {
                    replacement = replacement.leftChild;
                }
     
                node.value = replacement.value;
                node = replacement;
            }
     
            // Case 1 and 2: If the node has at most one child
            BinaryTreeNode<T> theChild = node.leftChild != null ? node.leftChild : node.rightChild;
     
            // If the element to be deleted has one child
            if (theChild != null)
            {
                theChild.parent = node.parent;
                // Handle the case when the element is the root
                if (node.parent == null)
                {
                    root = theChild;
                }
                else
                {
                    // Replace the element with its child subtree
                    if (node.parent.leftChild == node)
                    {
                        node.parent.leftChild = theChild;
                    }
                    else
                    {
                        node.parent.rightChild = theChild;
                    }
                }
            }
            else
            {
                // Handle the case when the element is the root
                if (node.parent == null)
                {
                    root = null;
                }
                else
                {
                    // Remove the element - it is a leaf
                    if (node.parent.leftChild == node)
                    {
                        node.parent.leftChild = null;
                    }
                    else
                    {
                        node.parent.rightChild = null;
                    }
                }
            }
        }
    }

