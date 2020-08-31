using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School
{
    [Table(Name = "Sessions")]
    public class Session
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }
    }
}
