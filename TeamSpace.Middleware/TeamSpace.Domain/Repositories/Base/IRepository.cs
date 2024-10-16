using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSpace.Domain.Entities.Base;
using TeamSpace.Domain.Specifications.Base;

namespace TeamSpace.Domain.Repositories.Base
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetByIdAsync(Guid id);
        Task<IReadOnlyList<TEntity>> ListAllAsync();
        Task<IReadOnlyList<TEntity>> ListAsync(ISpecification<TEntity> spec);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> UpdateAsync(
            Guid id,
            TEntity entity);
        Task<TEntity> UpdateAsync(
            TEntity entity,
            params object[] keyValues);
        Task DeleteAsync(Guid id);
        Task<int> SaveChangesAsync();
    }
}
