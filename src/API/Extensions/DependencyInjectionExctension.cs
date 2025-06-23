using Microsoft.Extensions.DependencyInjection;
using Otter.Core.Interfaces.Services;
using Otter.Core.Services;
using Otter.Core.Repositories;
using Otter.Core.Data;

namespace Otter.API.Extensions;
public static class DependencyInjectionExctension
{
    public static void AddDependency(this IServiceCollection services)
    {
        services.AddTransient<PGSQLContext>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRepository, UserRepository>();
    }
}