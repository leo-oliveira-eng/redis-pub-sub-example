namespace RedisExample.VaccinationCard.Common.Settings
{
    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public MongoDbCollectionNames CollectionNames { get; set; } = null!;
    }
}
