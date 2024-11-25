namespace CustomerService.Infrastructure.Repository;

public class CommandRepository<TEntity,TID>(CrudTestDbContext context)
    : ICommandRepository<TEntity, TID>, IDisposable
    where TEntity : BaseEntity<TID>
{
    private readonly CrudTestDbContext _context = context;
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();
    private bool disposedValue;

    public void Add(TEntity entity)
    {
        _context.Entry(entity).Property<DateTime>(ShadowProperties.CreatedDateTime).CurrentValue = DateTime.UtcNow;
        _dbSet.Add(entity);
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        _context.Entry(entity).Property<DateTime>(ShadowProperties.CreatedDateTime).CurrentValue = DateTime.UtcNow;
        await _dbSet.AddAsync(entity, cancellationToken);
    }

    public void Delete(TEntity entity)
    {
        _context.Remove(entity);
    }

    public void SaveChange()
    {
        _context.SaveChanges();
    }


    public async Task<bool> SaveChangeAsync(CancellationToken cancellationToken)
    {
        try
        {
            return await _context.SaveChangesAsync(cancellationToken) > 0;
        }
        catch (Exception ex)
        {
            throw ex;
        }

    }

    public void Update(TEntity entity)
    {
        _context.Entry(entity).Property<DateTime?>(ShadowProperties.ModifiedDateTime).CurrentValue = DateTime.UtcNow;
        _context.Entry(entity).State = EntityState.Modified;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            if (disposing)
            {
                // TODO: dispose managed state (managed objects)
            }

            // TODO: free unmanaged resources (unmanaged objects) and override finalizer
            // TODO: set large fields to null
            disposedValue = true;
        }
    }

    

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
