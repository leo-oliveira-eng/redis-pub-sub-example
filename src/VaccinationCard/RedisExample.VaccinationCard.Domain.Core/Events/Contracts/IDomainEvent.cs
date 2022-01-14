namespace RedisExample.VaccinationCard.Domain.Core.Events.Contracts
{
    public interface IDomainEvent
    {
        DateTimeOffset DateOccurred { get; }
    }
}
