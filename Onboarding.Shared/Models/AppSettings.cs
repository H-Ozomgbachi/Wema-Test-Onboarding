namespace Onboarding.Shared.Models
{
    public record AppSettings
    {
        public string DbConnection { get; set; }
        public string AllowedOrigins { get; set; }
        public string RedisConnection { get; set; }
        public string AllBanksUrl { get; set; }
        public string EKey { get; set; }
        public string EIv { get; set; }
    }
}
