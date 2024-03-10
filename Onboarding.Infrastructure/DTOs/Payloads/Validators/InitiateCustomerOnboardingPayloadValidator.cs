namespace Onboarding.Infrastructure.DTOs.Payloads.Validators
{
    public class InitiateCustomerOnboardingPayloadValidator : AbstractValidator<InitiateCustomerOnboardingPayload>
    {
        private readonly IConfiguration _config;

        public InitiateCustomerOnboardingPayloadValidator(IConfiguration config)
        {
            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .MaximumLength(13).WithMessage("Phone number cannot exceed")
                .Must(c => c.All(x => char.IsDigit(x))).WithMessage("Phone number must contain only digits");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Please provide a valid email");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Password is required");

            RuleFor(c => c.StateOfResidence)
                .NotEmpty().WithMessage("State of residence is required")
                .Must(c => ValidateState(c)).WithMessage("Invalid Nigerian state selected");

            When(c => !string.IsNullOrEmpty(c.StateOfResidence), () =>
            {
                RuleFor(c => c.LocalGovernmentArea)
                .NotEmpty().WithMessage("LGA is required")
                .Must((model, lga) => ValidateLga(model.StateOfResidence, lga)).WithMessage("Invalid local govt area for the selected state");
            });
            _config = config;
        }

        private bool ValidateLga(string stateOfResidence, string lga)
        {
            string[] lgas = _config.GetSection($"StatesAndLga:{stateOfResidence}").Get<string[]>();

            return lgas.Contains(lga);
        }

        public bool ValidateState(string input)
        {;
            return _config.GetSection("StatesAndLga").GetSection(input).Exists();
        }
    }
}
