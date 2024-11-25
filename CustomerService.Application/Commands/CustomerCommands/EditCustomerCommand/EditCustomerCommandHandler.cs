using CustomerService.Domain.DomainErrors;
using CustomerService.Domain.Entities;

namespace CustomerService.Application.Commands.CustomerCommands.EditCustomerCommand;

public class EditCustomerCommandHandler(
    ICustomerCommandRepository commandRepository,
    ICustomerQueryRepository queryRepository,
     IMapper mapper)
    : IRequestHandler<EditCustomerCommandRequest, Result>
{
    private readonly ICustomerCommandRepository _commandRepository = commandRepository;
    private readonly ICustomerQueryRepository _queryRepository = queryRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<Result> Handle(EditCustomerCommandRequest request, CancellationToken cancellationToken)
    {
        Customer customer = await _queryRepository
            .GetByIdAsync(
           request.Customer.Id);
         
        if (customer is null)
        {
            return Result
                .Failure(CustomerErrors.NotFound(request.Customer.Id));
        }
        _mapper.Map(request.Customer, customer);

        _commandRepository.Update(customer);

        await _commandRepository.SaveChangeAsync(cancellationToken);
        return Result.Success();

    }


}
