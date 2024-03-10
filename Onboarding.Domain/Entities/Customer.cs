namespace Onboarding.Domain.Entities
{
    public record Customer : BaseEntity
    {
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string SecurityStamp { get; set; }
        public string StateOfResidence { get; set; }
        public string LocalGovernmentArea { get; set; }
    }
}
