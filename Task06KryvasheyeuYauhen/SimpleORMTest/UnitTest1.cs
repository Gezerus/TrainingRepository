using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleORM;

namespace SimpleORMTest
{    
    [TestClass]
    public class UnitTest1
    {
        private void CreateDataBase()
        {
            string sqlConnectionString = @"Data Source=(local);Initial Catalog=master;Integrated Security=True";

            FileInfo file = new FileInfo(@"..\..\Scripts\School\SchoolScript.sql");
            string script = file.OpenText().ReadToEnd();

            SqlConnection conn = new SqlConnection(sqlConnectionString);

            Server server = new Server(new ServerConnection(conn));

            server.ConnectionContext.ExecuteNonQuery(script);
            conn.Close();
        }

        private void DeleteDataBase()
        {
            string sqlConnectionString = @"Data Source=(local);Initial Catalog=master;Integrated Security=True";
            string script = "ALTER DATABASE School SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE School";
            SqlConnection conn = new SqlConnection(sqlConnectionString);

            Server server = new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery(script);
        }
        /// <summary>
        /// Test Query method when model and database table are correct. Method should return a collection of persons
        /// </summary>
        [TestMethod]
        public void Query_WhenModelMatchTheDataBaseTable_Shouldmapcorrectly()
        {
            // Arrange
            string sqlConnectionString = @"Data Source=(local);Initial Catalog=master;Integrated Security=True";

            string createScript = "CREATE DATABASE TestDB;";
            string tableCreateScript = "USE TestDB CREATE TABLE Persons (Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(30), Age INT, Nationality INT, Gender BIT)";
            string insertScript = "USE TestDB INSERT INTO Persons VALUES ('Brad',   15, 0, 1), ('Nicole', 25, 1, 0), ('Vera', 35, 2, 0)";
            var persons = new List<Person>()
            {
                new Person () {Id = 1, Name = "Brad", Age = 15, Gender = true, Nationality = Nationality.Russian },
                new Person () {Id = 2, Name = "Nicole", Age = 25, Gender = false, Nationality = Nationality.American },
                new Person () {Id = 3, Name = "Vera", Age = 35, Gender = false, Nationality = Nationality.Pole }
            };

            SqlConnection conn = new SqlConnection(sqlConnectionString);

            Server server = new Server(new ServerConnection(conn));
            ADOReflectionOrmDataContext dbContext =
                new ADOReflectionOrmDataContext(@"Data Source=(local);Initial Catalog=TestDB;Integrated Security=True");

            try
            {
                server.ConnectionContext.ExecuteNonQuery(createScript);
                server.ConnectionContext.ExecuteNonQuery(tableCreateScript);
                server.ConnectionContext.ExecuteNonQuery(insertScript);
                conn.Close();


                // Act
                var result = dbContext.Query<Person>("SELECT * FROM Persons");
                // Assert
                Assert.AreEqual(result.Count(), 3);

                foreach (Person r in result)
                {
                    Assert.IsTrue(persons.Any(p => (p.Id == r.Id) && (p.Name == r.Name) && (p.Age == r.Age) &&
                    (p.Gender == r.Gender) && (p.Nationality == r.Nationality)));
                }
            }
            finally
            {
                dbContext.Dispose();
                server.ConnectionContext.ExecuteNonQuery("ALTER DATABASE TestDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE TestDB");
            }
        }

        /// <summary>
        /// Test method Query when the query result has no rows. Methon should return an empty collection
        /// </summary>
        [TestMethod]
        public void Query_WhenQueryResultHasNoData_ShouldReturnEmptyPersonsCollection()
        {
         // Arrange
         string sqlConnectionString = @"Data Source=(local);Initial Catalog=master;Integrated Security=True";

         string createScript = "CREATE DATABASE TestDB;";
         string tableCreateScript = "USE TestDB CREATE TABLE Persons (Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(30), Age INT, Nationality INT, Gender BIT)";     

         SqlConnection conn = new SqlConnection(sqlConnectionString);

         Server server = new Server(new ServerConnection(conn));
         ADOReflectionOrmDataContext dbContext =
             new ADOReflectionOrmDataContext(@"Data Source=(local);Initial Catalog=TestDB;Integrated Security=True");
            try
            {
                server.ConnectionContext.ExecuteNonQuery(createScript);
                server.ConnectionContext.ExecuteNonQuery(tableCreateScript);
                
                conn.Close();

                // Act
                var result = dbContext.Query<Person>("SELECT * FROM Persons");
                // Assert
                Assert.AreEqual(result.Count(), 0);

            }
            finally
            {
                dbContext.Dispose();
                server.ConnectionContext.ExecuteNonQuery("ALTER DATABASE TestDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE TestDB");
            }
        }

        /// <summary>
        /// Test method query when query result has incorrect data. The method should throw MappingException
        /// </summary>
        [ExpectedException(typeof(MappingException))]
        [TestMethod]
        public void Query_WhenQueryResultHasIncorrectData_ShouldThrowMappingException()
        {
            // Arrange
            string sqlConnectionString = @"Data Source=(local);Initial Catalog=master;Integrated Security=True";

            string createScript = "CREATE DATABASE TestDB;";
            string tableCreateScript = "USE TestDB CREATE TABLE Persons (Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(30), Age INT, Nationality INT, Gender BIT)";
            string insertScript = "USE TestDB INSERT INTO Persons VALUES ('Brad', 15, 0, 1), ('Nicole', 25, 1, 0), ('Vera', 35, 2, 0)";

            SqlConnection conn = new SqlConnection(sqlConnectionString);

            Server server = new Server(new ServerConnection(conn));

            ADOReflectionOrmDataContext dbContext =
                   new ADOReflectionOrmDataContext(@"Data Source=(local);Initial Catalog=TestDB;Integrated Security=True");
            try
            {
                server.ConnectionContext.ExecuteNonQuery(createScript);
                server.ConnectionContext.ExecuteNonQuery(tableCreateScript);
                server.ConnectionContext.ExecuteNonQuery(insertScript);
                conn.Close();

                // Act
                var result = dbContext.Query<Person>("SELECT Name, Age FROM Persons");
            }
            finally
            {
                dbContext.Dispose();
                server.ConnectionContext.ExecuteNonQuery("ALTER DATABASE TestDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE TestDB");                
            }
        }

    }
}
