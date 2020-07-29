using BinaryTreeLibrary;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BinaryTreeLibraryTest
{
    [TestClass]
    public class BinaryTreeSerializerTest
    {
        [TestMethod]
        public void SerializeToXml_ShouldSerializeTreeCorrectly()
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
            BinaryTreeSerializer.SerializeToXml(binaryTree, @"..\..\Files\testTree.xml");
        }

        [TestMethod]
        public void DeserializeFromXml_ShouldDeserializeCorrectly()
        {
            //Arrange
            BinaryTree<Test> binaryTree = new BinaryTree<Test>();
            //act
            BinaryTreeSerializer.DeserializeFromXml(ref binaryTree, @"..\..\Files\testTree.xml");
            //Assert
            Assert.AreEqual(binaryTree.Count, 3);
            Assert.AreEqual(binaryTree.RootNode.Data.ID, 2);
            Assert.AreEqual(binaryTree.RootNode.LeftNode.Data.Name, "Interfaces");
            Assert.AreEqual(binaryTree.RootNode.RightNode.Data.StudentName, "Robert De Niro");
        }
    }
}
