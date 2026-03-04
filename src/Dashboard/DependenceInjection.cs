
using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.Hosting;
using System.Data;
using Npgsql;
using Dashboard.Entities;
using Dashboard.Repositories;

public static class DependenceInjection
{
    public static void AddApplications (this IHostApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddScoped<IDbConnection, NpgsqlConnection>((sp) => 
        {
            return new NpgsqlConnection(connectionString);
        });
        
        builder.Services.AddScoped<IUserRepository, UserRepository>();
    }
}