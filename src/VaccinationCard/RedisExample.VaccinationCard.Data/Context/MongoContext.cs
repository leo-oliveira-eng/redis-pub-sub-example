using MongoDB.Driver;
using RedisExample.VaccinationCard.Common.Settings.Contracts;
using RedisExample.VaccinationCard.Data.Context.Contracts;

namespace RedisExample.VaccinationCard.Data.Context
{
    public class MongoContext : IMongoContext
    {
        private IMongoDatabase Database { get; }

        public MongoClient MongoClient { get; set; }

        public MongoContext(ISettings settings)
        {
            MongoClient = new MongoClient(settings.MongoDbSettings.ConnectionString);

            Database = MongoClient.GetDatabase(settings.MongoDbSettings.DatabaseName);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
            => Database.GetCollection<T>(name);
    }
}
