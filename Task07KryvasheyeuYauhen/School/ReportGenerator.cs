using School.Reports;
using School.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School
{
    public class ReportGenerator
    {
        private SchoolRepository _db = new SchoolRepository();

        public IQueryable<IEnumerable<SpecialtyAverageReport>> SpecialtyAverageReportGenerate()
        {
            var query = from gr in _db.Groups.GetAll()
                        join st in _db.Students.GetAll() on gr.Id equals st.GroupId
                        join stEx in _db.StudentsExams.GetAll() on st.Id equals stEx.StudentId
                        join ex in _db.Exams.GetAll() on stEx.ExamId equals ex.Id
                        join ss in _db.Exams.GetAll() on ex.SessionId equals ss.Id
                        orderby ss.Id
                        group new { gr, st, stEx, ex, ss } by ss.Id into sessions
                        select from sARep in sessions
                               group sARep by new { sARep.gr.Specialty, sARep.ss.Id } into rep
                               select new SpecialtyAverageReport
                               {
                                   SessionId = rep.Key.Id,
                                   Specialty = rep.Key.Specialty,
                                   Average = (from grades in rep select (double)grades.stEx.Grade).Average()
                               };

            return query;
              
        }

        public IQueryable<IEnumerable<SpecialtyAverageReport>> SpecialtyAverageReportGenerate<TKey>(Func<SpecialtyAverageReport, TKey> keySelector)
        {
            var query = (from gr in _db.Groups.GetAll()
                         join st in _db.Students.GetAll() on gr.Id equals st.GroupId
                         join stEx in _db.StudentsExams.GetAll() on st.Id equals stEx.StudentId
                         join ex in _db.Exams.GetAll() on stEx.ExamId equals ex.Id
                         join ss in _db.Exams.GetAll() on ex.SessionId equals ss.Id
                         orderby ss.Id
                         group new { gr, st, stEx, ex, ss } by ss.Id into sessions
                         select from sARep in sessions
                                group sARep by new { sARep.gr.Specialty, sARep.ss.Id } into rep
                                select new SpecialtyAverageReport
                                {
                                    SessionId = rep.Key.Id,
                                    Specialty = rep.Key.Specialty,
                                    Average = (from grades in rep select (double)grades.stEx.Grade).Average()
                                }).AsEnumerable().Select(q => q.OrderBy(keySelector)).AsQueryable();

            return query;
        }

        public IQueryable<IEnumerable<TeacherAverageReport>> TeacherAverageReportGenerate()
        {
            var query = from t in _db.Teachers.GetAll()
                        join ex in _db.Exams.GetAll() on t.Id equals ex.TeacherId
                        join stEx in _db.StudentsExams.GetAll() on ex.Id equals stEx.ExamId
                        join ss in _db.Sessions.GetAll() on ex.SessionId equals ss.Id
                        orderby ss.Id
                        group new { t, ex, stEx, ss } by ss.Id into sessions
                        select from tARep in sessions
                               group tARep by new { SessionId = tARep.ss.Id, TeacherId = tARep.t.Id,
                               tARep.t.Name} into rep
                               select new TeacherAverageReport
                               {
                                   SessionId = rep.Key.SessionId,
                                   Id = rep.Key.TeacherId,
                                   Name = rep.Key.Name,
                                   Average = (from grades in rep select (double)grades.stEx.Grade).Average()
                               };

            return query;
        }

        public IQueryable<IEnumerable<TeacherAverageReport>> TeacherAverageReportGenerate<TKey>(Func<TeacherAverageReport, TKey> keySelector)
        {
            var query = (from t in _db.Teachers.GetAll()
                        join ex in _db.Exams.GetAll() on t.Id equals ex.TeacherId
                        join stEx in _db.StudentsExams.GetAll() on ex.Id equals stEx.ExamId
                        join ss in _db.Sessions.GetAll() on ex.SessionId equals ss.Id
                        orderby ss.Id
                        group new { t, ex, stEx, ss } by ss.Id into sessions
                        select from tARep in sessions
                               group tARep by new
                               {
                                   SessionId = tARep.ss.Id,
                                   TeacherId = tARep.t.Id,
                                   tARep.t.Name
                               } into rep
                               select new TeacherAverageReport
                               {
                                   SessionId = rep.Key.SessionId,
                                   Id = rep.Key.TeacherId,
                                   Name = rep.Key.Name,
                                   Average = (from grades in rep select (double)grades.stEx.Grade).Average()
                               }).AsEnumerable().Select(q => q.OrderBy(keySelector)).AsQueryable();

            return query;
        }

        public IQueryable<IEnumerable<AverageDynamicReport>> AverageDynamicReportGenerate()
        {
            var query = from s in _db.Subjects.GetAll()
                        join ex in _db.Exams.GetAll() on s.Id equals ex.SubjectId
                        join stEx in _db.StudentsExams.GetAll() on ex.Id equals stEx.ExamId
                        orderby ex.Date.Year
                        group new { s, ex, stEx } by ex.Date.Year into years
                        select from aDRep in years
                               group aDRep by new { aDRep.ex.Date.Year, aDRep.s.Name} into rep
                               select new AverageDynamicReport
                               {
                                   Year = rep.Key.Year,
                                   SubjectName = rep.Key.Name,                                   
                                   Average = (from grades in rep select (double)grades.stEx.Grade).Average()
                               };

            return query;
        }

        public IQueryable<IEnumerable<AverageDynamicReport>> AverageDynamicReportGenerate<TKey>(Func<AverageDynamicReport, TKey> keySelector)
        {
            var query = (from s in _db.Subjects.GetAll()
                        join ex in _db.Exams.GetAll() on s.Id equals ex.SubjectId
                        join stEx in _db.StudentsExams.GetAll() on ex.Id equals stEx.ExamId
                        orderby ex.Date.Year
                        group new { s, ex, stEx } by ex.Date.Year into years
                        select from aDRep in years
                               group aDRep by new { aDRep.ex.Date.Year, aDRep.s.Name } into rep
                               select new AverageDynamicReport
                               {
                                   Year = rep.Key.Year,
                                   SubjectName = rep.Key.Name,
                                   Average = (from grades in rep select (double)grades.stEx.Grade).Average()
                               }).AsEnumerable().Select(q => q.OrderBy(keySelector)).AsQueryable();

            return query;
        }
    }
}
