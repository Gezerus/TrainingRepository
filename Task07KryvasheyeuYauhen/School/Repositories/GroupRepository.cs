using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repositories
{
    public class GroupRepository : Repository<Group>
    {
        public GroupRepository(DataContext dataContext) : base(dataContext)
        { }

        public override void Update(Group group)
        {
            var newGroup = GetById(group.Id);
            newGroup.Name = group.Name;
            newGroup.Specialty = group.Specialty;
            _dataContext.SubmitChanges();
        }
    }
}
