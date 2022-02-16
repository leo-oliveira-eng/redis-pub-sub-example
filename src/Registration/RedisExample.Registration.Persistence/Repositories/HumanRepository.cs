#pragma warning disable CS8604 // Possible null reference argument.
using Infrastructure.Repositories;
using Messages.Core;
using Microsoft.EntityFrameworkCore;
using RedisExample.Registration.Domain.Models;
using RedisExample.Registration.Domain.Repositories;
using RedisExample.Registration.Persistence.Context;

namespace RedisExample.Registration.Persistence.Repositories
{
    public class HumanRepository : Repository<Human, RegistrationContext>, IHumanRepository
    {
        public HumanRepository(RegistrationContext context) : base(context) { }

        public override async Task<Maybe<Human>> FindAsync(Guid code)
            => await DbSet
                .Include(human => human.Pets)
                    .ThenInclude(pet => pet.Vaccines)
                .SingleOrDefaultAsync(human => human.Code.Equals(code));
    }
}
#pragma warning restore CS8604 // Possible null reference argument.
