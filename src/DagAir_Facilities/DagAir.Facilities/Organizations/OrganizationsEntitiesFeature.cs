using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Data.AppEntitities;
using DagAir.Facilities.Organizations.Commands;
using DagAir.Facilities.Organizations.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Facilities.Organizations
{
    public static class OrganizationsEntitiesFeature
    {
        public static IServiceCollection AddOrganizationsEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetOrganizationQuery, GetOrganizationQuery>();
            services.AddScoped<ICommandHandler<AddNewOrganizationCommand, Organization>, AddNewOrganizationCommandHandler>();

            return services;
        }
    }
}