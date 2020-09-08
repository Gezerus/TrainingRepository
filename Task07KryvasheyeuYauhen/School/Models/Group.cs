using System.Data.Linq.Mapping;

namespace School.Models
{
    [Table(Name = "Groups")]
    public class Group
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Specialty { get; set; }
    }
}
