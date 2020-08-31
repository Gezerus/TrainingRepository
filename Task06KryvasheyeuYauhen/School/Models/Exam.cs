using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School
{
    [Table(Name = "Exams")]
    public class Exam
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        public int SubjectId { get; set; }

        public int SessionId { get; set; }

        public DateTime Date { get; set; }
    }
}
