namespace CustomerService.Application.Queries.CustomerQueries.GetCustomerByIdQuery;

public record GetCustomerByIdQueryRequest
(Guid Id): IRequest<CustomerDto>;
