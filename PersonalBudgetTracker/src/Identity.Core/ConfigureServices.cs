using Microsoft.Extensions.DependencyInjection;

namespace Identity.Core;

public static class ConfigureServices
{
    public static IServiceCollection AddIdentityServices(this IServiceCollection services)
    {
        return services;
    }
}
