namespace Onboarding.Infrastructure.DTOs.Models
{
    public record OnboardingOtpModel
    {
        public string Otp { get; set; }
        public string Email { get; set; }
        public DateTimeOffset Expires { get; set; }
    }
}
