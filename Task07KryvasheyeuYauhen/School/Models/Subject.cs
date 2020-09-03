using System.Data.Linq.Mapping;

namespace School.Models
{
    [Table(Name = "Subjects")]
    public class Subject
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }
    }
}
