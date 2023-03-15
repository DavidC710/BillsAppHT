
namespace BillsApp.Domain.Entities
{
    public class UserRole : BaseEntity, IAuditable
    {
        public string RoleName { get; set; }
        public bool IsAdmin { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public DateTime Created { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public string? LastModifiedBy { get; set; }
        
    }
}
