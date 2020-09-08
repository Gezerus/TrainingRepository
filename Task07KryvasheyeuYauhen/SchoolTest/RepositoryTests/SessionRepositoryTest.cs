using Microsoft.VisualStudio.TestTools.UnitTesting;
using School;
using School.Models;
using School.Repositories;
using System;

namespace SchoolTest
{
    [TestClass]
    public class SessionRepositoryTest
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

            var session = new Session
            { Id = 1, Name = "Test", StartDate = DateTime.Now };
            // Act

            db.Sessions.Update(session);
            session = null;
            session = db.Sessions.GetById(1);
            // Assert
            Assert.IsTrue(session != null);
            Assert.AreEqual(session.Name, "Test");            
        }
    }
}
