using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Facilities
{
    public static class FacilitiesFeature
    {
        public static IServiceCollection AddFacilitiesFeature(this IServiceCollection services,
            IConfiguration collection)
        {
            return services;
        }
    }
}