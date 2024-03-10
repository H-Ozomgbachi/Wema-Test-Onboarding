namespace Onboarding.Infrastructure.DTOs.Models
{
    public record BankModel
    {
        [JsonProperty("bankName")]
        public string BankName { get; set; }

        [JsonProperty("bankCode")]
        public string BankCode { get; set; }
    }
}
