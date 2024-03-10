namespace Onboarding.Infrastructure.DTOs.Payloads
{
    public record InitiateCustomerOnboardingPayload
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string StateOfResidence { get; set; }
        public string LocalGovernmentArea { get; set; }
    }
}
