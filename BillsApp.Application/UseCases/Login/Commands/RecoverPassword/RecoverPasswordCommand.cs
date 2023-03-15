
namespace BillsApp.Application.UseCases.Login.Commands.RecoverPasswordCommand
{
    public record RecoverPasswordCommand : IRequest<Result<LoginDTO>>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Cellphone { get; set; }
        public string? NewPassword { get; set; }
    }

    public class RecoverPasswordHandler : UseCaseHandler, IRequestHandler<RecoverPasswordCommand, Result<LoginDTO>>
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        public RecoverPasswordHandler(IRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Result<LoginDTO>> Handle(RecoverPasswordCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.GetOneBySpecificationAsync(new GetByEmailSpecification(request.Email!, false), cancellationToken);
                var isValid = false;

                if (user != null)
                {
                    var userDTO = mapper.Map<LoginDTO>(user);
                    user.Password = request.NewPassword!;
                    isValid = user.Cellphone.Equals(request.Cellphone);
                    userDTO.Valid = isValid;

                    if (userDTO.Valid) {
                        userDTO.Token = "1234";
                        await userRepository.UpdateAsync(user);
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
