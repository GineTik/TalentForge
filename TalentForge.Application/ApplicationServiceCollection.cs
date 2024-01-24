using Microsoft.Extensions.DependencyInjection;
using TalentForge.Application.Services.User;

namespace TalentForge.Application;

public static class ApplicationServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();
        return services;
    }
}