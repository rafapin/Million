using Million.Domain.Contracts;
using Million.Domain.Features.Shared.Entities;
using MongoDB.Driver;
using SharpCompress.Common;
using System.Linq.Expressions;

namespace Million.Infrastructure.Repositories
{
    public class MongoRepository<TEntity> : IGenericRepository<TEntity> where TEntity : Entity, new()
    {
        private readonly IMongoCollection<TEntity> _collection;

        public MongoRepository(IMongoDatabase database)
        {
            var collectionName = typeof(TEntity).Name; // Usa el nombre del tipo genérico como el nombre de la colección
            _collection = database.GetCollection<TEntity>(collectionName);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.GenerateId(); 
            await _collection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
            var result = await _collection.ReplaceOneAsync(filter, entity);

            if (result.IsAcknowledged && result.ModifiedCount > 0)
            {
                return entity;
            }

            throw new Exception($"Entity with Id {entity.Id} not updated.");
        }

        public async Task<TEntity> SaveAsync(TEntity entity)
        {
            if (string.IsNullOrEmpty(entity.Id))
            {
                return await AddAsync(entity);
            }

            return await UpdateAsync(entity);
        }

        public async Task<TEntity> GetByIdAsync(string id)
        {
            return await _collection.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<TEntity> GetFirst(Expression<Func<TEntity, bool>>? predicate = null, bool disableTracking = true)
        {
            return await _collection.Find(predicate ?? (e => true)).FirstOrDefaultAsync();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<List<TEntity>> GetAsync(
            Expression<Func<TEntity, bool>> predicate,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>? orderBy = null,
            List<Expression<Func<TEntity, object>>>? includes = null,
            bool disableTracking = true)
        {
            var query = _collection.AsQueryable();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            // MongoDB does not support includes in the same way as relational databases. You can implement joins manually if needed.

            return await Task.FromResult(query.ToList());
        }
    }
}
