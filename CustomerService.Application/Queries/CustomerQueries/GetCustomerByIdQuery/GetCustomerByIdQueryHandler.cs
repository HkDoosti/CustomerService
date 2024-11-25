using CustomerService.Domain.DomainErrors;

namespace CustomerService.Application.Queries.CustomerQueries.GetCustomerByIdQuery;

public class GetCustomerByIdQueryHandler(ICustomerQueryRepository repository, IMapper mapper) : IRequestHandler<GetCustomerByIdQueryRequest, CustomerDto>
{
    private readonly ICustomerQueryRepository _repository = repository;
    private readonly IMapper _mapper = mapper;



    public async Task<CustomerDto> Handle(GetCustomerByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id);
        if (customer == null)
            throw new CustomException(CustomerErrors.NotFound(request.Id));

        return _mapper.Map<CustomerDto>(customer);
    }


}
