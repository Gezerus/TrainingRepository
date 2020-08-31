using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School
{
    [Table(Name = "StudentsExams")]
    public class StudentExam
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        public int ExamId { get; set; }

        public int StudentId { get; set; }

        public int Grade { get; set; }
    }
}
