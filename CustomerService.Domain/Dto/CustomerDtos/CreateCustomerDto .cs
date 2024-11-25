using PhoneNumber = CustomerService.Domain.ValueObjects.PhoneNumber;

namespace CustomerService.Domain.Dto.CustomerDtos;

public record CreateCustomerDto
 (string FirstName ,
 string LastName ,
 DateTime DateOfBirth ,
 PhoneNumber PhoneNumber ,
 Email Email ,
 BankAccountNumber BankAccountNumber 
);
