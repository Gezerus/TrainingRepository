using System;


namespace BinaryTreeLibrary
{
    /// <summary>
    /// Describes a node of a binary tree
    /// </summary>
    /// <typeparam name="T">any type that implements IComparable</typeparam>
    
    public class TreeNode<T> : IComparable<T> where T : IComparable
    {
        private TreeNode<T> _leftNode;

        private TreeNode<T> _rightNode;

        /// <summary>
        /// reference to parent node
        /// </summary>
        public TreeNode<T> ParentNode { get; set; }

        /// <summary>
        /// Data of the node
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// reference to left child of the node
        /// </summary>
        public TreeNode<T> LeftNode
        {
            get
            {
                return _leftNode;
            }
            set
            {
                _leftNode = value;
                if(_leftNode != null)                
                    _leftNode.ParentNode = this;
                
            }
        }
        /// <summary>
        /// reference to right child of the node
        /// </summary>
        public TreeNode<T> RightNode
        {
            get
            {
                return _rightNode;
            }
            set
            {
                _rightNode = value;
                if (_rightNode != null)
                    _rightNode.ParentNode = this;

            }
        }

        /// <summary>
        /// the node height to the left
        /// </summary>
        public int LeftHeight
        {
            get
            {
                return MaxHeight(LeftNode);
            }
        }

        /// <summary>
        /// the node height to the right
        /// </summary>
        public int RightHeight
        {
            get
            {
                return MaxHeight(RightNode);
            }
        }

        public TreeNode()
        {

        }
        /// <summary>
        /// initializes a node
        /// </summary>
        /// <param name="data"></param>
        /// <param name="parentNode"></param>
        public TreeNode(T data, TreeNode<T> parentNode)
        {
            Data = data;
            ParentNode = parentNode;
        }

        /// <summary>
        /// calculates Height of a node
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private int MaxHeight(TreeNode<T> node)
        {
            if (node != null)
                return 1 + Math.Max(MaxHeight(node.LeftNode), MaxHeight(node.RightNode));
            return 0;
        }

        public int CompareTo(T other)
        {
            return Data.CompareTo(other);
        }
    }


}
