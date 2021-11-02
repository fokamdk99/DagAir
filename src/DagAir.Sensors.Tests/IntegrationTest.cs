using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;

namespace DagAir.Sensors.Tests
{
    [Category("Integration")]
    public abstract class IntegrationTest
    {
        protected IServiceProvider Services => CurrentHost!.Services!;
        
        protected IHost? CurrentHost { get; private set; }

        [SetUp]
        public async Task Setup()
        {
            
        }
    }
}