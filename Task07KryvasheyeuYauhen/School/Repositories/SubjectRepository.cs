using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repositories
{
    public class SubjectRepository : Repository<Subject>
    {
        public SubjectRepository(DataContext dataContext) : base(dataContext)
        { }

        public override void Update(Subject subject)
        {
            var newSubject = GetById(subject.Id);
            newSubject.Name = subject.Name;            
            _dataContext.SubmitChanges();
        }
    }
}
