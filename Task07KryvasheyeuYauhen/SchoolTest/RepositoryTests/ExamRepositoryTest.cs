using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;
using School.Models;
using School.Repositories;
using System;

namespace SchoolTest
{
    [TestClass]
    public class ExamRepositoryTest
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

            var exam = new Exam
            { Id = 1, Date = DateTime.Now, SessionId = 1, TeacherId = 1, SubjectId = 1 };
            // Act

            db.Exams.Update(exam);
            exam = null;
            exam = db.Exams.GetById(1);
            // Assert
            Assert.IsTrue(exam != null);
            Assert.AreEqual(exam.SessionId, 1);
            Assert.AreEqual(exam.TeacherId, 1);
            Assert.AreEqual(exam.SubjectId, 1);
        }
    }
}
