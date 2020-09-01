using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Management.Common;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleORM;

namespace SimpleORMTest
{    
    [TestClass]
    public class OrmDataContextTest
    {
        private string _connectionString = "Data Source=(local);Initial Catalog = TestDB; Integrated Security = True";
        private Server CreateTestDB()
        {
            string sqlConnectionString = @"Data Source=(local);Initial Catalog=master;Integrated Security=True";

            string createScript = "CREATE DATABASE TestDB;";
            string tableCreateScript = "USE TestDB CREATE TABLE Persons (Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(30), Age INT, Nationality INT, Gender BIT)";
            string insertScript = "USE TestDB INSERT INTO Persons VALUES ('Brad', 15, 0, 1), ('Nicole', 25, 1, 0), ('Vera', 35, 2, 0)";
            SqlConnection conn = new SqlConnection(sqlConnectionString);

            Server server = new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery(createScript);
            server.ConnectionContext.ExecuteNonQuery(tableCreateScript);
            server.ConnectionContext.ExecuteNonQuery(insertScript);
            conn.Close();
            return server;
        }

        private void DeleteTestDB()
        {
            string sqlConnectionString = @"Data Source=(local);Initial Catalog=master;Integrated Security=True";
            SqlConnection conn = new SqlConnection(sqlConnectionString);
            Server server = new Server(new ServerConnection(conn));
            server.ConnectionContext.ExecuteNonQuery("ALTER DATABASE TestDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE; DROP DATABASE TestDB");
        }

        /// <summary>
        /// Test Query method when model and database table are correct. Method should return a collection of persons
        /// </summary>
        [TestMethod]
        public void Query_WhenModelMatchTheDataBaseTable_ShouldMapcorrectly()
        {
            // Arrange
            var persons = new List<Person>
            {
                new Person  {Id = 1, Name = "Brad", Age = 15, Gender = true, Nationality = Nationality.Russian },
                new Person  {Id = 2, Name = "Nicole", Age = 25, Gender = false, Nationality = Nationality.American },
                new Person  {Id = 3, Name = "Vera", Age = 35, Gender = false, Nationality = Nationality.Pole }
            };
           
            var dbContext = OrmDataContext.Initialize(_connectionString);
            try
            {
                CreateTestDB();
                // Act
                var result = dbContext.Query<Person>("SELECT * FROM Persons");
                // Assert
                Assert.AreEqual(result.Count(), 3);

                foreach (Person person in result)
                {
                    Assert.IsTrue(persons.Any(p => (p.Id == person.Id) && (p.Name == person.Name) && (p.Age == person.Age) &&
                    (p.Gender == person.Gender) && (p.Nationality == person.Nationality)));
                }
            }
            finally
            {
                dbContext.Dispose();
                DeleteTestDB();
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
            var dbContext = OrmDataContext.Initialize(_connectionString);
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
            var dbContext = OrmDataContext.Initialize(_connectionString);
            try
            {
                CreateTestDB();
                // Act
                var result = dbContext.Query<Person>("SELECT Name, Age FROM Persons");
            }
            finally
            {
                dbContext.Dispose();
                DeleteTestDB();              
            }
        }

        /// <summary>
        /// Test Query method with parameters when model and database table are correct. Method should return a collection of persons
        /// </summary>
        [TestMethod]
        public void QueryWhithParameters_WhenModelMatchTheDataBaseTable_Shouldmapcorrectly()
        {
            // Arrange
            var persons = new List<Person>()
            {
                new Person () {Id = 2, Name = "Nicole", Age = 25, Gender = false, Nationality = Nationality.American },
                new Person () {Id = 3, Name = "Vera", Age = 35, Gender = false, Nationality = Nationality.Pole }
            };
            var dbContext = OrmDataContext.Initialize(_connectionString);

            try
            {
                CreateTestDB();
                // Act
                var result = dbContext.Query<Person>("SELECT * FROM Persons WHERE Gender = @Gender", new { Gender = false});
                // Assert
                Assert.AreEqual(result.Count(), 2);

                foreach (Person r in result)
                {
                    Assert.IsTrue(persons.Any(p => (p.Id == r.Id) && (p.Name == r.Name) && (p.Age == r.Age) &&
                    (p.Gender == r.Gender) && (p.Nationality == r.Nationality)));
                }
            }
            finally
            {
                dbContext.Dispose();
                DeleteTestDB();
            }
        }

        /// <summary>
        /// Test GetAll method when model and database table are correct. Method should return a collection of persons
        /// </summary>
        [TestMethod]
        public void GetAll_WhenModelMatchTheDataBaseTable_Shouldmapcorrectly()
        {
            // Arrange
            var persons = new List<Person>()
            {
                new Person () {Id = 1, Name = "Brad", Age = 15, Gender = true, Nationality = Nationality.Russian },
                new Person () {Id = 2, Name = "Nicole", Age = 25, Gender = false, Nationality = Nationality.American },
                new Person () {Id = 3, Name = "Vera", Age = 35, Gender = false, Nationality = Nationality.Pole }
            };

            var dbContext = OrmDataContext.Initialize(_connectionString);

            try
            {
                CreateTestDB();
                // Act
                var result = dbContext.GetAll<Person>();
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
                DeleteTestDB();
            }
        }
        /// <summary>
        /// Test method GetAll when the model does not have TableAttribute. The method should throw MappingException
        /// </summary>
        [ExpectedException(typeof(MappingException))]
        [TestMethod]
        public void GetAll_WhenModeHasNoTableAttribute_Shouldmapcorrectly()
        {
            // Arrange
            var dbContext = OrmDataContext.Initialize(_connectionString);

            try
            {
                var server = CreateTestDB();
                string tableCreateScript = "USE TestDB CREATE TABLE Cars (Id INT PRIMARY KEY IDENTITY, Model NVARCHAR(30))";
                string insertScript = "USE TestDB INSERT INTO Persons VALUES ('Brad', 15, 0, 1), ('Nicole', 25, 1, 0), ('Vera', 35, 2, 0)";
                server.ConnectionContext.ExecuteNonQuery(tableCreateScript);
                server.ConnectionContext.ExecuteNonQuery(insertScript);
                // Act
                var result = dbContext.GetAll<Car>();
                // Assert
            }
            finally
            {
                dbContext.Dispose();
                DeleteTestDB();
            }
        }

        /// <summary>
        /// Test Exequte method when correct parameters of query provided. The method should exequte the instruction correctly
        /// </summary>
        [TestMethod]
        public void Exequte_WhenCorrectParametersProvided_ShouldExequteInstructionCorrectly()
        {
            // Arrange
            var persons = new List<Person>()
            {
                new Person () {Id = 1, Name = "Brad", Age = 15, Gender = true, Nationality = Nationality.Russian },
                new Person () {Id = 2, Name = "Nicole", Age = 25, Gender = false, Nationality = Nationality.American },
                new Person () {Id = 3, Name = "Vera", Age = 35, Gender = false, Nationality = Nationality.Pole },
                new Person () {Id = 4, Name = "Fedor", Age = 45, Gender = true, Nationality = Nationality.Pole }
            };

            var dbContext = OrmDataContext.Initialize(_connectionString);

            try
            {
                CreateTestDB();
                // Act
                var number = dbContext.Execute("INSERT INTO Persons (Name, Age, Gender, Nationality) VALUES (@Name, @Age,  @Gender, @Nationality)", 
                    new { Name = "Fedor", Age = 45, Gender = true, Nationality = Nationality.Pole });
                // Assert
                var result = dbContext.GetAll<Person>();

                Assert.AreEqual(number, 1);

                foreach (Person r in result)
                {
                    Assert.IsTrue(persons.Any(p => (p.Id == r.Id) && (p.Name == r.Name) && (p.Age == r.Age) &&
                    (p.Gender == r.Gender) && (p.Nationality == r.Nationality)));
                }
            }
            finally
            {
                dbContext.Dispose();
                DeleteTestDB();
            }
        }

        /// <summary>
        /// Test Exequte method when correct parameters of query provided. The method should exequte the instruction correctly
        /// </summary>
        [ExpectedException(typeof(System.Data.SqlClient.SqlException))]
        [TestMethod]
        public void Exequte_WhenInCorrectParametersProvided_ShouldExequteInstructionCorrectly()
        {
            // Arrange
            var dbContext = OrmDataContext.Initialize(_connectionString);

            try
            {
                CreateTestDB();
                // Act
                var number = dbContext.Execute("INSERT INTO Persons (Name, Age, Gender, Nationality) VALUES (@Name, @Age,  @Gender, @Nationality)",
                    new { Name = "Fedor", Age = 45, Gender = true });
            }
            finally
            {
                dbContext.Dispose();
                DeleteTestDB();
            }
        }

        /// <summary>
        /// Test method Insert when correct model provided. The Methon should insert a person to the database
        /// </summary>
        [TestMethod]
        public void Insert_WhenCorrectModelProvided_ShouldAddObjectToDataBase()
        {
            // Arrange
            var dbContext = OrmDataContext.Initialize(_connectionString);

            var person1 = new Person()
            { Name = "Bob", Age = 100, Gender = true, Nationality = Nationality.American };
            try
            {
                CreateTestDB();
                // Act
                var number = dbContext.Insert(person1);
                // Assert
                var person2 = dbContext.GetAll<Person>().Where(p => p.Id == 4).FirstOrDefault();


                Assert.AreEqual(number, 1);
                Assert.AreEqual(person1.Name, person2.Name);
                Assert.AreEqual(4, person2.Id);
                Assert.AreEqual(person1.Age, person2.Age);
                Assert.AreEqual(person1.Gender, person2.Gender);
                Assert.AreEqual(person1.Nationality, person2.Nationality);
            }
            finally
            {
                dbContext.Dispose();
                DeleteTestDB();
            }
        }

        /// <summary>
        /// Test method Insert when incorrect model provided. The Methon should throw mapping exception
        /// </summary>
        [ExpectedException(typeof(MappingException))]
        [TestMethod]
        public void Insert_WhenInCorrectModelProvided_ShouldThrowException()
        {
            // Arrange
            var dbContext = OrmDataContext.Initialize(_connectionString);
            try
            {
                var server = CreateTestDB();
                string tableCreateScript = "USE TestDB CREATE TABLE Cars (Id INT PRIMARY KEY IDENTITY, Model NVARCHAR(30))";
                string insertScript = "USE TestDB INSERT INTO Cars VALUES ('BMV'), ('LADA')";
                server.ConnectionContext.ExecuteNonQuery(tableCreateScript);
                server.ConnectionContext.ExecuteNonQuery(insertScript);
                var car = new Car() { Model = "AUDI" };
                dbContext.Insert(car);
                // Act
            }
            finally
            {
                dbContext.Dispose();
                DeleteTestDB();
            }
        }

        /// <summary>
        /// Test method Update when correct model provided. The Methon should update a person.
        /// </summary>
        [TestMethod]
        public void Update_WhenCorrectModelProvided_ShouldUpdateObjectCorrectly()
        {
            // Arrange
            var dbContext = OrmDataContext.Initialize(_connectionString);

            var person1 = new Person()
            { Name = "Bob", Age = 100, Gender = true, Nationality = Nationality.American };
            try
            {
                CreateTestDB();
                dbContext.Insert(person1);

                var person2 = new Person()
                {Id = 4, Name = "Boby", Age = 50, Gender = true, Nationality = Nationality.American };
                // Act
                var number = dbContext.Update(person2);
                // Assert
                var person3 = dbContext.GetAll<Person>().Where(p => p.Id == 4).FirstOrDefault();
                Assert.AreEqual(number, 1);
                Assert.AreEqual(person2.Name, person3.Name);
                Assert.AreEqual(person2.Id, person3.Id);
                Assert.AreEqual(person2.Age, person3.Age);
                Assert.AreEqual(person2.Gender, person3.Gender);
                Assert.AreEqual(person2.Nationality, person3.Nationality);
            }
            finally
            {
                dbContext.Dispose();
                DeleteTestDB();
            }
        }

        /// <summary>
        /// Test method Update when the model does not have any Primary Key. The Methon should throw MappongException
        /// </summary>
        [ExpectedException(typeof(MappingException))]
        [TestMethod]
        public void Update_WhenModelDoesNotHavePrimaryKey_ShouldThrowException()
        {
            // Arrange
            var dbContext = OrmDataContext.Initialize(_connectionString);
            try
            {
                var server = CreateTestDB();
                string tableCreateScript = "USE TestDB CREATE TABLE Footballer (Id INT PRIMARY KEY IDENTITY, Name NVARCHAR(30))";                
                server.ConnectionContext.ExecuteNonQuery(tableCreateScript);
                var foot1 = new Footballer()
                { Name = "Messi" };
                dbContext.Insert(foot1);

                var foot2 = new Footballer()
                { Id = 1, Name = "Ronaldo"};
                // Act
                var number = dbContext.Update(foot2);

            }
            finally
            {
                dbContext.Dispose();
                DeleteTestDB();
            }
        }

        /// <summary>
        /// Test method Delete when correct model provided. The Methon should delete a person.
        /// </summary>
        [TestMethod]
        public void Delete_WhenCorrectModelProvided_ShouldDeleteeObjectCorrectly()
        {
            // Arrange
            var dbContext = OrmDataContext.Initialize(_connectionString);
            try
            {
                CreateTestDB();
                var personsBeforeDel = dbContext.GetAll<Person>();
                var beforeCount = personsBeforeDel.Count();
                var personForDelete = personsBeforeDel.FirstOrDefault();
                // Act
                int number = dbContext.Delete(personForDelete);
                // Assert
                var personsAfterDel = dbContext.GetAll<Person>();
                var afterCount = personsAfterDel.Count();

                Assert.AreEqual(number, 1);
                Assert.AreEqual(beforeCount, afterCount + 1);
            }
            finally
            {
                dbContext.Dispose();
                DeleteTestDB();
            }
        }

    }
}
