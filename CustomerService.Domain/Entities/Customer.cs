using PhoneNumber = CustomerService.Domain.ValueObjects.PhoneNumber;

namespace CustomerService.Domain.Entities;

public  class Customer: BaseEntity<Guid>,ILogEntity,ISoftDeleteEntity
{
    public Customer() => Id = Guid.NewGuid();
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public PhoneNumber PhoneNumber { get; set; }
    public Email Email { get; set; }
    public BankAccountNumber BankAccountNumber { get; set; }

     
}
