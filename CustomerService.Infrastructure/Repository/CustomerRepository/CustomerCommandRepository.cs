namespace CustomerService.Infrastructure.Repository.CustomerRepository;

public class CustomerCommandRepository(CrudTestDbContext context) 
    : CommandRepository<Customer,Guid>(context), ICustomerCommandRepository
{
    

}
