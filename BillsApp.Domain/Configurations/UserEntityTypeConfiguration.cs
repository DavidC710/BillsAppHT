
namespace BillsApp.Domain.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.FirstName)
               .HasMaxLength(20)
               .IsRequired();
            
        }
    }
}
