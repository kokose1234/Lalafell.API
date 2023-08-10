using System.Threading.RateLimiting;
using Lalafell.API.Infrastructure.GraphQL;
using Lalafell.API.Infrastructure.Lumina;
using Microsoft.AspNetCore.RateLimiting;

namespace Lalafell.API.Infrastructure;

public static class Startup
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        return services
               .AddRateLimit()
               .AddGraphQL()
               .AddLumina()
               .AddRouting(options => options.LowercaseUrls = true);
    }

    private static IServiceCollection AddRateLimit(this IServiceCollection services)
    {
        return services.AddRateLimiter(l => l.AddFixedWindowLimiter(policyName: "fixed", options =>
        {
            options.PermitLimit = 5;
            options.Window = TimeSpan.FromSeconds(10);
            options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            options.QueueLimit = 2;
        }));
    }
}