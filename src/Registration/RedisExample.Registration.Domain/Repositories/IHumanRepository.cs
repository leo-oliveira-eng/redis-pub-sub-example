using BaseEntity.Domain.Repositories;
using RedisExample.Registration.Domain.Models;

namespace RedisExample.Registration.Domain.Repositories
{
    public interface IHumanRepository : IRepository<Human> 
    {
        Task Delete(Human human);
    }
}
