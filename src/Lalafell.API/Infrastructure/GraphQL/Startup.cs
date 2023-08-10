using HotChocolate.Execution.Configuration;
using Lalafell.API.Infrastructure.GraphQL.Query;

namespace Lalafell.API.Infrastructure.GraphQL;

public static class Startup
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        return services
               .AddGraphQLServer()
               .AddQueryTypes()
               .AddFiltering()
               .AddSorting()
               .Services;
    }

    private static IRequestExecutorBuilder AddQueryTypes(this IRequestExecutorBuilder services)
    {
        return services.AddQueryType<ItemQuery>();
    }
}