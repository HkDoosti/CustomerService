namespace CustomerService.Application.Commands.CustomerCommands.DeleteCustomerCommand;

public class DeleteCustomerCommandHandler(
    ICustomerCommandRepository commandRepository,
    ICustomerQueryRepository queryRepository)
    : IRequestHandler<DeleteCustomerCommandRequest, Result>
{
    private readonly ICustomerCommandRepository _commandRepository = commandRepository;

    private readonly ICustomerQueryRepository _queryRepository = queryRepository;

    public async Task<Result> Handle(
        DeleteCustomerCommandRequest request,
        CancellationToken cancellationToken)
    {
        var Customer = await _queryRepository
             .GetByIdAsync(
            request.Id
            );

        if (Customer is null)
        {
            return Result
                .Failure(CustomerErrors.NotFound(request.Id));
        }
        _commandRepository.Delete(Customer);

        await _commandRepository.SaveChangeAsync(cancellationToken);

        return Result.Success(Customer);
    }
}
