namespace Onboarding.Domain.IRepositories
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task<IEnumerable<T>> GetAll(CancellationToken cancellation);
        void Create(T entity);
    }
}
