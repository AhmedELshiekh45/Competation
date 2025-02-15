using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DataAccess_Layer.Context;
using DataAccess_Layer.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BL_Layer.Repos
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly MyContext _context;
        private DbSet<T> _dbSet;

        public BaseRepo(MyContext context)
        {
            this._context = context;
            _dbSet = context.Set<T>();
        }
        public async ValueTask CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);

        }

        public async ValueTask DeleteAsync(string id)
        {
            var entity = await GetByIdAsync(id);
            _dbSet.Remove(entity);
        }

        public virtual async ValueTask<IQueryable<T>> GetAllAsync(Expression<Func<T, object>>[] includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }

            }

            return query;
        }

        public async ValueTask<IQueryable<T>> GetWithQuery(Func<IQueryable<T>, IQueryable<T>> query)
        {
            IQueryable<T> baseQuery = _dbSet;
            return query(baseQuery);
        }

        public virtual async ValueTask<T> GetByIdAsync(string id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async ValueTask UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }


        public async ValueTask<int> UpdateSerialNumberAndDateTime(string id)
        {
            var idParam = new SqlParameter
            {
                ParameterName = "@Id",
                SqlDbType = System.Data.SqlDbType.NVarChar,
                Size = 50,
                Value = id
            };

            var newSerialNumberParam = new SqlParameter
            {
                ParameterName = "@NewSerialNumber",
                SqlDbType = System.Data.SqlDbType.Int,
                Direction = System.Data.ParameterDirection.Output
            };

            // تنفيذ الإجراء المخزن مع تمرير المعاملات بشكل صحيح
            await _context.Database.ExecuteSqlRawAsync("EXEC UpdateSerialNumberAndDateTime @Id, @NewSerialNumber OUTPUT", idParam, newSerialNumberParam);

            // استرجاع القيمة الجديدة
            return (int)newSerialNumberParam.Value;
        }

    }
}
