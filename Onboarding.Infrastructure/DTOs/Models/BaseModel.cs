namespace Onboarding.Infrastructure.DTOs.Models
{
    public record BaseModel
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }
}
