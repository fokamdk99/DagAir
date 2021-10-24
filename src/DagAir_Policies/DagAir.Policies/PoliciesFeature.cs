using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Policies
{
    public static class PoliciesFeature
    {
        public static IServiceCollection AddPoliciesFeature(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services;
        }
    }
}