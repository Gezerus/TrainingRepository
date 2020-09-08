using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;
using School.Models;
using School.Repositories;

namespace SchoolTest
{
    [TestClass]
    public class TeacherRepositoryTest
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

            var teacher = new Teacher
            { Id = 1, Name = "Test" };
            // Act

            db.Teachers.Update(teacher);
            teacher = null;
            teacher = db.Teachers.GetById(1);
            // Assert
            Assert.IsTrue(teacher != null);
            Assert.AreEqual(teacher.Name, "Test");
        }
    }
}
