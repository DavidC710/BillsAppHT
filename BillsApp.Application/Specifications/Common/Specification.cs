
namespace BillsApp.Application.Specifications.Common
{
    public abstract class Specification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { get; protected set; }
        public List<Expression<Func<T, object>>> Includes { get; protected set; } = new List<Expression<Func<T, object>>>();
        public List<string> IncludeStrings { get; protected set; } = new List<string>();

        public bool AsNoTracking { get; set; }

        public void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            this.Includes.Add(includeExpression);
        }

        public virtual void AddInclude(string includeString)
        {
            this.IncludeStrings.Add(includeString);
        }
        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = ToExpression().Compile();
            return predicate(entity);
        }
    }
}
