using School.Models;
using System.Data.Linq;

namespace School.Repositories
{
    /// <summary>
    /// describes the interaction with the Students table
    /// </summary>
    public class StudentRepository : Repository<Student>
    {
        public StudentRepository(DataContext dataContext) : base(dataContext)
        { }

        /// <summary>
        /// updates a student in the Students table
        /// </summary>
        /// <param name="student"></param>
        public override void Update(Student student)
        {
            var newStudent = GetById(student.Id);
            newStudent.Birthday = student.Birthday;
            newStudent.Gender = student.Gender;
            newStudent.Name = student.Name;
            newStudent.GroupId = student.GroupId;
            _dataContext.SubmitChanges();
        }
    }
}
