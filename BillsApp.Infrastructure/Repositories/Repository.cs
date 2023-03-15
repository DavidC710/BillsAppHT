
namespace BillsApp.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {

        protected readonly BillsDbContext context;
        protected DbSet<T> Entity => context.Set<T>();

        public Repository(IBillsDbContextFactory factory)
        {
            context = factory.CreateDbContext(new string[0]);
        }

        public async Task<List<T>> GetBySpecificationAsync(Specification<T> spec, CancellationToken cancellation)
        {
            var queryable = SetQueryWithSpecifications(spec);
            return await queryable.ToListAsync();
        }

        public List<T> GetBySpecification(Specification<T> spec)
        {
            var queryable = SetQueryWithSpecifications(spec);
            return queryable.ToList();
        }

        public Task<bool> CheckIfExistsBySpecificationAsync(Specification<T> spec, Expression<Func<T, bool>> conditionProperties)
        {
            throw new NotImplementedException();
        }

        public bool CheckIfExistsBySpecification(Specification<T> spec, Expression<Func<T, bool>> conditionProperties)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetOneBySpecificationAsync(Specification<T> spec, CancellationToken cancellationToken)
        {
            var queryable = SetQueryWithSpecifications(spec);
            return queryable.FirstOrDefaultAsync(cancellationToken);
        }

        public T GetOneBySpecification(Specification<T> spec)
        {
            throw new NotImplementedException();
        }

        public Task<T> GetLastBySpecificationAsync(Specification<T> spec)
        {
            throw new NotImplementedException();
        }

        public T GetLastBySpecification(Specification<T> spec)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountBySpecificationAsync(Specification<T> spec)
        {
            throw new NotImplementedException();
        }

        public int GetCountBySpecification(Specification<T> spec)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddAsync(T entity)
        {
            await Entity.AddAsync(entity);
            return await SaveAsync();
        }

        public async Task<T> GetByAsync(int id)
        {
            return await Entity.FindAsync(id);
        }

        public T GetBy(int id)
        {
            return Entity.Find(id)!;
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await Entity
                .AsNoTracking()
                .ToListAsync();
        }

        public List<T> GetAll()
        {
            return Entity.ToList();
        }

        public void Add(T entity)
        {
            Entity.Add(entity);
            Save();
        }

        public async Task DeleteByAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
            await SaveAsync();
        }

        public void DeleteBy(int id)
        {
            throw new NotImplementedException();
        }

        public Task DeleteRangeAsync(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public void DeleteRange(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(T entity)
        {
            Entity.Update(entity);
            await SaveAsync();
        }

        public async Task UpdateStatusAsync(T entity)
        {
            Entity.Update(entity);
            await SaveAsync();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        private async Task<int> SaveAsync()
        {
            return await this.context.SaveChangesAsync();
        }

        private void Save()
        {
            this.context.SaveChanges();
        }

        private IQueryable<T> SetQueryWithSpecifications(Specification<T> spec)
        {
            var queryable = spec.Includes
                .Aggregate(
                    Entity.AsQueryable(),
                    (current, include) => current.Include(include));

            queryable = spec.IncludeStrings
                .Aggregate(
                    queryable,
                    (current, include) => current.Include(include));

            if (spec.OrderBy != null)
            {
                queryable = spec.OrderBy(queryable);
            }

            if (spec.AsNoTracking)
            {
                queryable = queryable.AsNoTracking();
            }

            return queryable.Where(spec.ToExpression());
        }

    }
}
