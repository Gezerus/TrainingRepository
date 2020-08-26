using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleORMTest
{
    [Table(Name="Persons")]
    class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }


        public bool Gender { get; set; }

        public Nationality Nationality { get; set; }
    }

    enum Nationality
    {
        Russian,
        American,
        Pole
    }
}
