namespace CustomerService.Presentation.Server.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCrudTestInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration
        )
    {
        services.AddDbContext<CrudTestDbContext>(
            c => c.UseSqlServer(configuration.GetConnectionString("CrudTestConnectionString"))
             .ConfigureWarnings(warnings =>
               warnings.Ignore(RelationalEventId.PendingModelChangesWarning)));

        services.AddScoped(typeof(ICommandRepository<,>), typeof(CommandRepository<,>));
        services.AddScoped(typeof(IQueryRepository<,>), typeof(QueryRepository<,>));
        services.AddScoped<ICustomerCommandRepository, CustomerCommandRepository>();
        services.AddScoped<ICustomerQueryRepository, CustomerQueryRepository>();

        return services;
    }
    public static IServiceCollection AddCrudTestApplication(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(
      assembly: Assemblies.ApplicationAssembly,
      includeInternalTypes: true);

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(Assemblies.ApplicationAssembly);
        });

        services.AddAutoMapper(Assemblies.ApplicationAssembly);

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}
