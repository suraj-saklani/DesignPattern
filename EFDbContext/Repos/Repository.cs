﻿using Microsoft.EntityFrameworkCore;
using workingProject.EfDbContext.DBUnitOfWork;
using workingProject.EFDbContext;

namespace workingProject.EfDbContext.Repos
{
    public class Repository<T>: IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext appDbContext)
        {
            _dbSet = appDbContext.Set<T>();
        }
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
            }
        }
    }
}
