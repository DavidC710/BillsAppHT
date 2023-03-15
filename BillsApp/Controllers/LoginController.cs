using Serilog;
namespace BillsApp.Controllers
{
    public class LoginController : ApiBaseController
    {
        private readonly IValidator<LoginCommand> loginValidator;
        private readonly IValidator<CreateUserCommand> createUserValidator;
        private readonly Logger<LoginController> logger;

        public LoginController(IValidator<LoginCommand> loginValidator, IValidator<CreateUserCommand> createUserValidator, Logger<LoginController> logger)
        {
            this.loginValidator = loginValidator;
            this.createUserValidator = createUserValidator;
            this.logger = logger;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Create(CreateUserCommand input)
        {
            var createValidator = await createUserValidator.ValidateAsync(input);

            if (!createValidator.IsValid)
            {
                return BadRequest(createValidator.Errors);
            }
            else
            {
                var result = await Mediator.Send(input);
                return this.FromResult(result);
            }
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllUsersQuery { });
            return this.FromResult(result);
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginCommand input)
        {
            logger.LogInformation("TEST FOR MIKE");
            var validator = loginValidator.Validate(input);

            if (!validator.IsValid)
            {
                return BadRequest(validator.Errors);
            }
            else
            {
                var result = await Mediator.Send(input);
                return this.FromResult(result);
            }
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> RecoverPassword(RecoverPasswordCommand input)
        {
            var result = await Mediator.Send(input);
            return this.FromResult(result);
        }

        [HttpPut("[action]")]
        public async Task<IActionResult> Update(UpdateUserCommand input)
        {
            var result = await Mediator.Send(input);
            return this.FromResult(result);
        }
    }
}