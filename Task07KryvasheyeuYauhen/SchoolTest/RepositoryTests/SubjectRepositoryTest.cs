using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;
using School.Models;
using School.Repositories;

namespace SchoolTest
{
    [TestClass]
    public class SubjectRepositoryTest
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

            var subject = new Subject
            { Id = 1, Name = "Test"};
            // Act

            db.Subjects.Update(subject);
            subject = null;
            subject = db.Subjects.GetById(1);
            // Assert
            Assert.IsTrue(subject != null);
            Assert.AreEqual(subject.Name, "Test");            
        }
    }
}
