using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School
{

    [Table(Name = "StudentsCredits")]
    public class StudentCredit
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }

        public int CreditId { get; set; }

        public int StudentId { get; set; }

        public bool Result { get; set; }
    }
}
