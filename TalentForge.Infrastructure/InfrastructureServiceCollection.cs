using Microsoft.Extensions.DependencyInjection;
using TalentForge.Infrastructure.Data.EF;

namespace TalentForge.Infrastructure;

public static class InfrastructureServiceCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<DataContext>(DataContextFactory.ConfigureOptions);
        return services;
    }
}