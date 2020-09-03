using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repositories
{
    public class CreditRepository : Repository<Credit>
    {
        public CreditRepository(DataContext dataContext) : base(dataContext)
        { }

        public override void Update(Credit credit)
        {
            var newCredit = GetById(credit.Id);
            newCredit.SubjectId = credit.SubjectId;
            newCredit.SessionId = credit.SessionId;
            newCredit.Date = credit.Date;
            newCredit.TeacherId = credit.TeacherId;
            _dataContext.SubmitChanges();
        }
    }
}
