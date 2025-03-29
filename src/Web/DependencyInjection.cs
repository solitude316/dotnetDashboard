using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Hosting;
using Dashboard.Web.Interfaces.Repositories;
using Dashboard.Web.Interfaces.Services;
using Dashboard.Web.Repositories;
using Dashboard.Web.Services;


namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static void AddApplications(this IServiceCollection services)
    {
        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<IUserService, UserServices>();

    }
}
