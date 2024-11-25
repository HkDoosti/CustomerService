namespace CustomerService.Domain.DomainEvents;

public sealed record CreatedCustomerDomainEvent(Guid CustomerId): IDomainEvent;
