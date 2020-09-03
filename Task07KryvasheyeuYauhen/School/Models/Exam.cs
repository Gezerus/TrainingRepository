using System;
using System.Data.Linq.Mapping;

namespace School.Models
{
    [Table(Name = "Exams")]
    public class Exam
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column]
        public int SubjectId { get; set; }
        [Column]
        public int SessionId { get; set; }
        [Column]
        public int TeacherId { get; set; }

        [Column(UpdateCheck = UpdateCheck.Never)]
        public DateTime Date { get; set; }
    }
}
