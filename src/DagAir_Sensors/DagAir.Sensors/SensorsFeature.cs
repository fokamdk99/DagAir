using AutoMapper;
using DagAir.Sensors.Contracts.DTOs;
using DagAir.Sensors.Data;
using DagAir.Sensors.Data.AppEntities;
using DagAir.Sensors.Sensors;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.Sensors
{
    public static class SensorsFeature
    {
        public static IServiceCollection AddSensorsFeature(this IServiceCollection services)
        {
            services.AddDagAirSensorAppDbContext();
            services.AddSensorEntitiesFeature();
            services.AddAutoMapper(typeof(SensorsFeature).Assembly);
            
            return services;
        }
    }
    
    public class SensorsMappings : Profile
    {
        public SensorsMappings()
        {
            CreateMap<Producer, ProducerDto>();
            CreateMap<Sensor, SensorDto>();
            CreateMap<SensorModel, SensorModelDto>();

            CreateMap<ProducerDto, Producer>()
                .ForMember(x => x.Created, opts => opts.Ignore())
                .ForMember(x => x.Modified, opts => opts.Ignore())
                .ForMember(x => x.SensorModels, opts => opts.Ignore());
            CreateMap<SensorDto, Sensor>()
                .ForMember(x => x.Created, opts => opts.Ignore())
                .ForMember(x => x.Modified, opts => opts.Ignore())
                .ForMember(x => x.SensorModel, opts => opts.Ignore());
            CreateMap<SensorModelDto, SensorModel>()
                .ForMember(x => x.Created, opts => opts.Ignore())
                .ForMember(x => x.Modified, opts => opts.Ignore())
                .ForMember(x => x.Producer, opts => opts.Ignore())
                .ForMember(x => x.Sensors, opts => opts.Ignore());
        }
            
    }
}