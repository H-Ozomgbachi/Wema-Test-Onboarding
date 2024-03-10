namespace Onboarding.Infrastructure.DTOs.Payloads.Validators
{
    public class CompleteCustomerOnboardingPayloadValidator : AbstractValidator<CompleteCustomerOnboardingPayload>
    {
        public CompleteCustomerOnboardingPayloadValidator()
        {
            RuleFor(c => c.Otp)
                .NotEmpty().WithMessage("Otp is required")
                .Length(6).WithMessage("Otp must be six(6) characters long")
                .Must(c => c.All(x => char.IsDigit(x))).WithMessage("Otp must contain only digits");

            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage("Phone number is required")
                .MaximumLength(13).WithMessage("Phone number cannot exceed")
                .Must(c => c.All(x => char.IsDigit(x))).WithMessage("Phone number must contain only digits");
        }
    }
}
