using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess_Layer.Interfaces
{
    public interface IBaseRepo<T> where T : class
    {
        ValueTask<int> UpdateSerialNumberAndDateTime(string id);

        ValueTask CreateAsync(T entity);
        ValueTask UpdateAsync(T entity);
        ValueTask DeleteAsync(string id);
        ValueTask<T> GetByIdAsync(string id);
        ValueTask<IQueryable<T>> GetAllAsync( Expression<Func<T, object>>[] includes=null);
        ValueTask<IQueryable<T>> GetWithQuery(Func<IQueryable<T>, IQueryable<T>> query);
    }
}
