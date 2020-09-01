using School.Reports;
using System;
using System.Collections.Generic;
using System.Linq;
using School;


namespace School
{
    /// <summary>
    /// Report generator from  the school database 
    /// </summary>
    public  class ReportGenerator
    {
        private SchoolRepository _schoolDb = new SchoolRepository();
        /// <summary>
        /// gets students subject to expulsion from database
        /// </summary>
        /// <returns>the students subject to explusion</returns>
        public IEnumerable<IEnumerable<LosersReport>> LosersReportGenerate()
        {
            var query = from StEx in _schoolDb.StudentsExams
                        join St in _schoolDb.Students on StEx.StudentId equals St.Id
                        join StCr in _schoolDb.StudentsCredits on St.Id equals StCr.StudentId
                        join Gr in _schoolDb.Groups on St.GroupId equals Gr.Id
                        where StCr.Result == false || StEx.Grade < 4
                        orderby Gr.Id
                        group new { StEx, St, StCr, Gr } by Gr.Id into groups
                        select from lRep in groups
                               group lRep by new {lRep.St.Id, lRep.St.Name, GroupId = lRep.Gr.Id} into rep
                                select new LosersReport
                                {
                                    Id = rep.Key.Id,
                                    Name = rep.Key.Name,
                                    GroupId = rep.Key.GroupId
                                };

            return query;
        }

        /// <summary>
        /// gets students subject to expulsion from database sorted by keySelector
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns>the students subject to explusion</returns>
        public IEnumerable<IEnumerable<LosersReport>> LosersReportGenerate<TKey>(Func<LosersReport, TKey> keySelector)
        {
           

            var query = from StEx in _schoolDb.StudentsExams
                        join St in _schoolDb.Students on StEx.StudentId equals St.Id
                        join StCr in _schoolDb.StudentsCredits on St.Id equals StCr.StudentId
                        join Gr in _schoolDb.Groups on St.GroupId equals Gr.Id
                        where StCr.Result == false || StEx.Grade < 4
                        orderby Gr.Id
                        group new { StEx, St, StCr, Gr } by Gr.Id into groups
                        select (from lRep in groups
                               group lRep by new { lRep.St.Id, lRep.St.Name, GroupId = lRep.Gr.Id } into rep
                               select new LosersReport
                               {
                                   Id = rep.Key.Id,
                                   Name = rep.Key.Name,
                                   GroupId = rep.Key.GroupId
                               }).OrderBy(keySelector);

            return query;
        }

        /// <summary>
        /// gets the minimum, maximum and average score for each group
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IEnumerable<GroupResultReport>> SessinResultGenerate()
        {
            
            var query = from StEx in _schoolDb.StudentsExams
                        join Ex in _schoolDb.Exams on StEx.ExamId equals Ex.Id
                        join St in _schoolDb.Students on StEx.StudentId equals St.Id
                        join Gr in _schoolDb.Groups on St.GroupId equals Gr.Id
                        join Ss in _schoolDb.Sessions on Ex.SessionId equals Ss.Id
                        group new { Ex, St, Gr, Ss, StEx } by Ss.Id into sessions
                        select from gr in sessions group gr by  gr.Gr.Id  into rep
                                select new GroupResultReport
                                {
                                    SessionId = (from ss in rep select ss.Ss.Id).First(),
                                    GroupId = rep.Key,
                                    Min = (from grades in rep select grades.StEx.Grade).Min(),
                                    Max = (from grades in rep select grades.StEx.Grade).Max(),
                                    Average = (from grades in rep select grades.StEx.Grade).Average()
                                };

                        
            return query;
        }

        /// <summary>
        /// gets the minimum, maximum and average score for each group sorted by keSelector
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public IEnumerable<IEnumerable<GroupResultReport>> SessinResultGenerate<TKey>(Func<GroupResultReport, TKey> keySelector)
        {           

            var query = from StEx in _schoolDb.StudentsExams
                        join Ex in _schoolDb.Exams on StEx.ExamId equals Ex.Id
                        join St in _schoolDb.Students on StEx.StudentId equals St.Id
                        join Gr in _schoolDb.Groups on St.GroupId equals Gr.Id
                        join Ss in _schoolDb.Sessions on Ex.SessionId equals Ss.Id
                        group new { Ex, St, Gr, Ss, StEx } by Ss.Id into sessions
                        select (from gr in sessions
                               group gr by gr.Gr.Id into rep
                               select new GroupResultReport
                               {
                                   SessionId = (from ss in rep select ss.Ss.Id).First(),
                                   GroupId = rep.Key,
                                   Min = (from grades in rep select grades.StEx.Grade).Min(),
                                   Max = (from grades in rep select grades.StEx.Grade).Max(),
                                   Average = (from grades in rep select grades.StEx.Grade).Average()
                               }).OrderBy(keySelector);


            return query;
        }
    }
}
