using System;
using System.Data.Linq.Mapping;

namespace School.Models
{
    [Table(Name = "Students")]
    public class Student 
    {
        [Column(IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }

        [Column(UpdateCheck = UpdateCheck.Never)]
        public DateTime Birthday { get; set; }
        [Column]
        public Gender Gender { get; set; }
        [Column]
        public int GroupId { get; set; }
    }

    public enum Gender
    {
        woman,
        man
    }
}
