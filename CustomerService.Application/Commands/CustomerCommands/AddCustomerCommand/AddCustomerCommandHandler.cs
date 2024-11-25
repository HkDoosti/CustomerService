namespace CustomerService.Application.Commands.CustomerCommands.AddCustomerCommand;

public class AddCustomerCommandHandler(
ICustomerCommandRepository commandRepository,
ICustomerQueryRepository queryRepository,
IMapper mapper)
: IRequestHandler<AddCustomerCommandRequest, Result<CustomerDto>>
{
    private readonly ICustomerCommandRepository _commandRepository = commandRepository;

    private readonly ICustomerQueryRepository _queryRepository = queryRepository;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<CustomerDto>> Handle(AddCustomerCommandRequest request, CancellationToken cancellationToken)
    {
        var customer = _mapper.Map<Customer>(request.Customer);

        _commandRepository.Add(customer);

        await _commandRepository.SaveChangeAsync(cancellationToken);

        CustomerDto? customerDto = _mapper.Map<CustomerDto>(customer);

        Result<CustomerDto> result =  Result.Create<CustomerDto>(customerDto);
        return result;
    }

    
}
