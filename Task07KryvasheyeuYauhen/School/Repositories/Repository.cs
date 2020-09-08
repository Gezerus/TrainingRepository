using System;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;

namespace School.Repositories
{
    /// <summary>
    /// describes the interaction with each table
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Repository<T> where T : class
    {
        protected DataContext _dataContext;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
            
        }
        /// <summary>
        /// inserts a model to a table
        /// </summary>
        /// <param name="model"></param>
        public void Insert(T model) 
        {
            _dataContext.GetTable<T>().InsertOnSubmit(model);
            _dataContext.SubmitChanges();
        }

        /// <summary>
        /// gets all models from a table
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            return _dataContext.GetTable<T>();
        }

        /// <summary>
        /// gets all models from a table by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T GetById(int id)
        {
            var itemParameter = Expression.Parameter(typeof(T), "item");
            var whereExpression = Expression.Lambda<Func<T, bool>>(
                Expression.Equal(
                    Expression.Property(
                        itemParameter,
                        typeof(T).GetProperty("Id").Name
                        ),
                    Expression.Constant(id)
                    ),
                new[] { itemParameter }
                );
            return GetAll().Where(whereExpression).Single();
        }

        /// <summary>
        /// deletes a model from a table
        /// </summary>
        /// <param name="model"></param>
        public void Delete(T model)
        {
            _dataContext.GetTable<T>().DeleteOnSubmit(model);
            _dataContext.SubmitChanges();
        }

        public abstract void Update(T model);
    }
}
