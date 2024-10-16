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
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return _dbSet.FindAsync(id).AsTask();
        }

        public Task<IReadOnlyList<T>> ListAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await _dbSet.Where(spec.Criteria).ToListAsync();
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(Guid id, T entity)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity, params object[] keyValues)
        {
            throw new NotImplementedException();
        }
    }
}
