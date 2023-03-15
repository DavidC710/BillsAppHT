
namespace BillsApp.Application.Interfaces.Repositories
{
    public interface IRepository<T>
        where T : BaseEntity
    {
        
        Task<List<T>> GetBySpecificationAsync(Specification<T> spec, CancellationToken cancellationToken);

        List<T> GetBySpecification(Specification<T> spec);

        Task<bool> CheckIfExistsBySpecificationAsync(Specification<T> spec, Expression<Func<T, bool>> conditionProperties);

        bool CheckIfExistsBySpecification(Specification<T> spec, Expression<Func<T, bool>> conditionProperties);

        Task<T> GetOneBySpecificationAsync(Specification<T> spec, CancellationToken cancellationToken);

        T GetOneBySpecification(Specification<T> spec);

        Task<T> GetLastBySpecificationAsync(Specification<T> spec);

        T GetLastBySpecification(Specification<T> spec);

        Task<int> GetCountBySpecificationAsync(Specification<T> spec);

        int GetCountBySpecification(Specification<T> spec);

        Task<int> AddAsync(T entity);

        void Add(T entity);

        Task<T> GetByAsync(int id);

        T GetBy(int id);

        Task<List<T>> GetAllAsync();

        List<T> GetAll();

        Task DeleteByAsync(T entity);

        void DeleteBy(int id);

        Task DeleteRangeAsync(IEnumerable<int> ids);

        void DeleteRange(IEnumerable<int> ids);

        Task UpdateAsync(T entity);

        void Update(T entity);
    }
}
