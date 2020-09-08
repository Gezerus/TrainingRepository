using School.Models;
using System.Data.Linq;

namespace School.Repositories
{
    /// <summary>
    /// describes the interaction with the StudentsCredits table
    /// </summary>
    public class StudentCreditRepository : Repository<StudentCredit>
    {
        public StudentCreditRepository(DataContext dataContext) : base(dataContext)
        { }

        /// <summary>
        /// updates a StudentCredit in the StudentsCredits table
        /// </summary>
        /// <param name="studentCredit"></param>
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
