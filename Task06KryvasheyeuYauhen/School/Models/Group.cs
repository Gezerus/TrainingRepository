using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School
{
    [Table(Name="Groups")]
    public class Group
    {
        [Column(IsPrimaryKey =true, IsDbGenerated =true)]
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
