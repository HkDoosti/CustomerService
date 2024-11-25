namespace CustomerService.Infrastructure.Data.Properties;

public static class ShadowProperties
{
    public static readonly string CreatedDateTime = nameof(CreatedDateTime);

    public static readonly string ModifiedDateTime = nameof(ModifiedDateTime);

    public static readonly string DeletedDateTime = nameof(DeletedDateTime);

    public static readonly string IsDeleted = nameof(IsDeleted);

    public static void AddLogShadowProperties(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model
                                   .GetEntityTypes()
                                   .Where(c => typeof(ILogEntity)
                                     .IsAssignableFrom(c.ClrType))
                                   )
        {
            modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime>(CreatedDateTime)
                        .HasDefaultValue(DateTime.Now);

            modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime?>(ModifiedDateTime);

            }
    }

    public static void AddSoftDeleteShadowProperties(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model
                                   .GetEntityTypes()
                                   .Where(c => typeof(ISoftDeleteEntity)
                                     .IsAssignableFrom(c.ClrType))
                                   )
        {
            modelBuilder.Entity(entityType.ClrType)
                        .Property<bool>(IsDeleted);

            modelBuilder.Entity(entityType.ClrType)
                        .Property<DateTime?>(DeletedDateTime);
        }
    }

    
}
