using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Projeto.Iris.Repositories
{
    public interface IGenericRepository<T>
    {
        void Insert(T entity);
        void Update(T entity);
        void Remove(int id);
        T FindById(int id);
        IList<T> ListAll();
        IList<T> Query(Expression<Func<T, bool>> filter);
        void Save();
    }
}
