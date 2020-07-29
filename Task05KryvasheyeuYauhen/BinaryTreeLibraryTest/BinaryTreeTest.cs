using System;
using BinaryTreeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BinaryTreeLibraryTest
{
    [TestClass]
    public class BinaryTreeTest
    {
        /// <summary>
        /// Tests method Add. 
        /// It should add data in a binary tree and balance tree when it is necessary
        /// </summary>
        [TestMethod]
        public void Add_ShouldAddAndBalanceTreeNodeCorrectly()
        {
            // Arrange
            BinaryTree<int> binaryTreeInt = new BinaryTree<int>();
            // Act
            binaryTreeInt.Add(1);
            binaryTreeInt.Add(3);
            binaryTreeInt.Add(4); // now the tree should balance:
            // 1                  3
            //  \                /  \
            //   3      to :    1    4 
            //    \
            //      4
            // Assert
            Assert.AreEqual(binaryTreeInt.Count, 3);
            Assert.AreEqual(binaryTreeInt.RootNode.Data, 3);
            Assert.AreEqual(binaryTreeInt.RootNode.LeftNode.Data, 1);
            Assert.AreEqual(binaryTreeInt.RootNode.RightNode.Data, 4);
        }

        /// <summary>
        /// Tests method FindNode when the binary tree contains the required node. 
        /// Method should return the required node.
        /// </summary>
        [TestMethod]
        public void FindNode_WhenTreeContainsRequiredNode_ShouldFindNode()
        {
            // Arrange
            BinaryTree<int> binaryTreeInt = new BinaryTree<int>();
            
            binaryTreeInt.Add(5);
            binaryTreeInt.Add(3);
            binaryTreeInt.Add(6);
            // Act
            TreeNode<int> result = binaryTreeInt.FindNode(6);
            // Assert
            Assert.AreEqual(result.Data, 6);
        }

        /// <summary>
        /// Tests method FindNode when the binary tree does not contains the required node. 
        /// Method should return null.
        /// </summary>
        [TestMethod]
        public void Find_WhenTreeDoesNotContainsRequiredNode_ShouldReturnNull()
        {
            // Arrange
            BinaryTree<int> binaryTreeInt = new BinaryTree<int>();

            binaryTreeInt.Add(5);
            binaryTreeInt.Add(3);
            binaryTreeInt.Add(6);
            // Act
            TreeNode<int> result = binaryTreeInt.FindNode(7);
            // Assert
            Assert.AreEqual(result, null);
        }

        /// <summary>
        /// Tests method Remove when tree contoins the node being removed.
        /// Method should return true and balance the tree if it is necessary.
        /// </summary>
        [TestMethod]
        public void Remove_WhenTreeContainsNodeBeingRemoved_ShouldRemoveNodeAndBalanceTreeAndREturnTrue()
        {
            // Arrange
            BinaryTree<int> binaryTreeInt = new BinaryTree<int>();

            binaryTreeInt.Add(5); //          5
            binaryTreeInt.Add(3); //         / \
            binaryTreeInt.Add(8); //        3   8
            binaryTreeInt.Add(7); //           /  
            // Act                            7   
            bool result = binaryTreeInt.Remove(3); // now the tree should balance:
            //  5                        7
            //   \                      / \
            //    8         to:        5   8
            //   /
            //  7    
            // Assert
            Assert.AreEqual(result, true);
            Assert.AreEqual(binaryTreeInt.Count, 3);
            Assert.AreEqual(binaryTreeInt.RootNode.Data, 7);
            Assert.AreEqual(binaryTreeInt.RootNode.LeftNode.Data, 5);
            Assert.AreEqual(binaryTreeInt.RootNode.RightNode.Data, 8);

        }

        /// <summary>
        /// Tests method Remove when the tree does not contains the node being removed.
        /// Method should return false.
        /// </summary>
        [TestMethod]
        public void Remove_WhenTreeDoesNotContainsNodeBeingRemoved_ShouldReturnFalse()
        {
            // Arrange
            BinaryTree<int> binaryTreeInt = new BinaryTree<int>();

            binaryTreeInt.Add(5); 
            binaryTreeInt.Add(3); 
            binaryTreeInt.Add(8); 
            binaryTreeInt.Add(7);  
            // Act                           
            bool result = binaryTreeInt.Remove(4);
            // Assert
            Assert.AreEqual(result, false);
            Assert.AreEqual(binaryTreeInt.Count, 4);
        }

        /// <summary>
        /// checks if the tree can store test results.
        /// </summary>
        [TestMethod]
        public void BinaryTree_CanContainsClassTest()
        {
            //Arrange
            BinaryTree<Test> binaryTree = new BinaryTree<Test>();

            var test1 = new Test()
            {
                ID = 1,
                StudentName = "Morgan Freeman",
                Name = "Interfaces",
                Date = new DateTime(2020, 7, 23),
                Grade = 9
            };

            var test2 = new Test()
            {
                ID = 2,
                StudentName = "Leonardo DiCaprio",
                Name = "Delegates",
                Date = new DateTime(2020, 7, 15),
                Grade = 7
            };

            var test3 = new Test()
            {
                ID = 3,
                StudentName = "Robert De Niro",
                Name = "Properties",
                Date = new DateTime(2020, 8, 1),
                Grade = 5
            };
            // Act
            binaryTree.Add(test1);
            binaryTree.Add(test2);
            binaryTree.Add(test3);
            // Assert
            Assert.AreEqual(binaryTree.Count, 3);
            Assert.AreEqual(binaryTree.RootNode.Data.ID, 2);
            Assert.AreEqual(binaryTree.RootNode.LeftNode.Data.Name, "Interfaces");
            Assert.AreEqual(binaryTree.RootNode.RightNode.Data.StudentName, "Robert De Niro");
        }
    }
}
