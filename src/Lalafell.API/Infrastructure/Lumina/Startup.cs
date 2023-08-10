using Lalafell.API.Infrastructure.Lumina.Provider;

namespace Lalafell.API.Infrastructure.Lumina;

public static class Startup
{
    public static IServiceCollection AddLumina(this IServiceCollection services)
    {
        return services.AddSingleton<LuminaProvider>()
                       .AddSingleton<ItemProvider>();
    }
}