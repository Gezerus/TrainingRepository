using SimpleORM;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Repository
{
    /// <summary>
    /// implements interaction with the school database
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Repository<T> : IRepository<T>
    {
        private OrmDataContext _dbContext;
        public Repository()
        {
            _dbContext = OrmDataContext.Initialize("Data Source=(local);Initial Catalog=School;Integrated Security=True;");
        }

        /// <summary>
        ///  gets all models of type T from the database
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetAll()
        {
            return _dbContext.GetAll<T>();
        }

        /// <summary>
        /// inserts the model in database if it does not exist or updates if it does
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Save(T model)
        {
            if (_dbContext.Update(model) == 1)
                return true;
            else            
                if (_dbContext.Insert(model) == 1)
                    return true;
            return false;
        }

        /// <summary>
        /// deletes the model from database
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Delete(T model)
        {
            if (_dbContext.Delete(model) == 1)
                return true;
            return false;
        }

    }
}
