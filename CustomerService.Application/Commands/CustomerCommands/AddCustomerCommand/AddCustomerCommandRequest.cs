namespace CustomerService.Application.Commands.CustomerCommands.AddCustomerCommand;

public sealed record AddCustomerCommandRequest
(
  CreateCustomerDto Customer
     
) : ICommand<CustomerDto>;
