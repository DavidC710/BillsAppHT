
namespace BillsApp.Application.UseCases.Login.Commands.LoginCommand
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {

        public LoginCommandValidator()
        {
            RuleFor(c => c.Email)
                .NotEmpty().WithMessage("Field Email is required.")
                .EmailAddress().WithMessage("Not a valid Email.");

            RuleFor(c => c.Password)
                .NotEmpty().WithMessage("Field Password is required.");

        }
    }
}
