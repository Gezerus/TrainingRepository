using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repositories
{
    /// <summary>
    /// describes the interaction with the StudentsExams table
    /// </summary>
    public class StudentExamRepository : Repository<StudentExam>
    {
        public StudentExamRepository(DataContext dataContext) : base(dataContext)
        { }

        /// <summary>
        /// updates a StudentExam in the StudentsExams table
        /// </summary>
        /// <param name="studentExam"></param>
        public override void Update(StudentExam studentExam)
        {
            var newStudentExam = GetById(studentExam.Id);
            newStudentExam.ExamId = studentExam.ExamId;
            newStudentExam.StudentId = studentExam.StudentId;
            newStudentExam.Grade = studentExam.Grade;
            _dataContext.SubmitChanges();
        }
    }
}
