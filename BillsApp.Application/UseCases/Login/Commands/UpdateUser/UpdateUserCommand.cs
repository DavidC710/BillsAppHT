
namespace BillsApp.Application.UseCases.Login.Commands.UpdateUserCommand
{
    public record UpdateUserCommand : IRequest<Result<UpdateUserDTO>>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Cellphone { get; set; }
    }

    public class UpdateUserHandler : UseCaseHandler, IRequestHandler<UpdateUserCommand, Result<UpdateUserDTO>>
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        public UpdateUserHandler(IRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }

        public async Task<Result<UpdateUserDTO>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.GetOneBySpecificationAsync(new GetByEmailSpecification(request.Email!, false), cancellationToken);

                if (user != null)
                {
                    user.FirstName = request.FirstName!;
                    user.LastName = request.LastName!;
                    user.Cellphone = request.Cellphone!;
                    var userDTO = mapper.Map<UpdateUserDTO>(user);
                    await userRepository.UpdateAsync(user);
                    userDTO.Token = "1234";
                    return Succeded(userDTO);
                }
                else
                {
                    return NotFound<UpdateUserDTO>();
                }

            }
            catch (ApplicationException ex)
            {
                throw new ApplicationException(ex.Message, ex.InnerException);
            }
        }
    }
}
