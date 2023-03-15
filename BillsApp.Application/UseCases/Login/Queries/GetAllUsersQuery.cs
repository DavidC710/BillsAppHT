
using System.Diagnostics;

namespace BillsApp.Application.UseCases.Login.Queries
{
    public record GetAllUsersQuery : IRequest<Result<List<LoginDTO>>>;
    public class GetAllUsersHandler : UseCaseHandler, IRequestHandler<GetAllUsersQuery, Result<List<LoginDTO>>>
    {
        private readonly IRepository<User> userRepository;
        private readonly IMapper mapper;
        public GetAllUsersHandler(IRepository<User> userRepository, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
        }
        public async Task<Result<List<LoginDTO>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.GetAllAsync();
            var usersListDTO = mapper.ProjectTo<LoginDTO>(users.AsQueryable()).ToList();

            return Succeded(usersListDTO);
        }
    }
}
