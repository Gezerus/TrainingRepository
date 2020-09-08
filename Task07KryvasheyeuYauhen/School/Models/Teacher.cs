using System.Data.Linq.Mapping;

namespace School.Models
{
    [Table(Name = "Teachers")]
    public class Teacher
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public string Name { get; set; }
    }
}
