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
    [Table(Name ="Footballer")]
    public class Footballer
    {
        [Column(IsDbGenerated =true)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
