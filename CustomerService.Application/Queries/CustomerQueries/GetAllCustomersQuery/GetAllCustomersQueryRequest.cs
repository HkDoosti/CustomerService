namespace CustomerService.Application.Queries.CustomerQueries.GetAllCustomersQuery;

public record GetAllCustomersQueryRequest() : IRequest<List<CustomerDto>>;
 
