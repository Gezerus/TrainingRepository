using SerializeLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace SerializeLibraryTest
{
    [Serializable]
    public class Person : ISerialize
    {        
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
