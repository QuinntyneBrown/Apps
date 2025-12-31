// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using PersonalWiki.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using PersonalWiki.Core.Services;
namespace PersonalWiki.Infrastructure;

/// <summary>
/// Extension methods for configuring Infrastructure services.
/// </summary>
public static class ConfigureServices
{
    /// <summary>
    /// Adds Infrastructure services to the dependency injection container.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="configuration">The configuration.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<PersonalWikiContext>(options =>
        {
            options.UseSqlServer(connectionString, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(PersonalWikiContext).Assembly.FullName);
                sqlOptions.EnableRetryOnFailure(
                    maxRetryCount: 3,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
        });

        services.AddScoped<IPersonalWikiContext>(provider =>
            provider.GetRequiredService<PersonalWikiContext>());

        // Register multi-tenant services
        services.AddHttpContextAccessor();
        services.AddScoped<ITenantContext, TenantContext>();
        // Register identity services
        services.AddSingleton<IPasswordHasher, PasswordHasher>();
        services.AddSingleton<IJwtTokenService, JwtTokenService>();



        return services;
    }
}
