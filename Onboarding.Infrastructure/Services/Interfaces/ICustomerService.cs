namespace Onboarding.Infrastructure.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<BaseResponse<string>> InitiateCustomerOnboarding(InitiateCustomerOnboardingPayload createCustomerPayload, CancellationToken cancellationToken);
        Task<BaseResponse<string>> CompleteCustomerOnboarding(CompleteCustomerOnboardingPayload completeCustomerOnboarding, CancellationToken cancellationToken);
        Task<BaseResponse<List<CustomerModel>>> GetCustomers(CancellationToken cancellationToken);
        Task<BaseResponse<List<BankModel>>> GetAllBanks(CancellationToken cancellationToken);
    }
}
