using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;

namespace SchoolTest
{
    [TestClass]
    public class RepositoryTest
    {
        /// <summary>
        /// Test method Update when. The Method should update the model
        /// </summary>
        [TestMethod]
        public void Update_WhenCorrectModelProvided_SHouldUpdateModel()
        {
            // Arrange
            ScriptRunner.CreateSchoolDbIfNotExist();
            var schoolDb = new SchoolRepository();
            var studentBeforeChange = schoolDb.Students.FirstOrDefault();
            var tempStudent = new Student
            {
                Id = studentBeforeChange.Id,
                Name = "Evgen",
                Birthday = studentBeforeChange.Birthday,
                Gender = studentBeforeChange.Gender,
                GroupId = studentBeforeChange.GroupId
            };
            // Act
            var res = schoolDb.Update(tempStudent);
            // Assert
            var studentAfterChange = schoolDb.Students.FirstOrDefault();

            Assert.IsTrue(studentBeforeChange.Name != studentAfterChange.Name);
            Assert.AreEqual(studentAfterChange.Name, tempStudent.Name);
            Assert.AreEqual(res, true);
            // clean   
            schoolDb.Update(studentBeforeChange);
        }

        /// <summary>
        /// Test methods Insert. The method should add the model to database.
        /// Test method Delete. The method should delete the model that was added to the database
        /// </summary>
        [TestMethod]
        public void InsertAndDelete_WhenThereIsNOTTheModelInDatabase_SHouldInsertModel()
        {
            // Arrange
            ScriptRunner.CreateSchoolDbIfNotExist();
            var schoolDb = new SchoolRepository();            
            var tempStudent = new Student
            {
                Name = "TestStudent",
                Birthday = DateTime.Now,
                Gender = Gender.man,
                GroupId = 1
            };
            // Act
            var res1 = schoolDb.Insert(tempStudent);
            var student = schoolDb.Students.Where(st => st.Name == "TestStudent").FirstOrDefault();

            var res2 = schoolDb.Delete(student);
            var student2 = schoolDb.Students.Where(st => st.Name == "TestStudent").FirstOrDefault();
            // Assert
            Assert.IsTrue(student != null);
            Assert.IsTrue(student2 == null);
            Assert.AreEqual(res1, true);
            Assert.AreEqual(res2, true);
        }
    }
}
