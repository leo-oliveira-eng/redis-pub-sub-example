using BaseEntity.Domain.Mediator.Contracts;
using Microsoft.EntityFrameworkCore;
using RedisExample.Registration.Domain.Models;

namespace RedisExample.Registration.Persistence.Context.Extensions
{
    public static class MediatorExtension
    {
        public static async Task PublishDomainEvents<TContext>(this IMediatorHandler mediator, TContext context) where TContext : DbContext
        {
            var domainEntities = context.ChangeTracker
                .Entries<Human>()
                .Where(x => x.Entity?.DomainEvents.Count > 0);

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ToList()
                .ForEach(entity => entity.Entity.ClearDomainEvents());

            var tasks = domainEvents
                .Select(async (domainEvent) => await mediator.PublishEvent(domainEvent));

            await Task.WhenAll(tasks).ConfigureAwait(false);
        }
    }
}
