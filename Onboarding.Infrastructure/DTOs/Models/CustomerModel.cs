namespace Onboarding.Infrastructure.DTOs.Models
{
    public record CustomerModel : BaseModel
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string StateOfResidence { get; set; }
        public string LocalGovernmentArea { get; set; }
    }
}
