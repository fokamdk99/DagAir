using DagAir.Facilities.Affiliates.Commands;
using DagAir.Facilities.Affiliates.Queries;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Data.AppEntitities;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Facilities.Affiliates
{
    public static class AffiliatesEntitiesFeature
    {
        public static IServiceCollection AddAffiliatesEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetAffiliateQuery, GetAffiliateQuery>();
            services.AddScoped<IGetAffiliatesQuery, GetAffiliatesQuery>();
            services.AddScoped<IGetAffiliatesByOrganizationQuery, GetAffiliatesByOrganizationQuery>();
            services.AddScoped<ICommandHandler<AddNewAffiliateCommand, Affiliate>, AddNewAffiliateCommandHandler>();
            services.AddScoped<IDeleteAffiliateHandler, DeleteAffiliateHandler>();

            return services;
        }
    }
}