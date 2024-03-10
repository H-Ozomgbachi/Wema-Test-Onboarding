namespace Onboarding.Shared.Models
{
    public record RabbitMqSettings
    {
        public string RootUri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string QueueName { get; set; }
    }
}
