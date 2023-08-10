using Lalafell.API.Data.Options;

namespace Lalafell.API.Configurations;

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
}