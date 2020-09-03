using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repositories
{
    public class ExamRepository : Repository<Exam>
    {
        public ExamRepository(DataContext dataContext) : base(dataContext)
        { }

        public override void Update(Exam exam)
        {
            var newExam = GetById(exam.Id);
            newExam.SubjectId = exam.SubjectId;
            newExam.SessionId = exam.SessionId;
            newExam.Date = exam.Date;
            newExam.TeacherId = exam.TeacherId;
            _dataContext.SubmitChanges();
        }
    }
}
