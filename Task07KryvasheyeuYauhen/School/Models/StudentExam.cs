using System.Data.Linq.Mapping;

namespace School.Models
{
    [Table(Name = "StudentsExams")]
    public class StudentExam
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public int ExamId { get; set; }
        [Column]
        public int StudentId { get; set; }
        [Column]
        public int Grade { get; set; }
    }
}
