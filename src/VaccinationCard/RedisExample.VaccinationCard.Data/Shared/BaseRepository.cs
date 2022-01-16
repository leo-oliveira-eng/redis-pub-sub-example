using Messages.Core;
using MongoDB.Driver;
using RedisExample.VaccinationCard.Data.Context.Contracts;
using RedisExample.VaccinationCard.Domain.Core.Events;
using RedisExample.VaccinationCard.Domain.Core.Mediator.Contracts;
using RedisExample.VaccinationCard.Domain.Models;
using RedisExample.VaccinationCard.Domain.Repositories;

namespace RedisExample.VaccinationCard.Data.Shared
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : Entity
    {
        protected readonly IMongoContext _context;

        protected readonly IMongoCollection<TEntity> _collection;

        private readonly IMediatorHandler _mediatorHandler;

        protected BaseRepository(IMongoContext context, IMediatorHandler mediatorHandler)
        {
            _context = context;
            _collection = _context.GetCollection<TEntity>(typeof(TEntity).Name);
            _mediatorHandler = mediatorHandler;
        }

        public async Task AddAsync(TEntity entity)
        {
            await _collection.InsertOneAsync(entity);
            await DispatchEvents(entity.DomainEvents.ToList()).ConfigureAwait(false);
        }

        public async Task<Maybe<TEntity>> FindAsync(Guid code)
            => await _collection.FindAsync(entity => entity.Id.Equals(code))
                .GetAwaiter()
                .GetResult()
                .FirstOrDefaultAsync();

        public async Task<List<TEntity>> GetAllAsync()
            => await _collection.FindAsync(_ => true)
                .GetAwaiter()
                .GetResult()
                .ToListAsync();

        public async Task<DeleteResult> RemoveAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);

            var deleteResult = await _collection.DeleteOneAsync(filter);

            await DispatchEvents(entity.DomainEvents.ToList()).ConfigureAwait(false);

            return deleteResult;
        }

        public async Task<ReplaceOneResult> UpdateAsync(TEntity entity)
        {
            var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);

            var updateResult = await _collection.ReplaceOneAsync(filter, entity);

            await DispatchEvents(entity.DomainEvents.ToList()).ConfigureAwait(false);

            return updateResult;
        }

        private async Task DispatchEvents(List<DomainEvent> events)
        {
            foreach (var domainEvent in events.Where(e => !e.IsPublished).ToList())
            {
                if (domainEvent is null) break;

                await _mediatorHandler.PublishEvent(domainEvent).ConfigureAwait(false);

                domainEvent.IsPublished = true;
            }
        }
    }
}
