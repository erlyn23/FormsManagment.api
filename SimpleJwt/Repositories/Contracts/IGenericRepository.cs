using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleJwt.Repositories.Contracts
{
    public interface IGenericRepository <T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetManyWithFilterAsync(Expression<Func<T, bool>> expression);
        Task<T> GetOneAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<List<T>> AddRangeAsync(T[] entities);
        T Update(T entity);
        void Remove(T entity);
        void RemoveRange(T[] entities);
        Task<bool> SaveChangesAsync();
    }
}
