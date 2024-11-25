namespace CustomerService.Application.IRepository;

public interface IQueryRepository<TEntity,TID>
{
    Task<TEntity> GetByIdAsync(TID Id);
    IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
    IQueryable<TEntity> FindWithPaging(Expression<Func<TEntity, bool>> predicate,short index , short size);
    IQueryable<TEntity> GetAll();
}
