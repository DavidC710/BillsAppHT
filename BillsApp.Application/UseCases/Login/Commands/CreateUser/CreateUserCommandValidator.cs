
namespace BillsApp.Application.UseCases.Login.Commands.CreateUserCommand
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly IRepository<User> userRepository;

        public CreateUserCommandValidator(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;

            RuleFor(c => c.FirstName)
                .NotEmpty().WithMessage("Field FirstName is required.")
                .MaximumLength(20).WithMessage("Field FirstName must not exceed 20 characters.");

            RuleFor(c => c.LastName)
                .NotEmpty().WithMessage("Field LastName is required.")
                .MaximumLength(20).WithMessage("Field LastName must not exceed 20 characters.");

            RuleFor(c => c.Cellphone)
                .NotEmpty().WithMessage("Field Cellphone is required.")
                .MaximumLength(20).WithMessage("Field Cellphone must not exceed 20 characters.");

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Field Email is required.")
                .MustAsync(BeUniqueEmail).WithMessage("The specified Email already exists.")
                .EmailAddress().WithMessage("Not a valid Email.");                

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Field Password is required.");
        }

        public async Task<bool> BeUniqueEmail(string email, CancellationToken cancellationToken)
        {
            var spec = new GetByEmailSpecification(email, false);
            return await userRepository.GetOneBySpecificationAsync(spec, cancellationToken) == null;
        }
    }
}
