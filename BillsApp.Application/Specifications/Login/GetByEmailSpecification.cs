
namespace BillsApp.Application.Specifications.Login
{
    public class GetByEmailSpecification : Specification<User>
    {
        private readonly string email;

        public GetByEmailSpecification(string email, bool tracking)
        {
            this.email = email;
            AsNoTracking = tracking;
        }

        public override Expression<Func<User, bool>> ToExpression()
        {
            return user => user.Email.Equals(email);
        }
    }
}