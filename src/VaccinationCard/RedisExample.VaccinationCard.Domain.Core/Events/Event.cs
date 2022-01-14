using MediatR;

namespace RedisExample.VaccinationCard.Domain.Core.Events
{
    public abstract class Event : INotification
    {
        public DateTimeOffset DateOccurred { get; protected set; } = DateTime.UtcNow;

        protected Event()
        {
            DateOccurred = DateTimeOffset.UtcNow;
        }
    }
}
