namespace Onboarding.Infrastructure.Mappers
{
    public class CustomerMappings : Profile
    {
        public CustomerMappings()
        {
            CreateMap<InitiateCustomerOnboardingPayload, Customer>().ReverseMap();
            CreateMap<CustomerModel, Customer>().ReverseMap();
        }
    }
}
