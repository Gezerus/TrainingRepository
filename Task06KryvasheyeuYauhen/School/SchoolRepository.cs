using SimpleORM;
using System.Collections.Generic;
using System.Configuration;

namespace School
{
    /// <summary>
    /// implements interaction with the school database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SchoolRepository
    {
        private OrmDataContext _dbContext;
        public SchoolRepository()
        {
            _dbContext = OrmDataContext.Initialize(ConfigurationManager.ConnectionStrings["SchoolConnection"].ConnectionString);
        }


        /// <summary>
        /// updae the model in the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update<T>(T model)
        {
            if (_dbContext.Update(model) == 1)
                return true;
            return false;
        }

        /// <summary>
        /// inserts the model to the database
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Insert<T>(T model )
        {
            if (_dbContext.Insert(model) == 1)
                return true;
            return false;
        }

        /// <summary>
        /// deletes the model from database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete<T>(T model)
        {
            if (_dbContext.Delete(model) == 1)
                return true;
            return false;
        }

        public IEnumerable<Student> Students
        {
            get
            {
                return _dbContext.GetAll <Student> ();
            }
        }

        public IEnumerable<Group> Groups
        {
            get
            {
                return _dbContext.GetAll<Group>();
            }
        }

        public IEnumerable<Session> Sessions        
        {
            get
            {
                return _dbContext.GetAll<Session>();
            }
        }

        public IEnumerable<Subject> Subjects
        {
            get
            {
                return _dbContext.GetAll<Subject>();
            }
        }

        public IEnumerable<Exam> Exams
        {
            get
            {
                return _dbContext.GetAll<Exam>();
            }
        }

        public IEnumerable<Credit> Credits
        {
            get
            {
                return _dbContext.GetAll<Credit>();
            }
        }

        public IEnumerable<StudentExam> StudentsExams
        {
            get
            {
                return _dbContext.GetAll<StudentExam>();
            }
        }

        public IEnumerable<StudentCredit> StudentsCredits
        {
            get
            {
                return _dbContext.GetAll<StudentCredit>();
            }
        }

    }
}
