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
    public class CreditRepositoryTest
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

            var credit = new Credit
            { Id = 1, Date = DateTime.Now, SessionId = 1, TeacherId = 1, SubjectId = 1};
            // Act

            db.Credits.Update(credit);
            credit = null;
            credit = db.Credits.GetById(1);
            // Assert
            Assert.IsTrue(credit != null);
            Assert.AreEqual(credit.SessionId, 1);
            Assert.AreEqual(credit.TeacherId, 1);
            Assert.AreEqual(credit.SubjectId, 1);            
        }
    }
}
