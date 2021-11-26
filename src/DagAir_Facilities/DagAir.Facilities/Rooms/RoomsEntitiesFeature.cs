using DagAir.Facilities.Affiliates.Commands;
using DagAir.Facilities.Contracts.Commands;
using DagAir.Facilities.Data.AppEntitities;
using DagAir.Facilities.Organizations.Commands;
using DagAir.Facilities.Rooms.Commands;
using DagAir.Facilities.Rooms.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Facilities.Rooms
{
    public static class RoomsEntitiesFeature
    {
        public static IServiceCollection AddRoomsEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetCurrentRoomQuery, GetCurrentRoomQuery>();
            services.AddScoped<ICommandHandler<AddNewRoomCommand, Room>, AddNewRoomCommandHandler>();

            return services;
        }
    }
}