using MediatR;
using Messages.Core;

namespace RedisExample.Registration.Domain.Commands
{
    public class DeleteHumanCommand : IRequest<Response>
    {
        public Guid HumanId { get; set; }

        public DeleteHumanCommand(Guid humanId)
        {
            HumanId = humanId;
        }
    }
}
