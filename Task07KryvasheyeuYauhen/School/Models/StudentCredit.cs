using System.Data.Linq.Mapping;

namespace School.Models
{
    [Table(Name = "StudentsCredits")]
    public class StudentCredit
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public int CreditId { get; set; }
        [Column]
        public int StudentId { get; set; }
        [Column]
        public bool Result { get; set; }
    }
}
