using School.Models;
using System.Data.Linq;

namespace School.Repositories
{
    /// <summary>
    /// describes the interaction with the Subjects table
    /// </summary>
    public class SubjectRepository : Repository<Subject>
    {
        public SubjectRepository(DataContext dataContext) : base(dataContext)
        { }

        /// <summary>
        /// updates a subject in the Subjects table
        /// </summary>
        /// <param name="subject"></param>
        public override void Update(Subject subject)
        {
            var newSubject = GetById(subject.Id);
            newSubject.Name = subject.Name;            
            _dataContext.SubmitChanges();
        }
    }
}
