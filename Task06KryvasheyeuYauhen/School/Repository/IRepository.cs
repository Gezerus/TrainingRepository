using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository
{
    /// <summary>
    /// describes methods of interacting with the database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        IEnumerable<T> GetAll();

        bool Save(T model);

        bool Delete(T model);

    }
}
