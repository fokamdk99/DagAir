using DagAir.Policies.Contracts.Commands;
using DagAir.Policies.Data.AppEntities;
using DagAir.Policies.Policies.Commands;
using DagAir.Policies.RoomConditions.Commands;
using DagAir.Policies.RoomConditions.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Policies.RoomConditions
{
    public static class ExpectedRoomConditionsEntitiesFeature
    {
        public static IServiceCollection AddExpectedRoomConditionsEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetExpectedRoomConditionsQuery, GetExpectedRoomConditionsQuery>();
            services.AddScoped<ICommandHandler<AddNewExpectedRoomConditionsCommand, ExpectedRoomConditions>, AddNewExpectedRoomConditionsCommandHandler>();

            return services;
        }
    }
}