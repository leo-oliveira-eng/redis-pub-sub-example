using MongoDB.Driver;

namespace RedisExample.VaccinationCard.Data.Context.Contracts
{
    public interface IMongoContext
    {
        IMongoCollection<T> GetCollection<T>(string name);
    }
}
