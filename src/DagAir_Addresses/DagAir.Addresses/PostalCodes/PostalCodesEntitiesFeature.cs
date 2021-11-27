using DagAir.Addresses.Addresses.Commands;
using DagAir.Addresses.Contracts.Commands;
using DagAir.Addresses.Data.AppEntities;
using DagAir.Addresses.PostalCodes.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Addresses.PostalCodes
{
    public static class PostalCodesEntitiesFeature
    {
        public static IServiceCollection AddPostalCodesEntitiesFeature(this IServiceCollection services)
        {
            services.AddScoped<IGetPostalCodeQuery, GetPostalCodeQuery>();
            services.AddScoped<ICommandHandler<AddNewPostalCodeCommand, PostalCode>, AddNewPostalCodeCommandHandler>();
            
            return services;
        }
    }
}