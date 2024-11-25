namespace CustomerService.Infrastructure.Data;

public class CrudTestDbContext : DbContext
{
   
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
       
            
        try
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch(Exception ex)
        {
            throw ex;
        }

    }
    
    public CrudTestDbContext(DbContextOptions<CrudTestDbContext> options)
       : base(options)
    {
    }

    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);

        configurationBuilder.Properties<PhoneNumber>().HaveConversion<PhoneNumberConversion>();

        configurationBuilder.Properties<Email>().HaveConversion<EmailConversion>();

        configurationBuilder.Properties<BankAccountNumber>().HaveConversion<BankAccountNumberConversion>();

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CustomerConfiguration());

        ShadowProperties.AddLogShadowProperties(modelBuilder);

        ShadowProperties.AddSoftDeleteShadowProperties(modelBuilder);

        

        modelBuilder.Entity<Customer>()
        .HasIndex(u => u.Email, "IX_Customer_Email")
        .IsUnique()
        .HasFilter("[IsDeleted] = 0");

        modelBuilder.Entity<Customer>()
            .HasIndex(c => new { c.FirstName, c.LastName, c.DateOfBirth }, "IX_Customer_FirstNameLastNameDateOfBirth")
            .IsUnique()
            .HasFilter("[IsDeleted] = 0");

    }
    public override EntityEntry Remove(object entity)
    { 
        if(entity is not ISoftDeleteEntity) 
            base.Remove(entity);

        var entry = Entry(entity);
        if (entry.State == EntityState.Detached)
        {
            Attach(entity);
            entry = Entry(entity);
        }

        entry.CurrentValues[ShadowProperties.IsDeleted] = true;
        entry.CurrentValues[ShadowProperties.DeletedDateTime] = DateTime.UtcNow;
        entry.State = EntityState.Modified;
        return entry;
    }
    public override EntityEntry<TEntity> Remove<TEntity>(TEntity entity)
    {
        if (entity is not ISoftDeleteEntity) 
            base.Remove<TEntity>(entity);

        var entry = Entry(entity);
        if (entry.State == EntityState.Detached)
        {
            Attach(entity);
            entry = Entry(entity);
        }

        entry.CurrentValues[ShadowProperties.IsDeleted] = true;
        entry.CurrentValues[ShadowProperties.DeletedDateTime] = DateTime.UtcNow;
        entry.State = EntityState.Modified;
        return entry;
    }


}
