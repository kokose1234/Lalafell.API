using XIVAPI.KR.Data.Options;

namespace XIVAPI.KR.Services;

public static class Startup
{
    public static WebApplicationBuilder AddConfigurations(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions();
        builder.Configuration
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();

        builder.Services.Configure<LuminaOption>(builder.Configuration.GetSection("Lumina"));

        return builder;
    }

    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        return services
               .AddRouting(options => options.LowercaseUrls = true)
               .AddServices();
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        return services.AddSingleton<LuminaProvider>();
    }
}