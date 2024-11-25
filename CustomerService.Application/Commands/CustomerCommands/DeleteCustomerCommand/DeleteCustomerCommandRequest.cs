namespace CustomerService.Application.Commands.CustomerCommands.DeleteCustomerCommand;

public sealed record DeleteCustomerCommandRequest
(Guid Id)
    : ICommand;
