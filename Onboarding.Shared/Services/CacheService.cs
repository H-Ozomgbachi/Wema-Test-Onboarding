namespace Onboarding.Shared.Services
{
    public interface ICacheService
    {
        Task<string> GetFromCache(string key);
        Task SetCache(string key, string value, TimeSpan expiry);
        Task<bool> RemoveFromCache(string key);
    }
    public class CacheService : ICacheService
    {
        private readonly IDatabase _database;
        public CacheService(IConfiguration config)
        {
            string connStr = config["AppSettings:RedisConnection"];
            var connection = ConnectionMultiplexer.Connect(connStr);
            _database = connection.GetDatabase();
        }

        public async Task<string> GetFromCache(string key)
        {
            return await _database.StringGetAsync(key);
        }

        public async Task SetCache(string key, string value, TimeSpan expiry)
        {
            await _database.StringSetAsync(key, value, expiry);
        }

        public async Task<bool> RemoveFromCache(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }
    }
}
