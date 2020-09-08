using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;
using School.Models;
using School.Repositories;

namespace SchoolTest
{
    [TestClass]
    public class RepositoryTest
    {
        
        /// <summary>
        /// Test method GetAll. The method should return a collection of models from database
        /// </summary>
        [TestMethod]
        public void GetAll_ShouldReturnModelsFromDatabase()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();
            var db = new SchoolRepository();
            //Act
            var students = db.Students.GetAll();
            //Assert
            Assert.IsTrue(students != null);
            Assert.IsTrue(students.Count() > 0);
        }

        /// <summary>
        /// Test method GetById when there is the model in the database. The method should get the model from the database
        /// </summary>
        [TestMethod]
        public void GetById_WhenThereIsTheModelInDatabase_ShouldReturnModel()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();
            var db = new SchoolRepository();
            var student1 = db.Students.GetAll().Where(s => s.Id == 1).Single();
            // Act
            var student2 = db.Students.GetById(1);
            // Assert
            Assert.AreEqual(student1.Id, student2.Id);
            Assert.AreEqual(student1.Name, student2.Name);
            Assert.AreEqual(student1.Birthday, student2.Birthday);
            Assert.AreEqual(student1.Gender, student2.Gender);
            Assert.AreEqual(student1.GroupId, student2.GroupId);
        }


        /// <summary>
        /// Test method GetById whe there is not the model in the database. The method should throw an exception.
        /// </summary>
        [ExpectedException(typeof(InvalidOperationException))]
        [TestMethod]
        public void GetById_WhenThereIsNotTheModelInDatabase_ShouldThrowException()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();
            var db = new SchoolRepository();
            // Act
            db.Students.GetById(1000);            
        }


        /// <summary>
        /// Test method Insert. The method should insert the model to the school database.
        /// </summary>
        [TestMethod]
        public void Insert_ShouldAddMpdelToDatabaseCorrectly()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();
            var db = new SchoolRepository();

            var student = new Student
            { Name = "Kolya", Birthday = DateTime.Now, Gender = Gender.man, GroupId = 1 };
            // Act
            db.Students.Insert(student);

            student = null;
            student = db.Students.GetAll().Where(st => st.Name == "Kolya").FirstOrDefault();
            // Assert
            Assert.IsTrue(student != null);
            Assert.AreEqual(student.Name ,"Kolya");
            Assert.AreEqual(student.Gender, Gender.man);
            Assert.AreEqual(student.GroupId, 1);
        }

        /// <summary>
        /// Test method Delete. The method should delete a model from database
        /// </summary>
        [TestMethod]
        public void Delete_ShouldDeleteModelFromDatabase()
        {
            // Arrange
            ScriptRunner.DeleteSchoolDbIfExist();
            ScriptRunner.CreateSchoolDbIfNotExist();
            var db = new SchoolRepository();
            var student = new Student
            { Name = "Kolya", Birthday = DateTime.Now, Gender = Gender.man, GroupId = 1 };
            db.Students.Insert(student);
            // Act
            db.Students.Delete(student);
            student = db.Students.GetAll().Where(s => s.Name == "Kolya").FirstOrDefault();
            // Assert
            Assert.IsTrue(student == null);
        }
    }
}
