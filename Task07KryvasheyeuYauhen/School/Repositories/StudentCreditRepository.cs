using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repositories
{
    public class StudentCreditRepository : Repository<StudentCredit>
    {
        public StudentCreditRepository(DataContext dataContext) : base(dataContext)
        { }

        public override void Update(StudentCredit studentCredit)
        {
            var newStudentCredit = GetById(studentCredit.Id);
            newStudentCredit.CreditId = studentCredit.CreditId;
            newStudentCredit.StudentId = studentCredit.StudentId;
            newStudentCredit.Result = studentCredit.Result;            
            _dataContext.SubmitChanges();
        }
    }
}
