namespace CustomerService.Domain.IEntities;

public abstract class BaseEntity<TID>
{
    public TID Id { get; set; }
}
