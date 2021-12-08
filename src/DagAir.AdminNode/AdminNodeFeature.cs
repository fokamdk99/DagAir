using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.AdminNode
{
    public static class AdminNodeFeature
    {
        public static IServiceCollection AddAdminNodeFeature(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services;
        }
    }
}