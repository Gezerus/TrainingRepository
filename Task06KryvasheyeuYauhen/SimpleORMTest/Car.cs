using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleORMTest
{
    /// <summary>
    /// the model for tests
    /// </summary>
    public class Car
    {
        public int Id { get; set; }

        public string Model { get; set; }
    }
}
