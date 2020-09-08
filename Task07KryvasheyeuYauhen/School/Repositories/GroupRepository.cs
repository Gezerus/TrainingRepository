using School.Models;
using System.Data.Linq;

namespace School.Repositories
{
    /// <summary>
    /// describes the interaction with the groups table
    /// </summary>
    public class GroupRepository : Repository<Group>
    {
        public GroupRepository(DataContext dataContext) : base(dataContext)
        { }

        /// <summary>
        /// updates a credit in the credits table
        /// </summary>
        /// <param name="group"></param>
        public override void Update(Group group)
        {
            var newGroup = GetById(group.Id);
            newGroup.Name = group.Name;
            newGroup.Specialty = group.Specialty;
            _dataContext.SubmitChanges();
        }
    }
}
