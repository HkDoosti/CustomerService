using CustomerService.Application.Commands.CustomerCommands.EditCustomerCommand;

namespace CustomerService.Application.Commands.CustomerCommands.AddCustomerCommand;

public sealed class AddCustomerCommandRequestValidator
    : CustomerCommandRequestValidator<EditCustomerCommandRequest>
{
    protected override void SetupValidationRules()
    {
        ValidateCustomerName(x => x.Customer.FirstName, CustomerConfig.FirstNameMaxLength);
        ValidateCustomerLastName(x => x.Customer.LastName, CustomerConfig.LastNameMaxLength);
        ValidateCustomerBirthDate(x => x.Customer.DateOfBirth);
        ValidateCustomerPhoneNumber(x => x.Customer.PhoneNumber);
        ValidateCustomerEmail(x => x.Customer.Email);
        ValidateCustomerBankAccountNumber(x => x.Customer.BankAccountNumber);
    }
}