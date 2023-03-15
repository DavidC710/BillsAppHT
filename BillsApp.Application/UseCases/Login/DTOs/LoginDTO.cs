
namespace BillsApp.Application.UseCases.Login.DTOs
{
    public class LoginDTO : IMapFrom<User>
    {
        public int Id { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Cellphone { get; set; }
        public DateTime? Created { get; set; }
        public string? Password { get; set; }        
        public bool Valid { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
    }
}
