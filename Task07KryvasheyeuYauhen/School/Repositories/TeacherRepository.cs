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
    /// describes the interaction with the Teachers table
    /// </summary>
    public class TeacherRepository : Repository<Teacher>
    {
        public TeacherRepository(DataContext dataContext) : base(dataContext)
        { }

        /// <summary>
        /// updates a teacher in the Teachers table
        /// </summary>
        /// <param name="teacher"></param>
        public override void Update(Teacher teacher)
        {
            var newTeacher = GetById(teacher.Id);
            newTeacher.Name = teacher.Name;            
            _dataContext.SubmitChanges();
        }
    }
}
