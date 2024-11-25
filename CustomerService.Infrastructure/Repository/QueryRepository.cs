namespace CustomerService.Infrastructure.Repository;

public class QueryRepository<TEntity, TID>(CrudTestDbContext context)
    : IQueryRepository<TEntity, TID>
    where TEntity : BaseEntity<TID>
{
    protected readonly CrudTestDbContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public  IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
    {
        return _context.Set<TEntity>()
            .Where(e => EF.Property<bool>(e, "IsDeleted") == false)
            .Where(predicate);
        
    }

    public async  Task<TEntity> GetByIdAsync(TID Id)
    {
        
        return await _context.Set<TEntity>()
           .Where(e => EF.Property<bool>(e, "IsDeleted") == false) 
           .SingleOrDefaultAsync(e => EF.Property<TID>(e, "Id").Equals(Id));
    }


   public IQueryable<TEntity>  GetAll()
    {
        return _context.Set<TEntity>().Where(e => EF.Property<bool>(e, "IsDeleted") == false);
    }

    public IQueryable<TEntity> FindWithPaging(Expression<Func<TEntity, bool>> predicate, short index, short size)
    {
        return _context.Set<TEntity>()
            .Skip((index-1)*size)   // Skip the records according to the page 
            .Take(size)             // Take the records for the current page  
            .Where(e => EF.Property<bool>(e, "IsDeleted") == false)
            .Where(predicate);
    }
}
