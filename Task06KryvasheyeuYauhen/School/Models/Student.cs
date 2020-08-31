using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School
{
    [Table(Name ="Students")]
    public class Student
    {
        [Column(IsDbGenerated =true, IsPrimaryKey =true)]
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birthday { get; set; }

        public Gender Gender { get; set; }

        public int GroupId { get; set; }
    }

    public enum Gender
    {
        woman,
        man
    }
}
