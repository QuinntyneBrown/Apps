using Identity.Core.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenService, JwtTokenService>();

        return services;
    }
}
