﻿/*
Copyright (c) 2014 <a href="http://www.gutgames.com">James Craig</a>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.*/

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Utilities.DataTypes
{
    /// <summary>
    /// Binary tree
    /// </summary>
    /// <typeparam name="T">The type held by the nodes</typeparam>
    public class BinaryTree<T> : ICollection<T>
        where T : IComparable<T>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="root">Root of the binary tree</param>
        public BinaryTree(TreeNode<T> root = null)
        {
            if (Root == null)
            {
                NumberOfNodes = 0;
                return;
            }
            Root = root;
            NumberOfNodes = Traversal(Root).Count();
        }

        /// <summary>
        /// Number of items in the tree
        /// </summary>
        public virtual int Count
        {
            get { return NumberOfNodes; }
        }

        /// <summary>
        /// Is the tree empty
        /// </summary>
        public virtual bool IsEmpty { get { return Root == null; } }

        /// <summary>
        /// Is this read only?
        /// </summary>
        public virtual bool IsReadOnly
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the maximum value of the tree
        /// </summary>
        public virtual T MaxValue
        {
            get
            {
                Contract.Requires<InvalidOperationException>(!IsEmpty, "The tree is empty");
                Contract.Requires<NullReferenceException>(Root != null, "Root");
                TreeNode<T> TempNode = Root;
                while (TempNode.Right != null)
                    TempNode = TempNode.Right;
                return TempNode.Value;
            }
        }

        /// <summary>
        /// Gets the minimum value of the tree
        /// </summary>
        public virtual T MinValue
        {
            get
            {
                Contract.Requires<InvalidOperationException>(!IsEmpty, "The tree is empty");
                Contract.Requires<NullReferenceException>(Root != null, "Root");
                TreeNode<T> TempNode = Root;
                while (TempNode.Left != null)
                    TempNode = TempNode.Left;
                return TempNode.Value;
            }
        }

        /// <summary>
        /// The root value
        /// </summary>
        public virtual TreeNode<T> Root { get; set; }

        /// <summary>
        /// The number of nodes in the tree
        /// </summary>
        protected virtual int NumberOfNodes { get; set; }

        /// <summary>
        /// Converts the object to a string
        /// </summary>
        /// <param name="Value">Value to convert</param>
        /// <returns>The value as a string</returns>
        public static implicit operator string (BinaryTree<T> Value)
        {
            Contract.Requires<ArgumentNullException>(Value != null, "Value");
            return Value.ToString();
        }

        /// <summary>
        /// Adds an item to a binary tree
        /// </summary>
        /// <param name="item">Item to add</param>
        public virtual void Add(T item)
        {
            if (Root == null)
            {
                Root = new TreeNode<T>(item);
                ++NumberOfNodes;
            }
            else
            {
                Insert(item);
            }
        }

        /// <summary>
        /// Clears all items from the tree
        /// </summary>
        public virtual void Clear()
        {
            Root = null;
            NumberOfNodes = 0;
        }

        /// <summary>
        /// Determines if the tree contains an item
        /// </summary>
        /// <param name="item">Item to check</param>
        /// <returns>True if it is, false otherwise</returns>
        public virtual bool Contains(T item)
        {
            if (IsEmpty)
                return false;

            TreeNode<T> TempNode = Root;
            while (TempNode != null)
            {
                int ComparedValue = TempNode.Value.CompareTo(item);
                if (ComparedValue == 0)
                    return true;
                else
                    TempNode = ComparedValue < 0 ? TempNode.Left : TempNode.Right;
            }
            return false;
        }

        /// <summary>
        /// Copies the tree to an array
        /// </summary>
        /// <param name="array">Array to copy to</param>
        /// <param name="arrayIndex">Index to start at</param>
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            T[] TempArray = new T[NumberOfNodes];
            int Counter = 0;
            foreach (T Value in this)
            {
                TempArray[Counter] = Value;
                ++Counter;
            }
            Array.Copy(TempArray, 0, array, arrayIndex, NumberOfNodes);
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        public virtual IEnumerator<T> GetEnumerator()
        {
            foreach (TreeNode<T> TempNode in Traversal(Root))
            {
                yield return TempNode.Value;
            }
        }

        /// <summary>
        /// Removes an item from the tree
        /// </summary>
        /// <param name="item">Item to remove</param>
        /// <returns>True if it is removed, false otherwise</returns>
        public virtual bool Remove(T item)
        {
            TreeNode<T> Item = Find(item);
            if (Item == null)
                return false;
            --NumberOfNodes;
            var Values = new List<T>();
            foreach (TreeNode<T> TempNode in Traversal(Item.Left))
                Values.Add(TempNode.Value);
            foreach (TreeNode<T> TempNode in Traversal(Item.Right))
                Values.Add(TempNode.Value);
            if (Item.Parent != null)
            {
                if (Item.Parent.Left == Item)
                    Item.Parent.Left = null;
                else
                    Item.Parent.Right = null;
                Item.Parent = null;
            }
            else
            {
                Root = null;
            }
            foreach (T Value in Values)
                Add(Value);
            return true;
        }

        /// <summary>
        /// Gets the enumerator
        /// </summary>
        /// <returns>The enumerator</returns>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (TreeNode<T> TempNode in Traversal(Root))
            {
                yield return TempNode.Value;
            }
        }

        /// <summary>
        /// Outputs the tree as a string
        /// </summary>
        /// <returns>The string representation of the tree</returns>
        public override string ToString()
        {
            return this.ToString(x => x.ToString(), " ");
        }

        /// <summary>
        /// Finds a specific object
        /// </summary>
        /// <param name="item">The item to find</param>
        /// <returns>The node if it is found</returns>
        protected virtual TreeNode<T> Find(T item)
        {
            foreach (TreeNode<T> Item in Traversal(Root))
                if (Item.Value.Equals(item))
                    return Item;
            return null;
        }

        /// <summary>
        /// Inserts a value
        /// </summary>
        /// <param name="item">item to insert</param>
        protected virtual void Insert(T item)
        {
            Contract.Requires<NullReferenceException>(Root != null, "Root");
            TreeNode<T> TempNode = Root;
            bool Found = false;
            while (!Found)
            {
                int ComparedValue = TempNode.Value.CompareTo(item);
                if (ComparedValue > 0)
                {
                    if (TempNode.Left == null)
                    {
                        TempNode.Left = new TreeNode<T>(item, TempNode);
                        ++NumberOfNodes;
                        return;
                    }
                    else
                    {
                        TempNode = TempNode.Left;
                    }
                }
                else if (ComparedValue < 0)
                {
                    if (TempNode.Right == null)
                    {
                        TempNode.Right = new TreeNode<T>(item, TempNode);
                        ++NumberOfNodes;
                        return;
                    }
                    else
                    {
                        TempNode = TempNode.Right;
                    }
                }
                else
                {
                    TempNode = TempNode.Right;
                }
            }
        }

        /// <summary>
        /// Traverses the list
        /// </summary>
        /// <param name="Node">The node to start the search from</param>
        /// <returns>The individual items from the tree</returns>
        protected virtual IEnumerable<TreeNode<T>> Traversal(TreeNode<T> Node)
        {
            if (Node != null)
            {
                if (Node.Left != null)
                {
                    foreach (TreeNode<T> LeftNode in Traversal(Node.Left))
                        yield return LeftNode;
                }
                yield return Node;
                if (Node.Right != null)
                {
                    foreach (TreeNode<T> RightNode in Traversal(Node.Right))
                        yield return RightNode;
                }
            }
        }
    }

    /// <summary>
    /// Node class for the Binary tree
    /// </summary>
    /// <typeparam name="T">The value type</typeparam>
    public class TreeNode<T>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="value">Value of the node</param>
        /// <param name="parent">Parent node</param>
        /// <param name="left">Left node</param>
        /// <param name="right">Right node</param>
        public TreeNode(T value = default(T), TreeNode<T> parent = null, TreeNode<T> left = null, TreeNode<T> right = null)
        {
            Value = value;
            Right = right;
            Left = left;
            Parent = parent;
        }

        /// <summary>
        /// Is this a leaf
        /// </summary>
        public virtual bool IsLeaf { get { return Left == null && Right == null; } }

        /// <summary>
        /// Is this the root
        /// </summary>
        public virtual bool IsRoot { get { return Parent == null; } }

        /// <summary>
        /// Left node
        /// </summary>
        public virtual TreeNode<T> Left { get; set; }

        /// <summary>
        /// Parent node
        /// </summary>
        public virtual TreeNode<T> Parent { get; set; }

        /// <summary>
        /// Right node
        /// </summary>
        public virtual TreeNode<T> Right { get; set; }

        /// <summary>
        /// Value of the node
        /// </summary>
        public virtual T Value { get; set; }

        /// <summary>
        /// Visited?
        /// </summary>
        internal bool Visited { get; set; }

        /// <summary>
        /// Returns the node as a string
        /// </summary>
        /// <returns>String representation of the node</returns>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}