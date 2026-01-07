using Flexybook.Domain.Entities;

namespace Flexybook.Infrastructure.Repositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> Create(T baseEntity);
        Task<T?> Get(Guid id);
        Task<IEnumerable<T>> GetAll();
    }
}