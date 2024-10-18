using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSpace.Domain.Entities.Base;
using TeamSpace.Domain.Repositories.Base;
using TeamSpace.Domain.Specifications.Base;
using TeamSpace.Infraestructure.Context;

namespace TeamSpace.Infraestructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly TeamSpaceDbContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public Repository(TeamSpaceDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(Guid id)
        {
            var entity = _dbSet.Find(id);
            if (entity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            }
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id).AsTask();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await _dbSet.Where(spec.Criteria).ToListAsync();
        }

        public Task<T> UpdateAsync(Guid id, T entity)
        {
            var currentEntity = _dbSet.Find(id);
            if (currentEntity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            }
            _dbContext.Entry(currentEntity).CurrentValues.SetValues(entity);
            _dbSet.Update(currentEntity);
            return Task.FromResult(currentEntity);
        }

        public Task<T> UpdateAsync(T entity, params object[] keyValues)
        {
            var currentEntity = _dbSet.Find(keyValues);
            if (currentEntity == null)
            {
                throw new KeyNotFoundException("Entity not found");
            }
            _dbContext.Entry(currentEntity).CurrentValues.SetValues(entity);
            _dbSet.Update(currentEntity);
            return Task.FromResult(currentEntity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public Task Update(T entity)
        {
            var currentEntity = _dbSet.Update(entity);
            return Task.FromResult(currentEntity);
        }
    }
}
