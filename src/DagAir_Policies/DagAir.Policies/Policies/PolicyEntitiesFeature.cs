using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Policies.Commands;
using DagAir.Policies.Policies.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Policies.Policies
{
    public static class PolicyEntitiesFeature
    {
        public static IServiceCollection AddPolicyEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetCurrentRoomPolicyQuery, GetCurrentRoomPolicyQuery>();
            services.AddScoped<ICommandHandler<AddNewRoomPolicyCommand>, AddNewRoomPolicyCommandHandler>();

            return services;
        }
    }
}