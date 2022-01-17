using RedisExample.VaccinationCard.Domain.Core.Events;
using RedisExample.VaccinationCard.Domain.Models;

namespace RedisExample.VaccinationCard.Domain.Events
{
    public class HumanCreatedEvent : DomainEvent
    {
        public Human Human { get; set; }

        public HumanCreatedEvent(Human human)
            : base(human.Id)
        {
            Human = human;
        }
    }
}
