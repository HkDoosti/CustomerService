namespace CustomerService.Application.Queries.CustomerQueries.GetAllCustomersQuery;

public class GetAllCustomersQueryHandler(ICustomerQueryRepository repository, IMapper mapper)
    : IRequestHandler<GetAllCustomersQueryRequest, List<CustomerDto>>
{
    private readonly ICustomerQueryRepository _repository = repository;
    private readonly IMapper _mapper = mapper;


    public async Task<List<CustomerDto>> Handle(GetAllCustomersQueryRequest request, CancellationToken cancellationToken)
    {
        var customers = await _repository.GetAll().ToListAsync();
        return _mapper.Map<List<CustomerDto>>(customers);
    }
}