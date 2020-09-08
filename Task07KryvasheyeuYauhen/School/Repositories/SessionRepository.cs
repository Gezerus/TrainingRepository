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
    /// describes the interaction with the sessions table
    /// </summary>
    public class SessionRepository : Repository<Session>
    {
        public SessionRepository(DataContext dataContext) : base(dataContext)
        { }

        /// <summary>
        /// updates a session in the sessions table
        /// </summary>
        /// <param name="session"></param>
        public override void Update(Session session)
        {
            var newSession = GetById(session.Id);
            newSession.Name = session.Name;
            newSession.StartDate = session.StartDate;
            _dataContext.SubmitChanges();
        }
    }
}
