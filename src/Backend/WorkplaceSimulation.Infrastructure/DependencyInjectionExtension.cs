using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkplaceSimulation.Domain.Repositories.Departamento;
using WorkplaceSimulation.Domain.Repositories.Funcionario;
using WorkplaceSimulation.Infrastructure.DataAccess;
using WorkplaceSimulation.Infrastructure.DataAccess.Repositories;

namespace WorkplaceSimulation.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration) 
    {
        AddDataAccess(services, configuration);
        AddRepositories(services);
    }

    private static void AddDataAccess(IServiceCollection services, IConfiguration configuration) 
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (connectionString != null) 
        {
            services.AddScoped<DbSession>(_ => new DbSession(connectionString));
        }
    }
    private static void AddRepositories(IServiceCollection services) 
    {
        services.AddScoped<IDepartamentoReadRepository, DepartamentoRepository>();
        services.AddScoped<IDepartamentoWriteRepository, DepartamentoRepository>();
        services.AddScoped<IFuncionarioReadRepository, FuncionarioRepository>();
        services.AddScoped<IFuncionarioWriteRepository, FuncionarioRepository>();
    }
}
