namespace Onboarding.Domain
{
    public interface IUnitWork
    {
        ICustomerRepository CustomerRepository { get; }
        Task<int> SaveChanges(CancellationToken cancellation);
    }
}
