
namespace BillsApp.Domain.Entities
{
    public interface IAuditable
    {
        public DateTime Created { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? LastModified { get; set; }

        public string? LastModifiedBy { get; set; }
        public bool IsAuditable { get { return true; } }
    }
}
