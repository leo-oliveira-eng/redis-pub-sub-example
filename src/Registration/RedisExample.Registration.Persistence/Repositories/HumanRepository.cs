using Infrastructure.Repositories;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Domain.Repositories;
using RedisExample.Registration.Persistence.Context;

namespace RedisExample.Registration.Persistence.Repositories
{
    public class HumanRepository : Repository<Human, RegistrationContext>, IHumanRepository
    {
        public HumanRepository(RegistrationContext context) : base(context) { }
    }
}
