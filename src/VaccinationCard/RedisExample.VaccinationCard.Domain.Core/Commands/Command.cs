using Messages.Core;

namespace RedisExample.VaccinationCard.Domain.Core.Commands
{
    public abstract class Command
    {
        public abstract Response Validate();
    }
}
