
namespace BillsApp.Application.UseCases.Login.Commands.CreateUserCommand
{
    public class CreateUserCommand : IRequest<Result<CreateUserCommand>>, IMapFrom<User>
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public string Cellphone { get; init; }
        public string Email { get; init; }
        public string Password { get; set; }
        public string? Token { get; set; }
        public string? Errors { get; set; }
    }

    public class CreateUserHandler : UseCaseHandler, IRequestHandler<CreateUserCommand, Result<CreateUserCommand>>
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        public CreateUserHandler(IRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Result<CreateUserCommand>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = mapper.Map<User>(request);
                user.Created = DateTime.Now;
                request.Token = "12345";
                await userRepository.AddAsync(user!);

                return Succeded(request);
            }
            catch (Exception ex)
            {
                return Invalid<CreateUserCommand>(error: ex.Message + ". " + ex.StackTrace);
            }
          
        }
    }
}
