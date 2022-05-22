namespace RedisExample.VaccinationCard.Common.Settings.Contracts
{
    public interface ISettings
    {
        MongoDbSettings MongoDbSettings { get; set; }

        RedisSettings RedisSettings { get; set; }
    }
}
