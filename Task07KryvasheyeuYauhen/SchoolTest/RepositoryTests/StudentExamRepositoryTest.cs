using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;
using School.Models;
using School.Repositories;

namespace SchoolTest
{
    [TestClass]
    public class StudentExamRepositoryTest
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

            var studentExam = new StudentExam
            { Id = 1, ExamId= 1, StudentId = 1, Grade = 0 };
            // Act

            db.StudentsExams.Update(studentExam);
            studentExam = null;
            studentExam = db.StudentsExams.GetById(1);
            // Assert
            Assert.IsTrue(studentExam != null);
            Assert.AreEqual(studentExam.ExamId, 1);
            Assert.AreEqual(studentExam.StudentId, 1);
            Assert.AreEqual(studentExam.Grade, 0);
        }
    }
}
