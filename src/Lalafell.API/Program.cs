using Lalafell.API.Configurations;
using Lalafell.API.Infrastructure;

namespace Lalafell.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.Services.AddControllers().AddNewtonsoftJson();
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen();
                builder.Services.AddResponseCompression();
                builder.AddConfigurations();
                builder.Services.AddInfrastructure(builder.Configuration);

                var app = builder.Build();

                if (app.Environment.IsDevelopment())
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.EnableTryItOutByDefault();
                        c.DisplayRequestDuration();
                    });
                }

                app.UseHttpsRedirection();
                app.UseAuthorization();
                app.MapControllers();
                app.MapGraphQL();
                app.UseRateLimiter();
                app.Run();
            }
            catch (Exception ex) when (!ex.GetType().Name.Equals("HostAbortedException", StringComparison.Ordinal)) { }
            finally { }
        }
    }
}