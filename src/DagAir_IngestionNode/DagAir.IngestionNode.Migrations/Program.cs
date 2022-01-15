using System.Threading.Tasks;
using DagAir.Components.Influx;
using DagAir.IngestionNode.Data;
using DagAir.IngestionNode.Migrations.Buckets;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DagAir.IngestionNode.Migrations
{
    class Program
    {
        static async Task Main()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddIngestionNodeDataFeature(configuration);

            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            await CreateInfluxBucketTask(serviceProvider.GetRequiredService<IInfluxConfiguration>());
        }

        private static async Task CreateInfluxBucketTask(IInfluxConfiguration cfg)
        {
            await CreateBucketService.CreateBucket(
                cfg.Token.ToCharArray(),
                cfg.BucketName,
                cfg.Org,
                cfg.Url,
                cfg.Retention
            );
        }
    }
}