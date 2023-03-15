
namespace BillsApp.Application.UseCases.Login.Commands.LoginCommand
{
    public record LoginCommand : IRequest<Result<LoginDTO>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginHandler : UseCaseHandler, IRequestHandler<LoginCommand, Result<LoginDTO>>
    {
        private readonly IRepository<User> userRepository;
        private readonly IRepository<UserRole> userRoleRepository;
        private readonly IMapper mapper;
        public LoginHandler(IRepository<User> userRepository, IRepository<UserRole> userRoleRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.userRoleRepository = userRoleRepository;
            this.mapper = mapper;
        }

        public async Task<Result<LoginDTO>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.GetOneBySpecificationAsync(new GetByEmailSpecification(request.Email!, false), cancellationToken);
                var userRoles = await userRoleRepository.GetAllAsync();

                var isValid = false;
                var isAdmin = false;

                if (user != null)
                {
                    var userDTO = mapper.Map<LoginDTO>(user);
                    userDTO.Password = request.Password;
                    isValid = user.Password.Equals(request.Password);
                    isAdmin = userRoles.Where(t => t.UserId == user.Id && t.RoleName == "ADMIN").Any();
                    userDTO.Valid = isValid;
                    userDTO.IsAdmin = isAdmin;

                    if (userDTO.Valid) {
                        userDTO.Token = "1234";
                        return Succeded(userDTO);
                    } 
                    else return PermissionDenied<LoginDTO>();
                }
                else {
                    return NotFound<LoginDTO>();
                }

            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, ex.InnerException);
            }
        }
    }
}
