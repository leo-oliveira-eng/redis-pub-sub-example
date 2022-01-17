using Messages.Core;
using MongoDB.Driver;
using RedisExample.VaccinationCard.Domain.Models;

namespace RedisExample.VaccinationCard.Domain.Repositories
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task AddAsync(TEntity entity);

        Task<Maybe<TEntity>> FindAsync(Guid code);

        Task<List<TEntity>> GetAllAsync();

        Task<DeleteResult> RemoveAsync(TEntity entity);

        Task<ReplaceOneResult> UpdateAsync(TEntity entity);
    }
}
