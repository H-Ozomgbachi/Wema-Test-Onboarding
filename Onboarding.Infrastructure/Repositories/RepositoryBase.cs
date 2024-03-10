namespace Onboarding.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : BaseEntity
    {
        private readonly AppDbContext _context;

        public RepositoryBase(AppDbContext context)
        {
            _context = context;
        }

        public void Create(T entity) => _context.Set<T>().Add(entity);

        public async Task<IEnumerable<T>> GetAll(CancellationToken cancellation) => await _context.Set<T>().AsNoTracking().ToListAsync(cancellation);
    }
}
