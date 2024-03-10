namespace Onboarding.Infrastructure
{
    public class UnitOfWork : IUnitWork
    {
        private readonly AppDbContext _context;
        private readonly ICustomerRepository _customerRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public ICustomerRepository CustomerRepository
        {
            get
            {
                return _customerRepository ?? new CustomerRepository(_context);
            }
        }

        public async Task<int> SaveChanges(CancellationToken cancellation) => await _context.SaveChangesAsync(cancellation);
    }
}
