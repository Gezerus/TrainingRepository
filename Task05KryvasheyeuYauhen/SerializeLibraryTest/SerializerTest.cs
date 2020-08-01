
using System.Collections.Generic;
using System.Runtime.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializeLibrary;

namespace SerializeLibraryTest
{


    [TestClass]
    public class SerializerTest
    {
        /// <summary>
        /// Tests the BinarySerialize method. The method should serialize a person correctly.
        /// </summary>
        [TestMethod]
        public void BinarySerialize_ShouldSerializeCorrectly()
        {
            // Arrange
            var person = new Person()
            {
                Name = "Jon",
                Age = 31                
            };

            var serializer = new Serializer<Person>(person);
            // Act
            serializer.BinarySerialize(@"..\..\Files\Dat\Person.dat");
        }
        /// <summary>
        /// Tests the BinaryDeSerialize method when person class is correct. The method should deserialize a person correctly.
        /// </summary>
        [TestMethod]
        public void BinaryDeserialize_WhenCorrectPersonProvided_ShouldDeserializeCorrectly()
        {
            // Arrange and Act
            var person = Serializer<Person>.BinaryDeserialize(@"..\..\Files\Dat\Person.dat");
            // Assert
            Assert.AreEqual(person.Name, "Jon");
            Assert.AreEqual(person.Age, 31);
        }
        /// <summary>
        /// Tests the BinaryDeSerialize method when person class has just a property "string Name". The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void BinaryDeserialize_WhenPersonHasJusName_ShouldThrowException()
        {
            // Arrange and Act
            var person = Serializer<Person>.BinaryDeserialize(@"..\..\Files\Dat\PersonJustName.dat");
            // class Person in file "PersonJustName":
            //public class Person : ISerialize
            //{
            //public string Name { get; set; }
            //}
        }

        /// <summary>
        /// Tests the BinaryDeSerialize method when person class has a property "int TestField". The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void BinaryDeserialize_WhenPersonHasTestField_ShouldThrowException()
        {
            // Arrange and Act
            var person = Serializer<Person>.BinaryDeserialize(@"..\..\Files\Dat\PersonWithTestField.dat");
            // class Person in file "PersonWithTestField":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public int Age { get; set; }
            // public int TestField { get; set; }
            // }
        }

        /// <summary>
        /// Tests the BinaryDeSerialize method when person class has incorrect type of field ("string Age"). The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void BinaryDeserialize_WhenPersonHasIncorrectTypeOfField_ShouldThrowException()
        {
            // Arrange and Act
            var person = Serializer<Person>.BinaryDeserialize(@"..\..\Files\Dat\PersonIncorrectType.dat");
            // class Person in file "PersonIncorrectType":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public string Age { get; set; }          
            // }
        }

        /// <summary>
        /// Tests the BinaryDeSerialize method when person class has incorrect name of field ("int wrongName"). The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void BinaryDeserialize_WhenPersonHasIncorrectNameOfField_ShouldThrowException()
        {
            // Arrange and Act
            var person = Serializer<Person>.BinaryDeserialize(@"..\..\Files\Dat\PersonIncorrectName.dat");
            // class Person in file "PersonIncorrectName":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public int wrongName { get; set; }          
            // }
        }

        /// <summary>
        /// Tests the BinaryCollectionSerialize method. The method should serialize  persons correctly.
        /// </summary>
        [TestMethod]
        public void BinaryCollectionSerialize_ShouldSerializeCorrectly()
        {
            // Arrange
            var persons = new List<Person>()
                {
                    new Person() { Name = "Noah", Age = 25},
                    new Person() { Name = "Liam", Age = 27},
                    new Person() { Name = "Jacob", Age = 30}
                };
            // Act
            Serializer<Person>.BinaryCollectionSerialize(persons, @"..\..\Files\Dat\Persons.dat");
        }

        /// <summary>
        /// Tests the BinaryCollectionDeSerialize method when person class is correct. The method should deserialize  persons correctly.
        /// </summary>
        [TestMethod]
        public void BinaryCollectionDeserialize_ShouldDeserializeCorrectly()
        {
            // Arrange and Act
            List<Person> persons = (List<Person>)Serializer<Person>.BinaryCollectionDeserialize(@"..\..\Files\Dat\Persons.dat");
            // Assert
            Assert.AreEqual(persons.Count, 3);
            Assert.AreEqual(persons[0].Name, "Noah");
            Assert.AreEqual(persons[1].Name, "Liam");
            Assert.AreEqual(persons[2].Name, "Jacob");
        }

        /// <summary>
        /// Tests the BinaryCollectionDeSerialize method when person class has just a property "string Name". The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void BinaryCollectionDeserialize_WhenPersonHasJusName_ShouldThrowException()
        {
            // Arrange and Act
            var persons = Serializer<Person>.BinaryCollectionDeserialize(@"..\..\Files\Dat\PersonsJustNames.dat");
            // class Person in file "PersonJustNames":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // }
        }

        /// <summary>
        /// Tests the BinaryCollectionDeSerialize method when person class has a property "int TestField". 
        /// The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void BinaryCollectionDeserialize_WhenPersonHasTestField_ShouldThrowException()
        {
            // Arrange and Act
            var persons = Serializer<Person>.BinaryCollectionDeserialize(@"..\..\Files\Dat\PersonsWithTestField.dat");
            // class Person in file "PersonsWithTestField":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public int Age { get; set; }
            // public int TestField { get; set; }
            // }
        }

        /// <summary>
        /// Tests the BinaryCollection DeSerialize method when person class has incorrect type of field ("string Age"). 
        /// The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void BinaryCollectionDeserialize_WhenPersonHasIncorrectTypeOfField_ShouldThrowException()
        {
            // Arrange and Act
            var persons = Serializer<Person>.BinaryCollectionDeserialize(@"..\..\Files\Dat\PersonsIncorrectType.dat");
            // class Person in file "PersonsIncorrectType":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public string Age { get; set; }          
            // }
        }

        /// <summary>
        /// Tests the BinaryCollectionDeSerialize method when person class has incorrect name of field ("int wrongName"). 
        /// The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void BinaryCollectionDeserialize_WhenPersonHasIncorrectNameOfField_ShouldThrowException()
        {
            // Arrange and Act
            var persons = Serializer<Person>.BinaryCollectionDeserialize(@"..\..\Files\Dat\PersonsIncorrectName.dat");
            // class Person in file "PersonsIncorrectName":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public int wrongName { get; set; }          
            // }
        }

        /// <summary>
        /// Tests the JsonSerialize method. The method should serialize a person correctly.
        /// </summary>
        [TestMethod]
        public void JsonSerialize_ShouldSerializeCorrectly()
        {
            // Arrange
            var person = new Person()
            {
                Name = "Jon",
                Age = 31                
            };

            var ser = new Serializer<Person>(person);
            // Act
            ser.JsonSerialize(@"..\..\Files\Json\Person.json");
        }

        /// <summary>
        /// Tests the JsonDeSerialize method when person class is correct. The method should deserialize a person correctly.
        /// </summary>
        [TestMethod]
        public void JsonDeserialize_ShouldDeserializeObjectCorrectly()
        {
            // Arange and Act
            Person person = Serializer<Person>.JsonDeserialize(@"..\..\Files\Json\Person.json");
            // Assert
            Assert.AreEqual(person.Name, "Jon");
            Assert.AreEqual(person.Age, 31);
        }

        /// <summary>
        /// Tests the JsonDeSerialize method when person class has just a property "string Name". The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void JsonDeserialize_WhenPersonHasJusName_ShouldThrowException()
        {
            // Arrange and Act
            var person = Serializer<Person>.JsonDeserialize(@"..\..\Files\Json\PersonJustName.json");
            // class Person in file "PersonJustName":
            //public class Person : ISerialize
            //{
            //public string Name { get; set; }
            //}
        }

        /// <summary>
        /// Tests the JsonDeSerialize method when person class has a property "int TestField". The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void JsonDeserialize_WhenPersonHasTestField_ShouldThrowException()
        {
            // Arrange and Act
            var person = Serializer<Person>.JsonDeserialize(@"..\..\Files\Json\PersonWithTestField.json");
            // class Person in file "PersonWithTestField":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public int Age { get; set; }
            // public int TestField { get; set; }
            // }
        }

        /// <summary>
        /// Tests the JsonDeSerialize method when person class has incorrect type of field ("string Age"). 
        /// The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void JsonDeserialize_WhenPersonHasIncorrectTypeOfField_ShouldThrowException()
        {
            // Arrange and Act
            var person = Serializer<Person>.JsonDeserialize(@"..\..\Files\Json\PersonIncorrectType.json");
            // class Person in file "PersonIncorrectType":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public string Age { get; set; }          
            // }
        }

        /// <summary>
        /// Tests the JsonDeSerialize method when person class has incorrect name of field ("int wrongName"). 
        /// The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void JsonDeserialize_WhenPersonHasIncorrectNameOfField_ShouldThrowException()
        {
            // Arrange and Act
            var person = Serializer<Person>.JsonDeserialize(@"..\..\Files\Json\PersonIncorrectName.json");
            // class Person in file "PersonIncorrectName":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public int wrongName { get; set; }          
            // }
        }

        /// <summary>
        /// Tests the JsonCollectionSerialize method. The method should serialize  persons correctly.
        /// </summary>
        [TestMethod]
        public void JsonCollectionSerialize_ShouldSerializeCorrectly()
        {
            // Arrange
            var persons = new List<Person>()
            {
                    new Person() { Name = "Noah", Age = 25},
                    new Person() { Name = "Liam", Age = 27},
                    new Person() { Name = "Jacob", Age = 30}
            };
            // Act
            Serializer<Person>.JsonCollectionSerialize(persons, @"..\..\Files\Json\Persons.json");
        }
        /// <summary>
        /// Tests the JsonCollectionDeSerialize method when person class is correct. The method should deserialize  persons correctly.
        /// </summary>
        [TestMethod]
        public void JsonCollectionDeserialize_ShouldDeserializeCorrectly()
        {
            // Arrange and Act
            List<Person> persons = (List<Person>)Serializer<Person>.JsonCollectionDeserialize(@"..\..\Files\Json\Persons.json");
            // Assert
            Assert.AreEqual(persons.Count, 3);
            Assert.AreEqual(persons[0].Name, "Noah");
            Assert.AreEqual(persons[1].Name, "Liam");
            Assert.AreEqual(persons[2].Name, "Jacob");
        }

        /// <summary>
        /// Tests the JsonCollectionDeSerialize method when person class has just a property "string Name". 
        /// The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void JsonCollectionDeserialize_WhenPersonHasJusName_ShouldThrowException()
        {
            // Arrange and Act
            var persons = Serializer<Person>.JsonCollectionDeserialize(@"..\..\Files\Json\PersonsJustNames.json");
            // class Person in file "PersonsJustNames":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // }
        }

        /// <summary>
        /// Tests the JsonCollectionDeSerialize method when person class has a property "int TestField". 
        /// The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void JsonCollectionDeserialize_WhenPersonHasTestField_ShouldThrowException()
        {
            // Arrange and Act
            var persons = Serializer<Person>.JsonCollectionDeserialize(@"..\..\Files\Json\PersonsWithTestField.json");
            // class Person in file "PersonsWithTestField":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public int Age { get; set; }
            // public int TestField { get; set; }
            // }
        }

        /// <summary>
        /// Tests the JsonCollection DeSerialize method when person class has incorrect type of field ("string Age"). 
        /// The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void JsonCollectionDeserialize_WhenPersonHasIncorrectTypeOfField_ShouldThrowException()
        {
            // Arrange and Act
            var persons = Serializer<Person>.JsonCollectionDeserialize(@"..\..\Files\Json\PersonsIncorrectType.json");
            // class Person in file "PersonsIncorrectType":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public string Age { get; set; }          
            // }
        }

        /// <summary>
        /// Tests the JsonCollectionDeSerialize method when person class has incorrect name of field ("int wrongName"). 
        /// The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void JsonCollectionDeserialize_WhenPersonHasIncorrectNameOfField_ShouldThrowException()
        {
            // Arrange and Act
            var persons = Serializer<Person>.BinaryCollectionDeserialize(@"..\..\Files\Json\PersonsIncorrectName.json");
            // class Person in file "PersonsIncorrectName":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public int wrongName { get; set; }          
            // }
        }

        /// <summary>
        /// Tests the XmlSerialize method. The method should serialize a person correctly.
        /// </summary>
        [TestMethod]
        public void XmlSerialize_ShouldSerializeCorrectly()
        {
            // Arrange
            var person = new Person()
            {
                Name = "Jon",
                Age = 31                
            };
            var serializer = new Serializer<Person>(person);
            // Act
            serializer.XmlSerialize(@"..\..\Files\Xml\Person.xml");
        }

        /// <summary>
        /// Tests the XmlDeSerialize method when person class is correct. The method should deserialize a person correctly.
        /// </summary>
        [TestMethod]
        public void XmlDeserialize_ShouldDeserializeObjectCorrectly()
        {
            // Arrange and Act
            Person person = Serializer<Person>.XmlDeserialize(@"..\..\Files\Xml\Person.xml");
            // Assert
            Assert.AreEqual(person.Name, "Jon");
            Assert.AreEqual(person.Age, 31);
        }

        /// <summary>
        /// Tests the XmlDeSerialize method when person class has just a property "string Name". The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void XmlDeserialize_WhenPersonHasJusName_ShouldThrowException()
        {
            // Arrange and Act
            var person = Serializer<Person>.XmlDeserialize(@"..\..\Files\Xml\PersonJustName.xml");
            // class Person in file "PersonJustName":
            //public class Person : ISerialize
            //{
            //public string Name { get; set; }
            //}
        }

        /// <summary>
        /// Tests the XmlDeSerialize method when person class has a property "int TestField". The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void XmlDeserialize_WhenPersonHasTestField_ShouldThrowException()
        {
            // Arrange and Act
            var person = Serializer<Person>.XmlDeserialize(@"..\..\Files\Xml\PersonWithTestField.xml");
            // class Person in file "PersonWithTestField":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public int Age { get; set; }
            // public int TestField { get; set; }
            // }
        }

        /// <summary>
        /// Tests the XmlDeSerialize method when person class has incorrect type of field ("string Age"). The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void XmlDeserialize_WhenPersonHasIncorrectTypeOfField_ShouldThrowException()
        {
            // Arrange and Act
            var person = Serializer<Person>.XmlDeserialize(@"..\..\Files\Xml\PersonIncorrectType.xml");
            // class Person in file "PersonIncorrectType":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public string Age { get; set; }          
            // }
        }

        /// <summary>
        /// Tests the XmlDeSerialize method when person class has incorrect name of field ("int wrongName"). The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void XmlDeserialize_WhenPersonHasIncorrectNameOfField_ShouldThrowException()
        {
            // Arrange and Act
            var person = Serializer<Person>.XmlDeserialize(@"..\..\Files\Xml\PersonIncorrectName.xml");
            // class Person in file "PersonIncorrectName":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public int wrongName { get; set; }          
            // }
        }
        /// <summary>
        /// Tests the XmlCollectionSerialize method. The method should serialize  persons correctly.
        /// </summary>
        [TestMethod]
        public void XmlCollectionSerialize_ShouldSerializeCorrectly()
        {
            // Arrange
            var persons = new List<Person>()
            {
                    new Person() { Name = "Noah", Age = 25},
                    new Person() { Name = "Liam", Age = 27},
                    new Person() { Name = "Jacob", Age = 30}
            };
            // Act
            Serializer<Person>.XmlCollectionSerialize(persons, @"..\..\Files\Xml\Persons.xml");
        }

        /// <summary>
        /// Tests the XmlCollectionDeSerialize method when person class is correct. The method should deserialize  persons correctly.
        /// </summary>
        [TestMethod]
        public void XmlCollectionDeserialize_ShouldDeserializeCorrectly()
        {
            // Arrange and Act
            List<Person> persons = (List<Person>)Serializer<Person>.XmlCollectionDeserialize(@"..\..\Files\Xml\Persons.xml");
            // Assert
            Assert.AreEqual(persons.Count, 3);
            Assert.AreEqual(persons[0].Name, "Noah");
            Assert.AreEqual(persons[1].Name, "Liam");
            Assert.AreEqual(persons[2].Name, "Jacob");
        }

        /// <summary>
        /// Tests the XmlCollectionDeSerialize method when person class has just a property "string Name". The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void XmlCollectionDeserialize_WhenPersonHasJusName_ShouldThrowException()
        {
            // Arrange and Act
            var persons = Serializer<Person>.XmlCollectionDeserialize(@"..\..\Files\Xml\PersonsJustNames.xml");
            // class Person in file "PersonJustNames":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // }
        }

        /// <summary>
        /// Tests the XmlCollectionDeSerialize method when person class has a property "int TestField". 
        /// The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void XmlCollectionDeserialize_WhenPersonHasTestField_ShouldThrowException()
        {
            // Arrange and Act
            var persons = Serializer<Person>.XmlCollectionDeserialize(@"..\..\Files\Xml\PersonsWithTestField.xml");
            // class Person in file "PersonsWithTestField":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public int Age { get; set; }
            // public int TestField { get; set; }
            // }
        }

        /// <summary>
        /// Tests the XmlCollection DeSerialize method when person class has incorrect type of field ("string Age"). 
        /// The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void XmlCollectionDeserialize_WhenPersonHasIncorrectTypeOfField_ShouldThrowException()
        {
            // Arrange and Act
            var persons = Serializer<Person>.XmlCollectionDeserialize(@"..\..\Files\Xml\PersonsIncorrectType.xml");
            // class Person in file "PersonsIncorrectType":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public string Age { get; set; }          
            // }
        }

        /// <summary>
        /// Tests the XmlCollectionDeSerialize method when person class has incorrect name of field ("int wrongName"). 
        /// The method should throw SerializationException.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(SerializationException))]
        public void XmlCollectionDeserialize_WhenPersonHasIncorrectNameOfField_ShouldThrowException()
        {
            // Arrange and Act
            var persons = Serializer<Person>.XmlCollectionDeserialize(@"..\..\Files\Xml\PersonsIncorrectName.xml");
            // class Person in file "PersonsIncorrectName":
            // public class Person : ISerialize
            // {
            // public string Name { get; set; }
            // public int wrongName { get; set; }          
            // }
        }

    }
}
