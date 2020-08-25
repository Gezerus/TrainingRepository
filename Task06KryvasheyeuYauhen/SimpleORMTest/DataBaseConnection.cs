using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleORM;

namespace SimpleORMTest
{
    [TestClass]
    public class DataBaseConnectionTest
    {
        [TestMethod]
        public void Constructor_WhenCorrectConnectionStringProvided_()
        {
            DataBaseConnection dbConnection = 
                new DataBaseConnection(@"Data Source=.\SQLEXPRESS01;Initial Catalog=School;Integrated Security=True");

            //var result = dbConnection.Query<QueryResult>("SELECT Groups.Name, MIN(Grade) AS Min, AVG(CAST(Grade AS FLOAT)) AS Avg, MAX(Grade) AS Max " +
            //    "FROM StudentsExams, Groups, Sessions, Exams, Students  " +
            //    "WHERE StudentsExams.ExamId = Exams.Id AND StudentsExams.StudentId = Students.Id " +
            //    "AND Exams.SessionId = Sessions.Id AND Students.GroupId = Groups.Id " +
            //    "AND Sessions.Id = 1 " +
            //    "GROUP BY Groups.Name");

            var result = dbConnection.Query<Student>("SELECT * FROM Students");

            result.ToString();
        }
    }
}
