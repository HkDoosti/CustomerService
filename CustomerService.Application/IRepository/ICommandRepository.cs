namespace CustomerService.Application.IRepository;

public interface ICommandRepository<TEntity,TID>
{
    void Add(TEntity entity);
    void Delete(TEntity entity);
    void Update(TEntity entity);
    Task<bool> SaveChangeAsync(CancellationToken cancellationToken);

}
