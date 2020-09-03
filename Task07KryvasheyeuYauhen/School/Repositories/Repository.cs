using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace School.Repositories
{
    public abstract class Repository<T> where T : class
    {
        protected DataContext _dataContext;

        public Repository(DataContext dataContext)
        {
            _dataContext = dataContext;
            
        }

        public void Insert(T model) 
        {
            _dataContext.GetTable<T>().InsertOnSubmit(model);
            _dataContext.SubmitChanges();
        }

        public IQueryable<T> GetAll()
        {
            return _dataContext.GetTable<T>();
        }

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

        public void Delete(T model)
        {
            _dataContext.GetTable<T>().DeleteOnSubmit(model);
            _dataContext.SubmitChanges();
        }

        public abstract void Update(T model);
    }
}
