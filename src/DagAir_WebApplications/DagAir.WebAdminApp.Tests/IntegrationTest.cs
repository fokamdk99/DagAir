using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace DagAir.WebAdminApp.Tests
{
    [Category("Integration")]
    public abstract class IntegrationTest
    {
        protected IServiceProvider Services => CurrentHost!.Services;
        protected IHost CurrentHost { get; private set; }

        [SetUp]
        public async Task Setup()
        {
            await SetupPreHost();

            CurrentHost = HostProvider.Create(AddOverrides);
            
            await SetupTest();
        }

        [TearDown]
        protected virtual async Task CleanUp()
        {
            CurrentHost?.Dispose();
        }

        protected virtual Task SetupPreHost()
        {
            return Task.CompletedTask;
        }

        protected abstract void AddOverrides(IServiceCollection services);

        protected virtual Task SetupTest()
        {
            return Task.CompletedTask;
        }
    }
}