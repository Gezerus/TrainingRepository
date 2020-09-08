using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;
using School.Models;
using School.Repositories;

namespace SchoolTest
{
    [TestClass]
    public class GroupRepositoryTest
    {
        /// <summary>
        /// Test method Update. The method should update a model in the database
        /// </summary>
        [TestMethod]
        public void Update_ShouldUpdateModelCorrectly()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();

            var db = new SchoolRepository();

            var group = new Group
            { Id = 1, Name = "Test", Specialty = "Test"};
            // Act

            db.Groups.Update(group);
            group = null;
            group = db.Groups.GetById(1);
            // Assert
            Assert.IsTrue(group != null);
            Assert.AreEqual(group.Name, "Test");
            Assert.AreEqual(group.Specialty, "Test");            
        }
    }
}
