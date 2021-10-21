using Microsoft.Extensions.DependencyInjection;

namespace DagAir.QueryNode
{
    public static class QueryNodeFeature
    {
        public static IServiceCollection AddQueryNodeFeature(this IServiceCollection services)
        {
            return services;
        }
    }
}