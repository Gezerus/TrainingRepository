using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeLibrary
{
    /// <summary>
    /// Describes binary tree
    /// </summary>
    /// <typeparam name="T"></typeparam>
    
    public class BinaryTree<T> where T: IComparable
    {
        /// <summary>
        /// Root of the tree
        /// </summary>
        public TreeNode<T> RootNode { get; set; }

        /// <summary>
        /// Number of nodes of the tree
        /// </summary>
        public int Count { get; set; }

        public BinaryTree()
        {

        }

        /// <summary>
        /// adds a node to the tree
        /// </summary>
        /// <param name="data"></param>
        public void Add(T data)
        {
            // if tree is empty, create a root 
            if (RootNode == null)
                RootNode = new TreeNode<T>(data, null);
            // if tree is not empty, find the place
            else
                AddNode(RootNode, data);
            Count++;
        }
        /// <summary>
        /// recursively adds a node to its place and balances the tree
        /// </summary>
        /// <param name="node"></param>
        /// <param name="data"></param>
        private void AddNode(TreeNode<T> node, T data)
        {
            if (data.CompareTo(node.Data) < 0)
            {
                if (node.LeftNode == null)
                    node.LeftNode = new TreeNode<T>(data, node);
                else
                    AddNode(node.LeftNode, data);
            }
            else
            {
                if (node.RightNode == null)
                    node.RightNode = new TreeNode<T>(data, node);
                else
                    AddNode(node.RightNode, data);
            }
            BalanceNode(node);
        }

        /// <summary>
        /// finds a node in a tree
        /// </summary>
        /// <param name="data"></param>
        /// <param name="currentNode"></param>
        /// <returns>result of searching or null</returns>
        public TreeNode<T> FindNode(T data, TreeNode<T> currentNode = null)
        {
            //start at the root of the tree
            if (currentNode == null)
                currentNode = RootNode;

            int result = data.CompareTo(currentNode.Data);

            if (result == 0)
                return currentNode;
            else if (result < 0)
            {
                if (currentNode.LeftNode == null)
                    return null;
                else
                    return FindNode(data, currentNode.LeftNode);
            }
            else
            {
                if (currentNode.RightNode == null)
                    return null;
                else
                    return FindNode(data, currentNode.RightNode);
            }
            
        }

        /// <summary>
        /// removes a node of the tree and balances the tree
        /// </summary>
        /// <param name="data"></param>
        /// <returns>true if the tree contains the node or false</returns>
        public bool Remove (T data)
        {
            //find node to remove
            TreeNode<T> currentNode = FindNode(data);

            if (currentNode == null)
                return false;
            //balance the node relative to the parent node
            TreeNode<T> balanceNode = currentNode.ParentNode;

            Count--;
            // if a node being removed doesn't have right child
            if(currentNode.RightNode == null)
            {
                if(currentNode.ParentNode == null) //node being removed is the root
                {
                    RootNode = currentNode.LeftNode;
                    if (RootNode != null)
                        RootNode.ParentNode = null; //clear the reference of the pareent node
                }
                else //node being removed is not the root
                {
                    int result = currentNode.ParentNode.CompareTo(currentNode.Data);

                    if (result >= 0)
                        currentNode.ParentNode.LeftNode = currentNode.LeftNode;
                    else 
                        currentNode.ParentNode.RightNode = currentNode.LeftNode;
                }
            }
            // if the right child of the node being removed doesn't have left child, the right child becomes the child of the parent node
            else if(currentNode.RightNode.LeftNode == null) // if the right child doesn't have the left child 
            {
                currentNode.RightNode.LeftNode = currentNode.LeftNode;

                if(currentNode.ParentNode == null) //node being removed is the root
                {
                    RootNode = currentNode.RightNode;

                    if (RootNode != null)
                        RootNode.ParentNode = null; //clear the reference of the pareent node
                }
                else //node being removed is not the root
                {
                    int result = currentNode.ParentNode.CompareTo(currentNode.Data);
                    if (result >= 0)
                        // if the value of the parent node is bigger than the value of current node,
                        // turn the right child of the node being removed to the right child of the parent
                        currentNode.ParentNode.LeftNode = currentNode.RightNode;
                    else
                        currentNode.ParentNode.RightNode = currentNode.RightNode;
                }
            }
            // if the right child of the node being removed has the left child,
            // swap the node being removed for the last left child for the right child 
            else
            {
                // find the last left child for the right child of the node being removed
                TreeNode<T> lastLeft = currentNode.RightNode.LeftNode;
                while (lastLeft.LeftNode != null)
                    lastLeft = lastLeft.LeftNode;

                lastLeft.ParentNode.LeftNode = lastLeft.RightNode;

                lastLeft.LeftNode = currentNode.LeftNode;
                lastLeft.RightNode = currentNode.RightNode;

                if(currentNode.ParentNode == null)
                {
                    RootNode = lastLeft;

                    if (RootNode != null)
                        RootNode.ParentNode = null;
                }
                else
                {
                    int result = currentNode.ParentNode.CompareTo(currentNode.Data);

                    if (result >= 0)
                        currentNode.ParentNode.LeftNode = lastLeft;
                    else
                        currentNode.ParentNode.RightNode = lastLeft;
                }
            }
            if (balanceNode != null)
                BalanceNode(balanceNode);
            else if (RootNode != null)
                BalanceNode(RootNode);
            return true;
        }
        /// <summary>
        /// Balances a node.
        /// </summary>
        /// <param name="node"></param>
        private void BalanceNode(TreeNode<T> node)
        {
            if((node.RightHeight - node.LeftHeight) > 1)
            {
                if (node.RightNode != null &&
                    (node.RightNode.RightHeight - node.RightNode.LeftHeight) < 0)
                {
                    RightRotation(node.RightNode);
                    LeftRotation(node);
                }
                else
                    LeftRotation(node);
            }
            else if((node.LeftHeight - node.RightHeight) > 1)
            {
                if (node.LeftNode != null &&
                    (node.RightNode.RightHeight - node.RightNode.LeftHeight) > 0)
                {
                    LeftRotation(node.LeftNode);
                    RightRotation(node);
                }

                else
                    RightRotation(node);
            }
        }

        /// <summary>
        /// makes right rotation of the tree
        /// </summary>
        /// <param name="node"></param>
        private void RightRotation(TreeNode<T> node)
        {
            var newRoot = node.LeftNode;
            if (node.ParentNode != null)
            {
                if (node.ParentNode.LeftNode == node)
                    node.ParentNode.LeftNode = newRoot;
                else if (node.ParentNode.RightNode == node)
                    node.ParentNode.RightNode = newRoot;
            }
            else
                RootNode = newRoot;
            newRoot.ParentNode = node.ParentNode;
            node.ParentNode = newRoot;
            node.LeftNode = newRoot.RightNode;
            newRoot.RightNode = node;
        }

        /// <summary>
        /// make left rotation of the tree
        /// </summary>
        /// <param name="node"></param>
        private void LeftRotation(TreeNode<T> node)
        {
            var newRoot = node.RightNode;
            if (node.ParentNode != null)
            {
                if (node.ParentNode.LeftNode == node)
                    node.ParentNode.LeftNode = newRoot;
                else if (node.ParentNode.RightNode == node)
                    node.ParentNode.RightNode = newRoot;
            }
            else
                RootNode = newRoot;
            newRoot.ParentNode = node.ParentNode;
            node.ParentNode = newRoot;

            node.RightNode = newRoot.LeftNode;
            newRoot.LeftNode = node;
        }

        public void Clear()
        {
            RootNode = null;
            Count = 0;
        }

    }
}
