using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SerializeLibrary;

namespace SerializeLibraryTest
{


    [TestClass]
    public class SerializerTest
    {
        [TestMethod]
        public void Asdaksjdlakjsdladj()
        {
            var person = new Person()
            {
                Name = "Jon",
                //Age = 31
            };

            var serializer = new Serializer<Person>(person);

            serializer.BinarySerialize(@"..\..\Files\Person.dat");
        }

        [TestMethod]
        public void Asksjdlakjsdladj()
        {
            var person = Serializer<Person>.BinaryDeserialize(@"..\..\Files\Person.dat");
            
            Assert.AreEqual(person.Name, "Jon");
            //Assert.AreEqual(person.Age, 31);
        }

        [TestMethod]
        public void BinaryCollectionSerialize_ShouldSerializeCorrectly()
        {
            var persons = new List<Person>()
                {
                    //new Person() { Name = "Noah", Age = 25},
                    //new Person() { Name = "Liam", Age = 27},
                    //new Person() { Name = "Jacob", Age = 30}
                };

            Serializer<Person>.BinaryCollectionSerialize(persons, @"..\..\Files\Persons.dat");
        }

        [TestMethod]
        public void BinaryCollectionDeserialize_ShouldDeserializeCorrectly()
        {
            List<Person> persons = (List<Person>)Serializer<Person>.BinaryCollectionDeserialize(@"..\..\Files\Persons.dat");

            Assert.AreEqual(persons.Count, 3);
            Assert.AreEqual(persons[0].Name, "Noah");
            Assert.AreEqual(persons[1].Name, "Liam");
            Assert.AreEqual(persons[2].Name, "Jacob");

        }

        [TestMethod]
        public void JsonSerialize_ShouldSerializeCorrectly()
        {
            var person = new Person()
            {
                Name = "Jon",
                //Age = 31
            };

            var ser = new Serializer<Person>(person);

            ser.JsonSerialize(@"..\..\Files\Person.json");
        }

        [TestMethod]
        public void JsonDeserialize_ShouldDeserializeObjectCorrectly()
        {
            Person person = Serializer<Person>.JsonDeserialize(@"..\..\Files\Person.json");

            Assert.AreEqual(person.Name, "Jon");
            //Assert.AreEqual(person.Age, 31);
        }

        [TestMethod]
        public void JsonCollectionSerialize_ShouldSerializeCorrectly()
        {
            var persons = new List<Person>()
            {
                //new Person() { Name = "Noah", Age = 25},
                //new Person() { Name = "Liam", Age = 27},
                //new Person() { Name = "Jacob", Age = 30}
            };

            Serializer<Person>.JsonCollectionSerialize(persons, @"..\..\Files\Persons.json");
        }

        [TestMethod]
        public void JsonCollectionDeserialize_ShouldDeserializeCorrectly()
        {
            List<Person> persons = (List<Person>)Serializer<Person>.JsonCollectionDeserialize(@"..\..\Files\Persons.json");

            Assert.AreEqual(persons.Count, 3);
            Assert.AreEqual(persons[0].Name, "Noah");
            Assert.AreEqual(persons[1].Name, "Liam");
            Assert.AreEqual(persons[2].Name, "Jacob");

        }

        [TestMethod]
        public void XmlSerialize_ShouldSerializeCorrectly()
        {
            var person = new Person()
            {
                Name = "Jon",
                //Age = 31
            };
            var serializer = new Serializer<Person>(person);

            serializer.XmlSerialize(@"..\..\Files\Person.xml");
        }

        [TestMethod]
        public void Xmleserialize_ShouldDeserializeObjectCorrectly()
        {
            Person person = Serializer<Person>.XmlDeserialize(@"..\..\Files\Person.xml");

            Assert.AreEqual(person.Name, "Jon");
            //Assert.AreEqual(person.Age, 31);
        }

        [TestMethod]
        public void XmlCollectionSerialize_ShouldSerializeCorrectly()
        {
            var persons = new List<Person>()
            {
                //new Person() { Name = "Noah", Age = 25},
                //new Person() { Name = "Liam", Age = 27},
                //new Person() { Name = "Jacob", Age = 30}
            };

            Serializer<Person>.XmlCollectionSerialize(persons, @"..\..\Files\Persons.xml");
        }

        [TestMethod]
        public void XmlCollectionDeserialize_ShouldDeserializeCorrectly()
        {
            List<Person> persons = (List<Person>)Serializer<Person>.XmlCollectionDeserialize(@"..\..\Files\Persons.xml");

            Assert.AreEqual(persons.Count, 3);
            Assert.AreEqual(persons[0].Name, "Noah");
            Assert.AreEqual(persons[1].Name, "Liam");
            Assert.AreEqual(persons[2].Name, "Jacob");

        }

    }
}
