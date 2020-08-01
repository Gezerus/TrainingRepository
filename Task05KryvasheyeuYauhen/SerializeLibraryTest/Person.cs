using SerializeLibrary;
using System;

namespace SerializeLibraryTest
{
    /// <summary>
    /// Describes a person (Test class)
    /// </summary>
    [Serializable]
    public class Person : ISerialize
    {        
        public string Name { get; set; }

        public int Age { get; set; }

        //public int TestField { get; set; }

        //public int wrongAge { get; set; }

        //public string Age { get; set; }

    }
}
