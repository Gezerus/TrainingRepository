using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;
using School.Repository;

namespace SchoolTest
{
    [TestClass]
    public class RepositoryTest
    {
        
        /// <summary>
        /// Test method GetAll. Method should return correct data from database
        /// </summary>
        [TestMethod]
        public void GetAll_ShouldReturnCorrectData()
        {
            // Arrange
            ScriptRunner.CreateSchoolDataBase();

            var studentRep = new StudentRepository();
            // Act
            var students = studentRep.GetAll();
            // Assert
            Assert.AreEqual(students.Count(), 12);
        }

        /// <summary>
        /// Test method Save when there is the model in database. Method should update the model
        /// </summary>
        [TestMethod]
        public void Save_WhenThereIsTheModelInDatabase_SHouldUpdateModel()
        {
            // Arrange
            ScriptRunner.CreateSchoolDataBase();
            var studentRep = new StudentRepository();
            var studentBeforeChange = studentRep.GetAll().FirstOrDefault();
            var tempStudent = new Student
            {
                Id = studentBeforeChange.Id,
                Name = "Evgen",
                Birthday = studentBeforeChange.Birthday,
                Gender = studentBeforeChange.Gender,
                GroupId = studentBeforeChange.GroupId
            };
            // Act
            var res = studentRep.Save(tempStudent);
            // Assert
            var studentAfterChange = studentRep.GetAll().FirstOrDefault();

            Assert.IsTrue(studentBeforeChange.Name != studentAfterChange.Name);
            Assert.AreEqual(studentAfterChange.Name, tempStudent.Name);
            Assert.AreEqual(res, true);
            // clean   
            studentRep.Save(studentBeforeChange);
        }

        /// <summary>
        /// Test methods Save when there is not the model in the database. The method should add the model to database.
        /// Test method Delete. The method should delete the model that was added to the database
        /// </summary>
        [TestMethod]
        public void SaveAndDelete_WhenThereIsNOTTheModelInDatabase_SHouldInsertModel()
        {
            // Arrange
            ScriptRunner.CreateSchoolDataBase();
            var studentRep = new StudentRepository();            
            var tempStudent = new Student
            {
                Name = "TestStudent",
                Birthday = DateTime.Now,
                Gender = Gender.man,
                GroupId = 1
            };
            // Act
            var res1 = studentRep.Save(tempStudent);
            var student = studentRep.GetAll().Where(st => st.Name == "TestStudent").FirstOrDefault();

            var res2 = studentRep.Delete(student);
            var student2 = studentRep.GetAll().Where(st => st.Name == "TestStudent").FirstOrDefault();
            // Assert
            Assert.IsTrue(student != null);
            Assert.IsTrue(student2 == null);
            Assert.AreEqual(res1, true);
            Assert.AreEqual(res2, true);
        }
    }
}
