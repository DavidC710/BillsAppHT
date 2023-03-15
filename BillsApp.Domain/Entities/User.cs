
namespace BillsApp.Domain.Entities
{
    public class User : BaseEntity, IAuditable, ISoftDelete
    {
        public bool IsDeleted { get; set; } = false;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Cellphone { get; set; }
        public string Email { get; init; }
        public string Password { get; set; }
        public DateTime Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        
    }
}
