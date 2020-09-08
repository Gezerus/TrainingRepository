using System;
using System.Data.Linq.Mapping;

namespace School.Models
{
    [Table(Name = "Sessions")]
    public class Session
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column(UpdateCheck = UpdateCheck.Never)]
        public DateTime StartDate { get; set; }
    }
}
