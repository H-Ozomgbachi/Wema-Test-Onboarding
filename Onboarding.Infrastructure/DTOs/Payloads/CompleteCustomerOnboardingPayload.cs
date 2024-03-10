﻿namespace Onboarding.Infrastructure.DTOs.Payloads
{
    public record CompleteCustomerOnboardingPayload
    {
        public string Otp { get; set; }
        public string PhoneNumber { get; set; }
    }
}
