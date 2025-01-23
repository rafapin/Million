using Million.Domain.Features.Shared.Entities;
using System.Linq.Expressions;

namespace Million.Domain.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : Entity, new()
    {
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> SaveAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(string id);
        Task<TEntity> GetFirst(Expression<Func<TEntity, bool>>? predicate = null,
            bool disableTracking = true);
        Task<List<TEntity>> GetAllAsync();
        Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate ,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            List<Expression<Func<TEntity, object>>>? includes = null,
            bool disableTracking = true);
    }
}
