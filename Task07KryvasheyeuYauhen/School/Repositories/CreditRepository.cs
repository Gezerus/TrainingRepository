﻿using School.Models;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repositories
{
    /// <summary>
    /// describes the interaction with the credits table
    /// </summary>
    public class CreditRepository : Repository<Credit>
    {
        public CreditRepository(DataContext dataContext) : base(dataContext)
        { }

        /// <summary>
        /// updates a credit in the credits table
        /// </summary>
        /// <param name="credit"></param>
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
