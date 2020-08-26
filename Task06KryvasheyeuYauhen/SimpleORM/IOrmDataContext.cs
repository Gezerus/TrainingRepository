using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleORM
{
    interface IOrmDataContext
    {
        IEnumerable<T> Query<T>(string sqlString);

        IEnumerable<T> GetAll<T>();


    }
}
