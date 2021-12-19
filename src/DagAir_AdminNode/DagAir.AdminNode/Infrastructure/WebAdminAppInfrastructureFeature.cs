﻿using DagAir.AdminNode.Affiliates;
using DagAir.AdminNode.Facilities;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.AdminNode.Infrastructure
{
    public static class AdminNodeInfrastructureFeature
    {
        public static IServiceCollection AddAdminNodeInfrastructureFeature(this IServiceCollection services)
        {
            services.AddScoped<IFacilitiesHandler, FacilitiesHandler>();
            services.AddScoped<IAffiliatesHandler, AffiliatesHandler>();
            services.AddSingleton<IExternalServices, ExternalServices>();

            return services;
        }
    }
}