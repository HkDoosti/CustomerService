using System.Windows.Input;
using PhoneNumber = CustomerService.Domain.ValueObjects.PhoneNumber;

namespace CustomerService.Domain.Dto.CustomerDotes;

public sealed record CustomerDto
(Guid Id,
string FirstName,
string LastName,
DateTime DateOfBirth,
PhoneNumber PhoneNumber,
Email Email,
BankAccountNumber BankAccountNumber);



