namespace CustomerService.Application.Commands.CustomerCommands.EditCustomerCommand;

public sealed record EditCustomerCommandRequest
(
  CustomerDto Customer

) : ICommand;

