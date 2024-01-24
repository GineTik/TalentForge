using Microsoft.Extensions.DependencyInjection;
using TalentForge.Application.Interfaces;
using TalentForge.Infrastructure.Data.EF;

namespace TalentForge.Infrastructure;

public static class InfrastructureServiceCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddDbContext<IDataContext, DataContext>(DataContextFactory.ConfigureOptions);
        return services;
    }
}