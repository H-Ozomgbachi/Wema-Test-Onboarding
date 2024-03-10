namespace Onboarding.Domain.Entities
{
    public record BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}
