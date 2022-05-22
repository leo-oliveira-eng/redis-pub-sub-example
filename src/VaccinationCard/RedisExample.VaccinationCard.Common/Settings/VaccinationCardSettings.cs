using RedisExample.VaccinationCard.Common.Settings.Contracts;

namespace RedisExample.VaccinationCard.Common.Settings
{
    public class VaccinationCardSettings : ISettings
    {
        public MongoDbSettings MongoDbSettings { get; set; } = null!;

        public RedisSettings RedisSettings { get; set; } = null!;
    }
}
