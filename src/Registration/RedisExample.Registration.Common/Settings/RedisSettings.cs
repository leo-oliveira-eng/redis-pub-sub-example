namespace RedisExample.Registration.Common.Settings
{
    public class RedisSettings
    {
        public string Endpoint { get; set; } = null!;

        public int Port { get; set; }

        public string Password { get; set; } = null!;

        public IDictionary<string, string> Topics { get; set; } = new Dictionary<string, string>();
    }
}
