namespace CustomerService.Infrastructure.Repository.CustomerRepository;

public class CustomerQueryRepository(CrudTestDbContext context) 
    : QueryRepository<Customer,Guid>(context),ICustomerQueryRepository
{
    
}
