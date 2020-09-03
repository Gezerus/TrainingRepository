using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;
using School.Models;
using School.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolTest
{
    [TestClass]
    public class StudentCreditRepositoryTest
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

            var studentCredit = new StudentCredit
            { Id = 1, CreditId = 1, StudentId = 1, Result = true};
            // Act

            db.StudentsCredits.Update(studentCredit);
            studentCredit = null;
            studentCredit = db.StudentsCredits.GetById(1);
            // Assert
            Assert.IsTrue(studentCredit != null);
            Assert.AreEqual(studentCredit.CreditId, 1);
            Assert.AreEqual(studentCredit.StudentId, 1);
            Assert.AreEqual(studentCredit.Result, true);
        }
    }
}
