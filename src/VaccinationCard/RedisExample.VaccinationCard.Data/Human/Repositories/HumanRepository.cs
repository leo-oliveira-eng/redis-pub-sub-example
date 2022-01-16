using RedisExample.VaccinationCard.Data.Context.Contracts;
using RedisExample.VaccinationCard.Data.Shared;
using RedisExample.VaccinationCard.Domain.Core.Mediator.Contracts;
using RedisExample.VaccinationCard.Domain.Repositories;
using Model = RedisExample.VaccinationCard.Domain.Models;

namespace RedisExample.VaccinationCard.Data.Human.Repositories
{
    public class HumanRepository : BaseRepository<Model.Human> ,IHumanRepository
    {
        public HumanRepository(IMongoContext context, IMediatorHandler handler)
            : base(context, handler) { }
    }
}
