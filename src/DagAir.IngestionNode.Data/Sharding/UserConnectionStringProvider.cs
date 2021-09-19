using System;
using System.Collections.Generic;
using System.Linq;
using DagAir.IngestionNode.Contracts;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
namespace DagAir.IngestionNode.Data.Sharding
{
    public class UserConnectionStringProvider : IUserConnectionStringProvider
    {
        private const string SingleDbConnectionStringName = "DagAirUser";
        private const string ShardsConfigurationSection = "DagAirFactoryUserDatabasePartitioning";

        private readonly ILogger<UserConnectionStringProvider> _logger;

        private readonly Shard[] _shards;

        public UserConnectionStringProvider(IConfiguration configuration, ILogger<UserConnectionStringProvider> logger)
        {
            _logger = logger;

            var shardingConfigurationSection = configuration.GetSection(ShardsConfigurationSection);
            if (shardingConfigurationSection.Exists())
            {
                _shards = LoadShardConfiguration(configuration, shardingConfigurationSection);
            }
            else
            {
                var singleDbConnectionString = configuration.GetConnectionString(SingleDbConnectionStringName);
                if (string.IsNullOrEmpty(singleDbConnectionString))
                {
                    throw new InvalidOperationException(
                        $"Either connection string [{SingleDbConnectionStringName}] or [{ShardsConfigurationSection}] need to be defined");
                }

                _shards = new[] {new Shard(SingleDbConnectionStringName, singleDbConnectionString, long.MaxValue)};
            }
        }

        public string Provide(UserIdentity user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.Id == default)
            {
                throw new ArgumentException($"User Id needs to be provided, got [{user.Id}]");
            }

            return LocateUser(user).ConnectionString;
        }

        public IEnumerable<ConnectionString> ProvideAll()
        {
            return _shards.Select(x => new ConnectionString(x.Name, x.ConnectionString));
        }

        private Shard[] LoadShardConfiguration(IConfiguration configuration,
            IConfigurationSection shardingConfigurationSection)
        {
            var shardsConfigurations = shardingConfigurationSection
                .GetChildren()
                .Select(x => ParseShard(configuration, x)).ToArray();

            if (shardsConfigurations.Length == 1)
            {
                throw new InvalidOperationException(
                    $"Only a single shard defined. Please drop [{shardingConfigurationSection}] and define a single user database connection string [{SingleDbConnectionStringName}]");
            }

            return shardsConfigurations;
        }

        private static Shard ParseShard(IConfiguration configuration, IConfigurationSection x)
        {
            var connectionStringName = x.Key;
            if (!long.TryParse(x.Value, out var boundary))
            {
                throw new FormatException($"Failed to parse shard boundary. Expected a long value, got [{x.Value}]");
            }

            var connectionString = configuration.GetConnectionString(connectionStringName);
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    $"Found boundary for connection string [{connectionStringName}] but no connection string with such name exists. Verify connection string and sharding configuration");
            }

            return new Shard(connectionStringName, connectionString, boundary);
        }

        private Shard LocateUser(UserIdentity user)
        {
            foreach (var shard in _shards)
            {
                if (shard.UserBoundary < user.Id)
                {
                    continue;
                }

                return shard;
            }
            
            _logger.LogWarning($"User [{user.Id}] exceeds the limit of all configured shards. Using last configured shard. Consider adding new shards for User data");
            return _shards.Last();
        }

        private struct Shard
        {
            public Shard(string name, string connectionString, long userBoundary)
            {
                Name = name;
                ConnectionString = connectionString;
                UserBoundary = userBoundary;
            }
            
            public string Name { get; }
            public string ConnectionString { get; }
            public long UserBoundary { get; }
        }
    }
}