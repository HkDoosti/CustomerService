 
namespace CustomerService.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(CustomerConfig.FirstNameMaxLength);

        builder.Property(c => c.LastName)
           .IsRequired()
           .HasMaxLength(CustomerConfig.LastNameMaxLength);

        builder.Property(c => c.PhoneNumber)
            .IsRequired()
            .HasMaxLength(PhoneNumber.MaxLength)
            .HasColumnName(nameof(PhoneNumber));

        builder.Property(c => c.Email)
            .IsRequired() 
            .HasMaxLength(Email.MaxLength)
            .HasColumnName(nameof(Email));

        builder.Property(c => c.BankAccountNumber)
           .IsRequired()
           .HasMaxLength(BankAccountNumber.MaxLength)
           .HasColumnName(nameof(BankAccountNumber));

       
    }
}
