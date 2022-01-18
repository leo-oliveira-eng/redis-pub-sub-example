using RedisExample.Registration.Common.Settings.Contracts;

namespace RedisExample.Registration.Common.Settings
{
    public class RegistrationSettings : ISettings
    {
        public RedisSettings RedisSettings { get; set; } = null!;
    }
}
