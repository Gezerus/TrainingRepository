using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;
using School.Models;
using School.Repositories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTest
{
    [TestClass]
    public class StudentRepositoryTest
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

            var student = new Student
            { Id = 1, Name = "Kolya", Birthday = DateTime.Now, Gender = Gender.man, GroupId = 1 };
            // Act

            db.Students.Update(student);
            student = null;
            student = db.Students.GetById(1);
            // Assert
            Assert.IsTrue(student != null);
            Assert.AreEqual(student.Name, "Kolya");
            Assert.AreEqual(student.Gender, Gender.man);
            Assert.AreEqual(student.GroupId, 1);
            Assert.AreEqual(student.Id, 1);
        }
    }
}
