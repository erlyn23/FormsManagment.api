using Microsoft.EntityFrameworkCore;
using SimpleJwt.DbContexts;
using SimpleJwt.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace SimpleJwt.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FormsManagmentDbContext _dbContext;

        public GenericRepository(FormsManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.AddAsync(entity);
            return entity;
        }

        public async Task<List<T>> AddRangeAsync(T[] entities)
        {
            await _dbContext.AddRangeAsync(entities);
            return entities.ToList();
        }

        public async Task<List<T>> GetAllAsync() => 
            await _dbContext.Set<T>().ToListAsync();

        public async Task<List<T>> GetManyWithFilterAsync(Expression<Func<T, bool>> expression) => 
            await _dbContext.Set<T>().Where(expression).ToListAsync();

        public async Task<T> GetOneAsync(Expression<Func<T, bool>> expression) => 
            await _dbContext.Set<T>().Where(expression).FirstOrDefaultAsync();

        public void Remove(T entity) => _dbContext.Set<T>().Remove(entity);

        public void RemoveRange(T[] entities) => _dbContext.Set<T>().RemoveRange(entities);
        public T Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public async Task<bool> SaveChangesAsync()
        {
            var isSaved = (await _dbContext.SaveChangesAsync() > 0) ? true : false;

            return isSaved;
        }
    }
}
